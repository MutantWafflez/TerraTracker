using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Crafting {
    /// <summary>
    /// Tracks how many items a player has crafted in total.
    /// </summary>
    public class StatCraftCount : TrackedStat {
        public override string ParentPage => "PlayerCraftingPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.WorkBench);
        }
    }
}