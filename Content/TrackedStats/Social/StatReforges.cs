using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how many reforges a player has done.
    /// </summary>
    internal class StatReforges : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.IronAnvil);
        }
    }
}