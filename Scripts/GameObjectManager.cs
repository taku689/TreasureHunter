using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private Transform _bases;
    [SerializeField] public Transform Bases { get { return _bases; } }
    [SerializeField] private Transform _blocks;
    [SerializeField] public Transform Blocks { get { return _blocks; } }
}
