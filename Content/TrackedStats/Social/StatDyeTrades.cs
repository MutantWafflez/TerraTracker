using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how many times a player has traded a Strange Plant for dyes with the dye trader.
    /// </summary>
    public class StatDyeTrades : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.RainbowDye);
        }
    }
}