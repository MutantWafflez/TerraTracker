using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerraTracker.Common.Players;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Common.ModTypes;

/// <summary>
///     Custom ModType class that stores data for any given statistic. Handles all autoloading and localization, assuming
///     the class has all data properly initialized.
/// </summary>
public abstract class TrackedStat : ModType {
    /// <summary>
    ///     "Union" (see C/C++ unions) type that will be used to hold stat data of any one of the applicable types defined in the
    ///     struct.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct StatUnion {
        [FieldOffset(0)]
        public uint uintStat;

        [FieldOffset(0)]
        public float floatStat;

        [FieldOffset(0)]
        public long longStat;
    }

    /// <summary>
    ///     Field containing the actual data being tracked.
    /// </summary>
    public StatUnion theStat;

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
    public static void AddUInt<T>(uint increment = 1)
        where T : TrackedStat {
        ModContent.GetInstance<T>().theStat.uintStat += increment;
    }

    /// <summary>
    ///     Adds the specified increment value to the <see cref="float" /> representation
    ///     of this stat.
    /// </summary>
    public static void AddFloat<T>(float increment = 1f)
        where T : TrackedStat {
        ModContent.GetInstance<T>().theStat.floatStat += increment;
    }

    /// <summary>
    ///     Adds the specified increment value to the <see cref="long" /> representation
    ///     of this stat.
    /// </summary>
    public static void AddLong<T>(long increment = 1)
        where T : TrackedStat {
        ModContent.GetInstance<T>().theStat.longStat += increment;
    }

    /// <summary>
    ///     Called after default initialization has occurred in <see cref="InitializeStat" />. Use this to initialize
    ///     any extra data or data structures beyond the <see cref="theStat" /> field.
    /// </summary>
    public virtual void InitializeExtraData() { }

    /// <summary>
    ///     Save stat data to the <paramref name="tag" /> parameter. Defaults
    ///     to saving the data as its <see cref="uint" /> representation.
    /// </summary>
    public virtual void SaveData(TagCompound tag) {
        tag[FullName] = theStat.uintStat;
    }

    /// <summary>
    ///     Load stat data from the <paramref name="tag" /> parameter. Defaults
    ///     to loading the data as its <see cref="uint" /> representation.
    /// </summary>
    public virtual void LoadData(TagCompound tag) {
        theStat.uintStat = LoadFromTag<uint>(tag);
    }

    /// <summary>
    ///     Returns the string representation of the stat being tracked within this class. Defaults
    ///     to displaying the <see cref="uint" /> representation of the data.
    /// </summary>
    public virtual string DisplayStat() => TerraTracker.DefaultIntegerRepresentation(theStat.uintStat);

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
    public void InitializeStat() {
        // Guarantees that the entire union is reset to 0 (since long is the largest data type).
        theStat.longStat = 0;

        InitializeExtraData();
    }

    /// <summary>
    ///     Loads the specified type for this class into the stat struct.
    /// </summary>
    public T LoadFromTag<T>(TagCompound tag, string tagKey = null)
        where T : struct {
        if (!tag.ContainsKey(tagKey ??= FullName)) {
            return default(T);
        }

        return (T)Convert.ChangeType(tag[tagKey], typeof(T))!;
    }
}