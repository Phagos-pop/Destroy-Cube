using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcsCore.Components
{
    public class DashComponent : MonoBehaviour
    {
        public float minDelayBeforeDash = 3;
        public float maxDelayBeforeDash = 6;
        public float dashTime = 1;
        public float dashSpeedIncrease = 3;
    }
}

