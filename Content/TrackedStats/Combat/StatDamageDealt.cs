using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how much damage, no matter the class, a player dealt to enemies.
    /// </summary>
    public class StatDamageDealt : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.CopperShortsword);
        }
    }
}