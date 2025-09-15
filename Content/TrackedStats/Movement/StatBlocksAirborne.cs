using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Movement;

/// <summary>
///     Tracks how many blocks a player has traveled while in the air.
/// </summary>
public class StatBlocksAirborne : TrackedStat {
    public override string ParentPage => "PlayerMovementPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.AngelWings);
    }

    public override void SaveData(Player player, TagCompound tag) {
        tag[FullName] = GetCurrentStat(player).doubleValue;
    }

    public override void LoadData(Player player, TagCompound tag) {
        GetCurrentStat(player).doubleValue = LoadFromTag<float>(tag);
    }

    public override string DisplayStat(Player player) => TerraTracker.DefaultDecimalRepresentation(GetCurrentStat(player).doubleValue);
}