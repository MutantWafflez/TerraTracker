using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how much specifically summon damage a player has dealt to enemies.
    /// </summary>
    public class StatSummonDamageDealt : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.StardustHelmet);
        }
    }
}