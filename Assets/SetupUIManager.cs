using Assets.Enums;
using Assets.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets
{
    public class SetupUIManager : BaseUIManager
    {
        public ScriptableBallSpeedRank BallSpeed;
        public ScriptableAgentType LeftPlayerAgentType;
        public ScriptableAgentType RightPlayerAgentType;
        public ScriptableUI UI;

        private void Awake()
        {
        }

        private void OnEnable()
        {
            // TODO: Set the values of the dropdowns based on the variable values
        }

        private void OnDisable()
        {
        }

        private AgentType GetAgent(int index)
        {
            switch (index)
            {
                case 0:  return AgentType.Human;
                case 1:  return AgentType.AI_Staring;
                case 2:  return AgentType.AI_Default;
                default: throw new ArgumentException("Invalid index: " + index.ToString(), nameof(index));
            }
        }

        public void SetLeftPlayerAgent(int index)
        {
            LeftPlayerAgentType.RuntimeValue = GetAgent(index);
        }

        public void SetRightPlayerAgent(int index)
        {
            RightPlayerAgentType.RuntimeValue = GetAgent(index);
        }

        public void SetBallSpeed(int ballSpeed)
        {
            BallSpeedRank speed = (BallSpeedRank)ballSpeed;
            Debug.Log($"Setting ball speed to rank [{speed}] index [{ballSpeed}]");
            BallSpeed.RuntimeValue = speed;
        }

        public void Play()
        {
            UI.RuntimeValue = Enums.UI.HUD;
        }
    }
}
