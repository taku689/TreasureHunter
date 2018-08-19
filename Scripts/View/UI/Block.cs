using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.View.UI
{
    public class Block : MonoBehaviour
    {
        private Vector3 ORIGINAL_SCALE = new Vector3(1.0f, 1.0f, 0.0f);
        private Vector3 ON_DRAG_SCALE = new Vector3(2.65f, 2.65f, 0.0f);
        private List<BlockFragment> _fragments = new List<BlockFragment>();
        public static readonly string PREFAB_PATH = "Prefabs/UI/Block";

        private Vector3 _originalPosition;
        private RectTransform _rect;
        private Transform _baseTransform;

        public static Block Create(List<Vector3> fragmentPositions, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<Block>();
            script._Initialize(fragmentPositions);
            return script;
        }

        private void _Initialize(List<Vector3> fragmentPositions)
        {
            var parent = transform;
            var center = _GetCenter(fragmentPositions);
            foreach (var fragmentPos in fragmentPositions)
            {
                _fragments.Add(BlockFragment.Create(fragmentPos + center, parent));
            }
            var go = new GameObject("baseGameObject");
            _baseTransform = go.transform;
            _baseTransform.parent = parent;
            _baseTransform.localPosition = center;
            _rect = GetComponent<RectTransform>();
            _originalPosition = _rect.localPosition;
        }

        private Vector3 _GetCenter(List<Vector3> fragmentPositions)
        {
            var x = 0.0f;
            var y = 0.0f;
            foreach (var pos in fragmentPositions)
            {
                x += pos.x;
                y += pos.y;
            }
            var num = fragmentPositions.Count;
            return new Vector3(-x / num, -y / num, 0.0f);
        }

        public void MoveBlockBegin()
        {
            _rect.localScale = ON_DRAG_SCALE;
        }

        public void MoveBlockEnd()
        {
            _rect.localScale = ORIGINAL_SCALE;
            _rect.localPosition = _originalPosition;
        }

        public void MoveBlock(Vector3 diff)
        {
            _rect.position += diff;
        }

        public Vector2 GetPosition()
        {
            return _rect.localPosition;
        }

        public Vector2 GetBasePosition()
        {
            var pos = new Vector2(-1.0f, -1.0f);

            var ray = Camera.main.ScreenPointToRay(_baseTransform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pos.Set(hit.point.x, hit.point.z);
            }
            return pos;
        }


        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
