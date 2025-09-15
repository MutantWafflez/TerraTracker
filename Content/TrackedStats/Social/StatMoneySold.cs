using Terraria;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social;

/// <summary>
///     Tracks how much money a player has earned by selling items to NPCs.
/// </summary>
public class StatMoneySold : TrackedStat {
    public override string ParentPage => "PlayerSocialPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(TerraTracker.StatIconPath + "MoneySold");
    }

    public override void SaveData(Player player, TagCompound tag) {
        tag[FullName] = GetCurrentStat(player).longValue;
    }

    public override void LoadData(Player player, TagCompound tag) {
        GetCurrentStat(player).longValue = LoadFromTag<long>(tag);
    }

    public override string DisplayStat(Player player) => TerraTracker.DefaultCoinsRepresentation(GetCurrentStat(player).longValue);
}