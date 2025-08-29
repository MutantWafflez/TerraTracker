using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how much specifically magic damage a player has dealt to enemies.
/// </summary>
public class StatMagicDamageDealt : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.NebulaHelmet);
    }
}