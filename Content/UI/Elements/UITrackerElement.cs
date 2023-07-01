using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.UI.Elements {
    /// <summary>
    /// Smaller element that displays its given tracking statistic.
    /// </summary>
    public class UITrackerElement : UIPanel {
        private readonly TrackedStat _myStat;

        private UIElement _statIconRegion;
        private UIImage _statIcon;
        private UIText _statName;
        private UIText _statData;

        public UITrackerElement(TrackedStat stat) {
            _myStat = stat;
        }

        public override void OnInitialize() {
            Width.Set(-24f, 1f);
            Height.Set(60f, 0f);

            _statIconRegion = new UIElement {
                VAlign = 0.5f,
                Width = new StyleDimension(36f, 0f),
                Height = new StyleDimension(36f, 0f)
            };
            _statIcon = new UIImage(_myStat.statIcon) {
                VAlign = 0.5f,
                HAlign = 0.5f,
                ScaleToFit = true
            };

            _statIconRegion.Append(_statIcon);
            Append(_statIconRegion);

            _statName = new UIText(_myStat.StatName.GetTranslation(Language.ActiveCulture)) {
                VAlign = 0.5f,
                Left = new StyleDimension(_statIconRegion.Width.Pixels + 4f, 0f)
            };

            Append(_statName);

            _statData = new UIText("0") {
                VAlign = 0.5f,
                HAlign = 1f
            };

            Append(_statData);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            _statData.SetText(_myStat.DisplayStat());
        }
    }
}