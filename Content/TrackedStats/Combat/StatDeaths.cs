using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how many deaths a player has.
/// </summary>
public class StatDeaths : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon("Terraria/Images/UI/WorldCreation/IconDifficultyMaster");
    }
}