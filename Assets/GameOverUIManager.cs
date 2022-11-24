using Assets;
using Assets.Enums;
using Assets.Scriptables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIManager : BaseUIManager
{
    public TextMeshProUGUI WinnerMessageTextBox;
    public ScriptableUI UI;

    private PlayerSide _winningSide;

    private void OnEnable()
    {
        EnableControls(true);
    }

    private void OnDisable()
    {
        EnableControls(false);
    }

    private void EnableControls(bool enable)
    {
    }

    public void PlayAgain()
    {

        UI.RuntimeValue = Assets.Enums.UI.Setup;
    }

    public PlayerSide WinningSide
    {
        get
        {
            return _winningSide;
        }
        set
        {
            _winningSide = value;
            WinnerMessageTextBox.text = $"{_winningSide} Player Wins!";
        }
    }
}
