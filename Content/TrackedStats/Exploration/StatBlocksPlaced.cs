using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Exploration {
    /// <summary>
    /// Tracks how many blocks a player has placed.
    /// </summary>
    public class StatBlocksPlaced : TrackedStat {
        public override string ParentPage => "PlayerExplorationPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.DirtBlock);
        }
    }
}