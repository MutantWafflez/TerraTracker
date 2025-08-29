using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Crafting; 

/// <summary>
///     Tracks how many items that a player has crafted that grant buffs on consumption.
/// </summary>
public class StatBuffCraftCount : TrackedStat {
    public override string ParentPage => "PlayerCraftingPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.AlchemyTable);
    }
}