using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerraTracker.Common.Systems;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Content.UI; 

/// <summary>
///     Background state that holds children of the elements that don't change (other than the tracker page).
/// </summary>
public class UITrackerState : UIState {
    private UIPanel _backPanel;

    public int TrackerIndex {
        get;
        private set;
    }

    public override void OnInitialize() {
        _backPanel = new UIPanel { VAlign = 0.5f, HAlign = 0.5f, Width = new StyleDimension(500f, 0f), Height = new StyleDimension(700f, 0f) };
        _backPanel.SetPadding(2f);

        Append(_backPanel);

        // The default page (Player Combat Page)
        UITrackerPage combatPage = UISystem.Instance.trackerPages[TrackerIndex = 0];
        combatPage.Activate();

        _backPanel.Append(combatPage);

        StyleDimension leftPos = new(0f, 0f);
        for (int i = 0; i < UISystem.Instance.trackerPages.Count; i++) {
            UITrackerPage page = UISystem.Instance.trackerPages[i];
            UIPageSelector selector = new(page.PageIcon, i) { Left = leftPos };
            selector.Activate();
            selector.Recalculate();

            leftPos.Pixels += selector.GetDimensions().Width + 2f;

            _backPanel.Append(selector);
        }
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

        if (_backPanel.IsMouseHovering) {
            Main.LocalPlayer.mouseInterface = true;
        }
    }

    public void SwapTrackerPage(int newIndex) {
        IReadOnlyList<UITrackerPage> pages = UISystem.Instance.trackerPages;

        _backPanel.RemoveChild(pages[TrackerIndex]);

        UITrackerPage newPage = pages[newIndex];
        if (!newPage.IsInitialized) {
            newPage.Activate();
        }

        _backPanel.Append(newPage);
        TrackerIndex = newIndex;
    }
}