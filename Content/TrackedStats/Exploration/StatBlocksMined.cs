using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Exploration;

/// <summary>
///     Tracks how many singular blocks a player has broken/mined.
/// </summary>
public class StatBlocksMined : TrackedStat {
    public override string ParentPage => "PlayerExplorationPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.CopperPickaxe);
    }

    public override void Load() {
        base.Load();

        On_AchievementsHelper.HandleMining += OnTileMined;
    }

    private void OnTileMined(On_AchievementsHelper.orig_HandleMining orig) {
        orig();

        GetCurrentStat(Main.LocalPlayer).uintValue++;
    }
}