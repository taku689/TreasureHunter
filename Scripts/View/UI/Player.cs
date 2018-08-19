using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.ViewModel;

namespace TreasureHunter.View.UI
{
    public class Player : MonoBehaviour
    {
        public static readonly string PREFAB_PATH = "Prefabs/UI/Player";

        private const float ENEMY_Y_POS = 1165;

        [SerializeField] private Hp _hp;
        [SerializeField] private BlockButton _blockLButton;
        [SerializeField] private BlockButton _blockMButton;
        [SerializeField] private BlockButton _blockSButton;
        [SerializeField] private GameObject _handleBoard;

        public static Player Create(PlayerViewModel viewModel, Transform parent)
        {
            var go = Instantiate(Resources.Load(PREFAB_PATH), parent) as GameObject;
            var script = go.GetComponent<Player>();
            script._Initialize(viewModel);
            return script;
        }

        private void _Initialize(PlayerViewModel viewModel)
        {
            _blockLButton.Initialize(viewModel.IsEnemy);
            _blockMButton.Initialize(viewModel.IsEnemy);
            _blockSButton.Initialize(viewModel.IsEnemy);
            _hp.Initialize(viewModel);
            if (viewModel.IsEnemy)
            {
                var rect = GetComponent<RectTransform>();
                var pos = rect.localPosition;
                rect.localPosition = new Vector3(pos.x, ENEMY_Y_POS, pos.z);
            }
            UpdateBlocks(viewModel);
        }

        public void UpdateHp(PlayerViewModel viewModel)
        {
            _hp.UpdateHp(viewModel.Hp);
        }

        public void UpdateBlocks(PlayerViewModel viewModel)
        {
            _blockLButton.UpdateBlock(
                viewModel.LargeBlockPositions, viewModel.LargeBlockId, viewModel.OnBlockDragEnd);
            _blockMButton.UpdateBlock(
                viewModel.MediumBlockPositions, viewModel.MediumBlockId, viewModel.OnBlockDragEnd);
            _blockSButton.UpdateBlock(
                viewModel.SmallBlockPositions, viewModel.SmallBlockId, viewModel.OnBlockDragEnd);
        }

        public void UpdateHandleBoard(bool enable)
        {
            _handleBoard.SetActive(!enable);
        }
    }
}
