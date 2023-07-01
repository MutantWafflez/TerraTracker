using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how many critical strikes a player deals.
    /// </summary>
    public class StatCritsDealt : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon("Terraria/Images/UI/UI_quickicon1");
        }
    }
}