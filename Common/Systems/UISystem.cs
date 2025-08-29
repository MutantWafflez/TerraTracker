using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TerraTracker.Content.UI;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Common.Systems; 

/// <summary>
///     Mod System that handles all the UI in the mod.
/// </summary>
public class UISystem : ModSystem {
    public List<UITrackerPage> trackerPages = new();

    private UserInterface _trackerInterface;
    private UITrackerState _trackerState;
    private GameTime _lastGameTime;

    public static UISystem Instance {
        get;
        private set;
    }

    public static ModKeybind ToggleTrackerUIKeybind {
        get;
        private set;
    }

    public UISystem() {
        Instance = this;
    }

    public override void Load() {
        ToggleTrackerUIKeybind = KeybindLoader.RegisterKeybind(Mod, "ToggleTrackerUI", "O");
    }

    public override void SetStaticDefaults() {
        _trackerInterface = new UserInterface();
        _trackerState = new UITrackerState();
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        int specifiedIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));

        if (specifiedIndex != -1) {
            layers.Insert(
                specifiedIndex,
                new LegacyGameInterfaceLayer(
                    "TerraTracker: Tracker Window",
                    delegate {
                        if (_lastGameTime is not null && _trackerInterface.CurrentState is not null) {
                            _trackerInterface.Draw(Main.spriteBatch, _lastGameTime);
                        }

                        return true;
                    },
                    InterfaceScaleType.UI
                )
            );
        }
    }

    public override void UpdateUI(GameTime gameTime) {
        _lastGameTime = gameTime;

        _trackerInterface.Update(_lastGameTime);
    }

    public void SwapPageIndex(int newIndex) {
        _trackerState.SwapTrackerPage(newIndex);
    }

    public int GetPageIndex() => _trackerState.TrackerIndex;

    public void ToggleUI() {
        _trackerInterface.SetState(_trackerInterface.CurrentState is null ? _trackerState : null);
    }
}