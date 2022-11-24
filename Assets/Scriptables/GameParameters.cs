using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scriptables
{
    [CreateAssetMenu]
    public class GameParameters : ScriptableObject
    {
        public int WinningScore;
        public float SlowBallSpeed;
        public float MediumBallSpeed;
        public float FastBallSpeed;
        public float VeryFastBallSpeed;
    }
}
