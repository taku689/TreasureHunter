using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunter
{
    public class UIManager : MonoBehaviour
    {
        public Transform Players { get { return _players; } }
        public Transform Lifes { get { return _lifes; } }
        public Transform Root { get { return transform; } }
        [SerializeField] private Transform _players;
        [SerializeField] private Transform _lifes;
    }
}
