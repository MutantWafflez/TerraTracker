using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how many time a player has received a heal from the nurse.
    /// </summary>
    public class StatNurseHeals : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon("Terraria/Images/NPC_Head_" + NPCHeadID.Nurse);
        }
    }
}