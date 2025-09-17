using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;
using TerraTracker.Common.Systems;
using TerraTracker.Content.TrackedStats.Combat;
using TerraTracker.Content.TrackedStats.Movement;
using TerraTracker.Content.TrackedStats.Social;
using TerraTracker.DataStructures.Structs;

namespace TerraTracker.Common.Players;

/// <summary>
///     ModPlayer that handles all tracking for player related information.
/// </summary>
public class TrackerPlayer : ModPlayer {
    /// <summary>
    ///     Dictionary that holds all of the stats for this player.
    /// </summary>
    public Dictionary<string, StatUnion> stats;

    public override void Load() {
        IL_Player.SellItem += PlayerSoldItem;
    }

    public override void Initialize() {
        stats = [];

        foreach (TrackedStat stat in ModContent.GetContent<TrackedStat>()) {
            stat.InitializeStat(Player);
        }
    }

    public override void SaveData(TagCompound tag) {
        foreach (TrackedStat stat in ModContent.GetContent<TrackedStat>()) {
            stat.SaveData(Player, tag);
        }
    }

    public override void LoadData(TagCompound tag) {
        foreach (TrackedStat stat in ModContent.GetContent<TrackedStat>()) {
            stat.LoadData(Player, tag);
        }
    }

    public override void ProcessTriggers(TriggersSet triggersSet) {
        if (UISystem.ToggleTrackerUIKeybind.JustPressed) {
            UISystem.Instance.ToggleUI();
        }
    }

    public override void UpdateDead() {
        ModContent.GetInstance<StatLongestLife>().currentLifeTime = 0;
    }

    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
        if (Main.myPlayer != Player.whoAmI) {
            return;
        }

        TrackedStat.AddUInt<StatDeaths>(Player);
    }

    public override void OnHurt(Player.HurtInfo info) {
        if (Main.myPlayer != Player.whoAmI) {
            return;
        }

        ModContent.GetInstance<StatDamageBlocked>().preDefenseDamage = (uint)info.SourceDamage;
        TrackedStat.AddUInt<StatDamageReceived>(Player, (uint)info.Damage);

        StatDamageBlocked damageBlockStat = ModContent.GetInstance<StatDamageBlocked>();
        damageBlockStat.GetCurrentStat(Player).uintValue += damageBlockStat.preDefenseDamage - (uint)info.Damage;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
        AddToDamageStats((uint)hit.Damage, hit.DamageType, hit.Crit);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone) {
        AddToDamageStats((uint)hit.Damage, hit.DamageType, hit.Crit);
    }

    public override void OnConsumeAmmo(Item weapon, Item ammo) {
        if (Main.myPlayer != Player.whoAmI) {
            return;
        }

        TrackedStat.AddUInt<StatAmmoConsumed>(Player);
    }

    public override void PostUpdate() {
        if (Main.myPlayer != Player.whoAmI) {
            return;
        }

        StatLongestLife curLongestLife = ModContent.GetInstance<StatLongestLife>();
        if (++curLongestLife.currentLifeTime > curLongestLife.GetCurrentStat(Player).uintValue) {
            curLongestLife.GetCurrentStat(Player).uintValue = curLongestLife.currentLifeTime;
        }

        if (Player.justJumped) {
            TrackedStat.AddUInt<StatJumps>(Player);
        }

        if (Player.timeSinceLastDashStarted == 0) {
            TrackedStat.AddUInt<StatDashes>(Player);
        }

        if (Player.immune) {
            TrackedStat.AddUInt<StatImmuneTime>(Player);
        }

        Vector2 velocity = Player.velocity;
        if (velocity.HasNaNs()) {
            return;
        }

        StatMaxSpeed maxSpeedStat = ModContent.GetInstance<StatMaxSpeed>();
        if (velocity.Length() is var length && length > maxSpeedStat.GetCurrentStat(Player).doubleValue) {
            maxSpeedStat.GetCurrentStat(Player).doubleValue = length;
        }

        if (velocity.Y == 0f) {
            TrackedStat.AddDouble<StatBlocksWalked>(Player, length / 16f);
        }
        else {
            TrackedStat.AddDouble<StatBlocksAirborne>(Player, length / 16f);

            if (Player.gravity == 1f ? velocity.Y < 0f : velocity.Y > 0f) {
                TrackedStat.AddDouble<StatBlocksFallen>(Player, Math.Abs(velocity.Y / 16f));
            }
        }
    }

    public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item) {
        // TODO: Fix double buy?
        TrackedStat.AddUInt<StatItemsBought>(Player);
    }

    public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price) {
        TrackedStat.AddUInt<StatNurseHeals>(Player);
    }

    public override void GetDyeTraderReward(List<int> rewardPool) {
        TrackedStat.AddUInt<StatDyeTrades>(Player);
    }

    private void AddToDamageStats(uint damage, DamageClass damageClass, bool crit) {
        TrackedStat.AddUInt<StatDamageDealt>(Player, damage);
        if (crit) {
            TrackedStat.AddUInt<StatCritsDealt>(Player);
        }

        if (damageClass.CountsAsClass(DamageClass.Melee)) {
            TrackedStat.AddUInt<StatMeleeDamageDealt>(Player, damage);
        }

        if (damageClass.CountsAsClass(DamageClass.Magic)) {
            TrackedStat.AddUInt<StatMagicDamageDealt>(Player, damage);
        }

        if (damageClass.CountsAsClass(DamageClass.Ranged)) {
            TrackedStat.AddUInt<StatRangedDamageDealt>(Player, damage);
        }

        if (damageClass.CountsAsClass(DamageClass.Summon)) {
            TrackedStat.AddUInt<StatSummonDamageDealt>(Player, damage);
        }
    }

    // Not put into its own class due to it being needed for two distinct stats
    private void PlayerSoldItem(ILContext il) {
        ILCursor c = new(il);

        if (!c.TryGotoNext(MoveType.After, i => i.MatchStarg(2))) {
            return;
        }

        c.Emit(OpCodes.Ldarg_2);
        c.EmitDelegate<Action<int>>(itemStack => TrackedStat.AddUInt<StatItemsSold>(Main.LocalPlayer, (uint)itemStack));

        if (!c.TryGotoNext(MoveType.After, i => i.MatchStloc(6))) {
            return;
        }

        c.Emit(OpCodes.Ldloc_3);
        c.EmitDelegate<Action<int>>(sellAmount => TrackedStat.AddLong<StatMoneySold>(Main.LocalPlayer, sellAmount));
    }
}