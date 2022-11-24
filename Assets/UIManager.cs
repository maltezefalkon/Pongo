using Assets.Enums;
using Assets.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class UIManager : MonoBehaviour
    {
        public ScriptableUI UI;
        public SetupUIManager SetupUIManager;
        public GameOverUIManager GameOverUIManager;
        public HUDManager HUDManager;

        private void Awake()
        {
            // turn off all UIs
            foreach (Enums.UI ui in Enum.GetValues(typeof(Enums.UI)))
            {
                GetUIManager(ui)?.gameObject.SetActive(true);
                GetUIManager(ui)?.gameObject.SetActive(false);
            }
            ShowUI(Enums.UI.Setup);
            UI.BeforeValueChanged += UI_BeforeValueChanged;
        }

        private MonoBehaviour GetUIManager(UI ui)
        {
            switch (ui)
            {
                case Enums.UI.None: return null;
                case Enums.UI.Setup: return SetupUIManager;
                case Enums.UI.GameOver: return GameOverUIManager;
                case Enums.UI.HUD: return HUDManager;
                default: throw new ArgumentOutOfRangeException(nameof(ui));
            }
        }

        private void UI_BeforeValueChanged(object sender, ScriptableVariable<Enums.UI>.ValueChangeEventArgs e)
        {
            ShowUI(e.NewValue);
        }

        public void ShowUI(UI ui)
        {
            GetUIManager(UI.RuntimeValue).gameObject.SetActive(false);
            GetUIManager(ui).gameObject.SetActive(true);
        }

        public void HandleGameOver(PlayerSide side)
        {
            GameOverUIManager.WinningSide = side;
            UI.RuntimeValue = Enums.UI.GameOver;
        }
    }
}
