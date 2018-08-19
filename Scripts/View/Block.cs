using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.View
{
    public class Block : MonoBehaviour
    {
        private List<BlockFragment> _fragments = new List<BlockFragment>();
        public static readonly string PREFAB_PATH = "Prefabs/Block";

        public enum MaterialType
        {
            RED,
            BLUE,
        }

        public static Block Create(MaterialType materialType, List<Vector3> fragmentPositions, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<Block>();
            script._Initialize(materialType, fragmentPositions);
            return script;
        }

        private void _Initialize(MaterialType materialType, List<Vector3> fragmentPositions)
        {
            var parent = transform;
            foreach (var fragmentPos in fragmentPositions)
            {
                _fragments.Add(BlockFragment.Create(materialType, fragmentPos, parent));
            }
        }
    }
}
