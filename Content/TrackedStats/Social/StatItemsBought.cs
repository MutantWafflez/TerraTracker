using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how many items a player has bought from NPCs.
    /// </summary>
    public class StatItemsBought : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            // TODO: Get actual icon
            base.SetStaticDefaults();
            //statIcon = TerraTracker.GetIcon("Terraria/Images/Chat");
        }
    }
}