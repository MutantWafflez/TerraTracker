using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using TerraTracker.Common.Systems;

namespace TerraTracker.Content.UI.Elements {
    /// <summary>
    /// UI button that contains a given tracking page, that upon being clicked on, will cause the parent UIState to swap to the
    /// owned tracking page.
    /// </summary>
    public class UIPageSelector : UIElement {
        private readonly int _myTrackerIndex;
        private readonly Asset<Texture2D> _pageIconAsset;
        private UIPanel _backPanel;
        private UIImage _pageIcon;

        public UIPageSelector(Asset<Texture2D> pageIcon, int myTrackerIndex) {
            _pageIconAsset = pageIcon;
            _myTrackerIndex = myTrackerIndex;
        }

        public override void OnInitialize() {
            Width = Height = new StyleDimension(40f, 0f);
            OnClick += ElementClick;
            OnMouseOver += HoverElement;

            _backPanel = new UIPanel {
                Width = new StyleDimension(0f, 1f),
                Height = new StyleDimension(0f, 1f),
                IgnoresMouseInteraction = true
            };

            Append(_backPanel);

            _pageIcon = new UIImage(_pageIconAsset) {
                HAlign = 0.5f,
                VAlign = 0.5f,
                MaxWidth = new StyleDimension(40f, 0f),
                MaxHeight = new StyleDimension(40f, 0f),
                ScaleToFit = true,
                IgnoresMouseInteraction = true
            };
            _pageIcon.Recalculate();

            _backPanel.Append(_pageIcon);
        }

        public override void OnDeactivate() {
            _backPanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (IsMouseHovering) {
                Main.instance.MouseText(UISystem.Instance.trackerPages[_myTrackerIndex].PageName.GetTranslation(Language.ActiveCulture));
            }

            _backPanel.BackgroundColor = (UISystem.Instance.GetPageIndex() == _myTrackerIndex ? Color.Yellow : new Color(63, 82, 151)) * 0.7f;
        }

        private void ElementClick(UIMouseEvent evt, UIElement listeningElement) {
            UISystem.Instance.SwapPageIndex(_myTrackerIndex);
        }

        private void HoverElement(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
    }
}