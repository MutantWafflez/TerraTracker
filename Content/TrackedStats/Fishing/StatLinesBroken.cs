using System;
using IL.Terraria;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ID;
using TerraTracker.Common.ModTypes;
using Main = Terraria.Main;

namespace TerraTracker.Content.TrackedStats.Fishing {
    /// <summary>
    /// Tracks how many times a player has their fishing line snapped when attempting to reel in a fish.
    /// </summary>
    public class StatLinesBroken : TrackedStat {
        public override string ParentPage => "PlayerFishingPage";

        public override void Load() {
            base.Load();

            Player.ItemCheck_CheckFishingBobber_PullBobber += PullBobber;
        }

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.HighTestFishingLine);
        }

        // IL Edit necessary to track the fishing line breaks
        private void PullBobber(ILContext il) {
            ILCursor c = new(il);

            if (!c.TryGotoNext(i => i.MatchLdfld<Terraria.Player>(nameof(Terraria.Player.accFishingLine)))) {
                return;
            }

            c.Index += 6;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Action<Terraria.Player>>(player => {
                if (player.whoAmI == Main.myPlayer) {
                    theStat.uintStat++;
                }
            });
        }
    }
}