﻿using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scriptables
{
    [CreateAssetMenu(menuName = "Events/PlayerSide Event")]
    public class ScriptablePlayerSideEvent : ScriptableEvent<PlayerSide>
    {
    }
}
