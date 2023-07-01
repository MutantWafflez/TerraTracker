using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how many items the player has sold to NPCs.
    /// </summary>
    public class StatItemsSold : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            // TODO: Get actual icon
            base.SetStaticDefaults();
            //statIcon = TerraTracker.GetIcon("Terraria/Images/Chat");
        }
    }
}