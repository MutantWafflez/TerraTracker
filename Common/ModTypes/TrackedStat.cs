using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerraTracker.Common.Players;
using TerraTracker.Content.UI.Elements;
using TerraTracker.DataStructures.Structs;

namespace TerraTracker.Common.ModTypes;

/// <summary>
///     Custom ModType class that stores data for any given statistic. Handles all autoloading and localization, assuming
///     the class has all data properly initialized.
/// </summary>
public abstract class TrackedStat : ModType {
    /// <summary>
    ///     The icon texture to be displayed to the side of the stat name. Defaults to the "?" icon.
    /// </summary>
    public Asset<Texture2D> statIcon;

    /// <summary>
    ///     The localization for the name of this stat. Defaults to the given Mod Path + "Stat" + Internal Name.
    /// </summary>
    public virtual LocalizedText StatName => Language.GetText($"Mods.TerraTracker.Stat.{Name}");

    /// <summary>
    ///     The internal name of the <see cref="UITrackerPage" /> class that this stat belongs to.
    /// </summary>
    public abstract string ParentPage {
        get;
    }

    /// <summary>
    ///     Adds the specified increment value to the <see cref="uint" /> representation
    ///     of this stat.
    /// </summary>
    public static void AddUInt<T>(Player player, uint increment = 1) where T : TrackedStat {
        ModContent.GetInstance<T>().GetCurrentStat(player).uintValue += increment;
    }

    /// <summary>
    ///     Adds the specified increment value to the <see cref="float" /> representation
    ///     of this stat.
    /// </summary>
    public static void AddFloat<T>(Player player, float increment = 1f) where T : TrackedStat {
        ModContent.GetInstance<T>().GetCurrentStat(player).floatValue += increment;
    }

    /// <summary>
    ///     Adds the specified increment value to the <see cref="double" /> representation
    ///     of this stat.
    /// </summary>
    public static void AddDouble<T>(Player player, double increment = 1d) where T : TrackedStat {
        ModContent.GetInstance<T>().GetCurrentStat(player).doubleValue += increment;
    }

    /// <summary>
    ///     Adds the specified increment value to the <see cref="long" /> representation
    ///     of this stat.
    /// </summary>
    public static void AddLong<T>(Player player, long increment = 1) where T : TrackedStat {
        ModContent.GetInstance<T>().GetCurrentStat(player).longValue += increment;
    }

    /// <summary>
    ///     Called after default initialization has occurred in <see cref="InitializeStat" />. Use this to initialize
    ///     any extra data or data structures beyond the <see cref="GetCurrentStat" /> return stat.
    /// </summary>
    public virtual void InitializeExtraData() { }

    /// <summary>
    ///     Save stat data to the <paramref name="tag" /> parameter. Defaults
    ///     to saving the data as its <see cref="uint" /> representation.
    /// </summary>
    public virtual void SaveData(Player player, TagCompound tag) {
        tag[FullName] = GetCurrentStat(player).uintValue;
    }

    /// <summary>
    ///     Load stat data from the <paramref name="tag" /> parameter. Defaults
    ///     to loading the data as its <see cref="uint" /> representation.
    /// </summary>
    public virtual void LoadData(Player player, TagCompound tag) {
        GetCurrentStat(player).uintValue = LoadFromTag<uint>(tag);
    }

    /// <summary>
    ///     Returns the string representation of the stat being tracked within this class. Defaults
    ///     to displaying the <see cref="uint" /> representation of the data.
    /// </summary>
    public virtual string DisplayStat(Player player) => TerraTracker.DefaultIntegerRepresentation(GetCurrentStat(player).uintValue);

    public sealed override void SetupContent() => SetStaticDefaults();

    public override void SetStaticDefaults() {
        statIcon = ModContent.Request<Texture2D>("Terraria/Images/NPC_Head_0");
    }

    protected sealed override void Register() {
        ModTypeLookup<TrackedStat>.Register(this);
    }

    /// <summary>
    ///     Called by the corresponding save handler (<see cref="TrackerPlayer" /> for player stats, for example) to
    ///     initialize this stat to its default
    /// </summary>
    public void InitializeStat(Player player) {
        player.GetModPlayer<TrackerPlayer>().stats[FullName] = new StatUnion();

        InitializeExtraData();
    }

    /// <summary>
    ///     Loads the specified type for this class into the stat struct.
    /// </summary>
    public T LoadFromTag<T>(TagCompound tag, string tagKey = null) where T : struct {
        if (!tag.ContainsKey(tagKey ??= FullName)) {
            return default(T);
        }

        return (T)Convert.ChangeType(tag[tagKey], typeof(T))!;
    }

    /// <summary>
    ///     Gets a reference to the <see cref="StatUnion" /> related to this stat and player.
    /// </summary>
    public ref StatUnion GetCurrentStat(Player player) => ref CollectionsMarshal.GetValueRefOrNullRef(player.GetModPlayer<TrackerPlayer>().stats, FullName);
}