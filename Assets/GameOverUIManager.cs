using Assets;
using Assets.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    public TextMeshProUGUI WinnerMessageTextBox;

    private PlayerSide _winningSide;


    public void PlayAgain()
    {
        GameManager.Instance.Initialize();
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
