using Terraria;
using Terraria.ID;
using TerraTracker.Common.ModTypes;
using Main = Terraria.Main;

namespace TerraTracker.Content.TrackedStats.Combat;

/// <summary>
///     Tracks how many times a player has dodged an attack.
/// </summary>
public class StatDodges : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void Load() {
        base.Load();

        // Only ever called when a dodge occurs. Thus, we will be detouring it
        On_Player.SetImmuneTimeForAllTypes += PlayerDodged;
    }

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.BlackBelt);
    }

    private void PlayerDodged(On_Player.orig_SetImmuneTimeForAllTypes orig, Player self, int time) {
        orig(self, time);

        if (Main.myPlayer == self.whoAmI) {
            theStat.uintStat++;
        }
    }
}