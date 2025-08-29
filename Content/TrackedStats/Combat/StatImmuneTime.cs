using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how long a player has been immune in terms of game ticks.
/// </summary>
public class StatImmuneTime : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.CrossNecklace);
    }

    public override string DisplayStat() => TerraTracker.TicksToTimeString(theStat.uintStat);
}