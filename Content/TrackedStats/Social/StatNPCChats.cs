using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social; 

/// <summary>
///     Tracks how many times a player has talked to an NPC.
/// </summary>
public class StatNPCChats : TrackedStat {
    public override string ParentPage => "PlayerSocialPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon("Terraria/Images/Chat");
    }
}