using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTracker.Common.ModTypes;
using TerraTracker.Content.TrackedStats.Combat;
using TerraTracker.Content.TrackedStats.Crafting;
using TerraTracker.Content.TrackedStats.Fishing;
using TerraTracker.Content.TrackedStats.Social;

namespace TerraTracker.Common.GlobalItems;

/// <summary>
///     Global Item that handles item statistic tracking.
/// </summary>
public class TrackerGlobalItem : GlobalItem {
    public override bool ConsumeItem(Item item, Player player) {
        if (Main.myPlayer != player.whoAmI) {
            return base.ConsumeItem(item, player);
        }

        if (item.healLife > 0) {
            TrackedStat.AddUInt<StatHealingItemsConsumed>(player);
        }

        if (item.healMana > 0) {
            TrackedStat.AddUInt<StatManaItemsConsumed>(player);
        }

        if (item.buffType <= 0) {
            return base.ConsumeItem(item, player);
        }

        if (ItemID.Sets.IsFood[item.type]) {
            TrackedStat.AddUInt<StatFoodsConsumed>(player);
        }

        TrackedStat.AddUInt<StatBuffItemsConsumed>(player);

        return base.ConsumeItem(item, player);
    }

    public override void OnCreated(Item item, ItemCreationContext context) {
        if (context is not RecipeItemCreationContext recipeContext) {
            return;
        }

        Item createItem = recipeContext.Recipe.createItem;

        TrackedStat.AddUInt<StatCraftCount>(Main.LocalPlayer, (uint)createItem.stack);

        StatMostCrafted mostCraftedStat = ModContent.GetInstance<StatMostCrafted>();
        string craftKey = createItem.ModItem is null ? createItem.type.ToString() : createItem.ModItem.FullName;
        if (!mostCraftedStat.craftCounts.TryAdd(craftKey, (uint)createItem.stack)) {
            mostCraftedStat.craftCounts[craftKey] += (uint)createItem.stack;
        }

        if (createItem.buffTime > 0) {
            TrackedStat.AddUInt<StatBuffCraftCount>(Main.LocalPlayer, (uint)createItem.stack);
        }
    }

    public override void OnSpawn(Item item, IEntitySource source) {
        if (!item.IsACoin || source is not EntitySource_Gift { Entity: NPC { type: NPCID.TaxCollector } npc } || Main.LocalPlayer.talkNPC != npc.type) {
            return;
        }

        long value = 0;
        value += item.type switch {
            ItemID.PlatinumCoin => Item.platinum,
            ItemID.GoldCoin => Item.gold,
            ItemID.SilverCoin => Item.silver,
            _ => Item.copper
        };
        value *= item.stack;

        TrackedStat.AddLong<StatTaxesCollected>(Main.LocalPlayer, value);
    }

    public override void CaughtFishStack(int type, ref int stack) {
        Item caughtItem = new(type);

        TrackedStat.AddUInt<StatThingsCaught>(Main.LocalPlayer);

        if (ItemID.Sets.IsFishingCrate[caughtItem.type] || ItemID.Sets.IsFishingCrateHardmode[caughtItem.type]) {
            TrackedStat.AddUInt<StatCratesCaught>(Main.LocalPlayer);
        }

        if (caughtItem.rare == ItemRarityID.Gray) {
            TrackedStat.AddUInt<StatTrashCaught>(Main.LocalPlayer);
        }

        if (caughtItem.questItem) {
            TrackedStat.AddUInt<StatQuestFishCaught>(Main.LocalPlayer);
        }
    }

    public override void PostReforge(Item item) {
        TrackedStat.AddUInt<StatReforges>(Main.LocalPlayer);
    }
}