using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TreasureHunter.ViewModel;

namespace TreasureHunter.View.UI
{
    public class Hp : MonoBehaviour
    {

        [SerializeField] Text text;

        private const string HP_FORMAT = "{0} / {1}";
        private const float ENEMY_Y_POS = -328;

        private int _maxHp;
        private int _hp;

        public void Initialize(PlayerViewModel viewModel)
        {
            _maxHp = viewModel.MaxHp;
            _hp = viewModel.Hp;
            if (viewModel.IsEnemy)
            {
                var rect = GetComponent<RectTransform>();
                var pos = rect.localPosition;
                rect.localPosition = new Vector3(pos.x, ENEMY_Y_POS, pos.z);
            }
            _UpdateHp();
        }

        public void UpdateHp(int hp)
        {
            _hp = hp;
            _UpdateHp();

        }

        private void _UpdateHp()
        {
            text.text = string.Format(HP_FORMAT, _hp, _maxHp);
        }

    }
}