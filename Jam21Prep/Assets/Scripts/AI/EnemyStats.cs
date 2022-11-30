using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MyStuff
{
    [CreateAssetMenu (menuName = "PlugableAI/EnemyStats")]
    public class EnemyStats
    {
        public float moveSpeed = 7;
        public float lookRange = 40f;

        public float attackRange = 1;
        public float attackRate = 1;

        public float searchDuration = 4f;
        public float searchingTurnSpeed = 120f;
    }
}
