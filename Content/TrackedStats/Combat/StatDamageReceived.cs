using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how much damage a player has received in total.
/// </summary>
public class StatDamageReceived : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(TerraTracker.StatIconPath + "DamageReceived");
    }
}