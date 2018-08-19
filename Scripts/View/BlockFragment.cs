using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.View
{
    public class BlockFragment : MonoBehaviour
    {
        public static readonly string PREFAB_PATH = "Prefabs/BlockFragment";

        [SerializeField] private Material _materialRed;
        [SerializeField] private Material _materialBlue;


        public static BlockFragment Create(Block.MaterialType materialType, Vector3 pos, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<BlockFragment>();
            script._Initialize(pos, materialType);
            return script;
        }

        private void _Initialize(Vector3 pos, Block.MaterialType materialType)
        {
            var r = gameObject.GetComponent<Renderer>();
            r.material = materialType == Block.MaterialType.RED ? _materialRed : _materialBlue;
            transform.localPosition = pos;
        }

    }
}
