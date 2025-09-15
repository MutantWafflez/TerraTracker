using Terraria;
using Terraria.ModLoader;
using TerraTracker.Common.ModTypes;
using TerraTracker.Content.TrackedStats.Exploration;

namespace TerraTracker.Common.GlobalTiles;

/// <summary>
///     GlobalTile that assists with tracking tile related stats for a given player.
/// </summary>
public class TrackerGlobalTile : GlobalTile {
    public override void PlaceInWorld(int i, int j, int type, Item item) {
        TrackedStat.AddUInt<StatBlocksPlaced>(Main.LocalPlayer);
    }
}