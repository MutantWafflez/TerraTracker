using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Movement {
    /// <summary>
    /// Tracks how many dashes a player has done.
    /// </summary>
    public class StatDashes : TrackedStat {
        public override string ParentPage => "PlayerMovementPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.Tabi);
        }
    }
}