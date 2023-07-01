using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Fishing {
    /// <summary>
    /// Tracks how many trash items a player has fished up.
    /// </summary>
    public class StatTrashCaught : TrackedStat {
        public override string ParentPage => "PlayerFishingPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.TinCan);
        }
    }
}