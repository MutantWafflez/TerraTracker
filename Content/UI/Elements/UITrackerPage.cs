using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TerraTracker.Common.ModTypes;
using TerraTracker.Common.Systems;

namespace TerraTracker.Content.UI.Elements;

/// <summary>
///     A "page" that holds all the statistics for a certain category of data. (Player stats,
///     world stats, etc.)
/// </summary>
public abstract class UITrackerPage : UIElement, ILoadable {
    private UIScrollbar _statListScroll;
    private UIList _statList;

    /// <summary>
    ///     The internal class type's name.
    /// </summary>
    public virtual string InternalName => GetType().Name;

    /// <summary>
    ///     The name of this page that will be shown to the user. Localization path defaults to Mod Path +  "PageName" + Internal
    ///     Name
    /// </summary>
    public virtual LocalizedText PageName => Language.GetText($"Mods.TerraTracker.PageName.{InternalName}");

    public bool IsInitialized {
        get;
        private set;
    }

    /// <summary>
    ///     The Mod this page belongs to.
    /// </summary>
    public Mod Mod {
        get;
        private set;
    }

    public abstract Asset<Texture2D> PageIcon {
        get;
    }

    public override void OnInitialize() {
        HAlign = 0.5f;
        Top.Set(0f, 0.1f);
        Width.Set(0f, 0.95f);
        Height.Set(0f, 0.875f);

        _statListScroll = new UIScrollbar { VAlign = 1f, Left = new StyleDimension(-18f, 1f), Height = new StyleDimension(0f, 1f) };

        _statList = new UIList { VAlign = 1f, HAlign = 0.5f, Width = new StyleDimension(0f, 1f), Height = new StyleDimension(0f, 1f) };
        _statList.SetScrollbar(_statListScroll);

        Append(_statList);
        Append(_statListScroll);

        _statList.AddRange(ModContent.GetContent<TrackedStat>().Where(stat => stat.ParentPage == InternalName).Select(stat => new UITrackerElement(stat)));

        for (int i = 0; i < _statList.Count; i++) {
            _statList._items[i].Activate();
        }

        IsInitialized = true;
    }

    public void Load(Mod mod) {
        Mod = mod;

        UISystem.Instance.trackerPages.Add(this);
    }

    public void Unload() { }
}