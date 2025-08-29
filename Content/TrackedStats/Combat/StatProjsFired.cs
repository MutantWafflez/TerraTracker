using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how many projectiles a player creates, whether directly or indirectly.
/// </summary>
public class StatProjsFired : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.WoodenArrow);
    }
}