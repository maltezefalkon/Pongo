using Assets.Enums;
using Assets.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets
{
    public class HUDManager : BaseUIManager
    {
        public GameObject HUD;

        public GameObject Ball;

        public GameObject LeftPaddle;
        public GameObject RightPaddle;
        public GameObject LeftPaddleStartingPosition;
        public GameObject RightPaddleStartingPosition;

        public TextMeshProUGUI LeftPlayerScoreDisplay;
        public TextMeshProUGUI RightPlayerScoreDisplay;

        public ScriptableInt LeftPlayerScore;
        public ScriptableInt RightPlayerScore;

        public void Awake()
        {
            LeftPlayerScore.AfterValueChanged += LeftPlayerScore_AfterValueChanged;
            RightPlayerScore.AfterValueChanged += RightPlayerScore_AfterValueChanged;
        }

        private void RightPlayerScore_AfterValueChanged(object sender, ScriptableVariable<int>.ValueChangeEventArgs e)
        {
            RightPlayerScoreDisplay.text = RightPlayerScore.RuntimeValue.ToString();
        }

        private void LeftPlayerScore_AfterValueChanged(object sender, ScriptableVariable<int>.ValueChangeEventArgs e)
        {
            LeftPlayerScoreDisplay.text = LeftPlayerScore.RuntimeValue.ToString();
        }

        private void OnEnable()
        {
            ResetScores();
            ResetPaddlePositions();
            EnableControls(true);
        }

        private void ResetPaddlePositions()
        {
            LeftPaddle.transform.position = LeftPaddleStartingPosition.transform.position;
            RightPaddle.transform.position = RightPaddleStartingPosition.transform.position;
        }

        private void ResetScores()
        {
            LeftPlayerScore.RuntimeValue = 0;
            RightPlayerScore.RuntimeValue = 0;
        }

        private void OnDisable()
        {
            EnableControls(false);
        }

        /// <summary>
        /// Shows or hides the game controls.
        /// </summary>
        /// <remarks>
        /// This is necessary because the HUDManager is not attached to a parent
        /// object that is a common parent to all of the HUD controls.
        /// </remarks>
        /// <param name="enable"></param>
        private void EnableControls(bool enable)
        {
            LeftPaddle.SetActive(enable);
            RightPaddle.SetActive(enable);
            Ball.SetActive(enable);
            LeftPlayerScoreDisplay.gameObject.SetActive(enable);
            RightPlayerScoreDisplay.gameObject.SetActive(enable);
        }

        public void ScorePoint(PlayerSide side)
        {
        }
    }
}
