using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TreasureHunter.View
{
    public class Base : MonoBehaviour
    {
        public static readonly string PREFAB_PATH = "Prefabs/Base";

        public enum MaterialType
        {
            A,
            B,
        }

        [SerializeField] private Material _materialA;
        [SerializeField] private Material _materialB;

        public static Base Create(Vector3 pos, MaterialType materialType, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<Base>();
            script._Initialize(pos, materialType);
            return script;
        }

        private void _Initialize(Vector3 pos, MaterialType materialType)
        {
            var r = gameObject.GetComponent<Renderer>();
            r.material = materialType == MaterialType.A ? _materialA : _materialB;
            transform.localPosition = pos;
        }
    }
}