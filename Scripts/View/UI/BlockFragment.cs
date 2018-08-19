using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.View.UI
{
    public class BlockFragment : MonoBehaviour
    {
        public static readonly string PREFAB_PATH = "Prefabs/UI/BlockFragment";

        public static BlockFragment Create(Vector3 pos, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<BlockFragment>();
            script._Initialize(pos);
            return script;
        }

        private void _Initialize(Vector3 pos)
        {
            transform.localPosition = pos;
            var rect = GetComponent<RectTransform>();
            rect.localPosition = new Vector3(pos.x, pos.y, 0.0f);
        }

    }
}
