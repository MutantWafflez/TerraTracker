using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how many food items a player has consumed.
    /// </summary>
    public class StatFoodsConsumed : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(TerraTracker.StatIconPath + "FoodsConsumed");
        }
    }
}