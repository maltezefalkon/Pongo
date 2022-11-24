using Assets.Enums;
using Assets.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        public PaddleController Paddle;
        public PlayerSide Side;
        public ScriptableAgentType Agent;
        public ScriptableInt Score;
        public TextMeshProUGUI ScoreDisplay;

        private void OnEnable()
        {
            if (Paddle == null) throw new Exception($"No {nameof(Paddle)} passed to {nameof(PaddleController)}");
            if (ScoreDisplay == null) throw new Exception($"No {nameof(ScoreDisplay)} passed to {nameof(PaddleController)}");
            if (Side == PlayerSide.None) throw new Exception($"No {nameof(Side)} set up for {nameof(PaddleController)}");
            if (Score == null) throw new Exception($"No {nameof(Score)} set up for {nameof(PaddleController)}");
            if (ScoreDisplay == null) throw new Exception($"No {nameof(ScoreDisplay)} set up for {nameof(PaddleController)}");
        }
    }
}
