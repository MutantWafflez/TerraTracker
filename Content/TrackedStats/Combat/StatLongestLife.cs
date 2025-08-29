using Terraria.ID;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks the longest time a player has stayed alive without dying in terms of game ticks.
/// </summary>
public class StatLongestLife : TrackedStat {
    /// <summary>
    ///     How long a player has been alive for THIS specific life.
    /// </summary>
    public uint currentLifeTime;

    public override string ParentPage => "PlayerCombatPage";

    private string CurrentLifeIOString => $"{Mod.Name}/CurrentLifeTime";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.Heart);
    }

    public override void InitializeExtraData() {
        currentLifeTime = 0;
    }

    public override void SaveData(TagCompound tag) {
        base.SaveData(tag);

        tag[CurrentLifeIOString] = currentLifeTime;
    }

    public override void LoadData(TagCompound tag) {
        base.LoadData(tag);

        currentLifeTime = LoadFromTag<uint>(tag, CurrentLifeIOString);
    }

    public override string DisplayStat() => TerraTracker.TicksToTimeString(theStat.uintStat);
}