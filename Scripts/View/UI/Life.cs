using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunter.View.UI
{
    public class Life : MonoBehaviour
    {
        public static readonly string PREFAB_PATH = "Prefabs/UI/Life";

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _selfSprite;
        [SerializeField] private Sprite _enemySprite;

        public static Life Create(Vector3 pos, bool isEnemy, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<Life>();
            script._Initialize(pos, isEnemy);
            return script;
        }

        private void _Initialize(Vector3 pos, bool isEnemy)
        {
            transform.localPosition = pos;
            var rect = GetComponent<RectTransform>();
            rect.position = new Vector3(pos.x, pos.y, 0.0f);
            if (isEnemy)
            {
                _image.sprite = _enemySprite;
            }
            else
            {
                _image.sprite = _selfSprite;
            }
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}