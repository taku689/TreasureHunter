using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.View.UI;
using TreasureHunter.ViewModel;
using TreasureHunter.Model;

namespace TreasureHunter.Presenter
{
    public class PlayerUIPresenter
    {
        private Player _selfPlayer;
        private Player _enemyPlayer;

        public void Initialize(PlayerViewModel selfPlayerViewModel, PlayerViewModel enemyPlayerViewModel, Transform parent)
        {
            _selfPlayer = Player.Create(selfPlayerViewModel, parent);
            _enemyPlayer = Player.Create(enemyPlayerViewModel, parent);
        }

        public void UpdatePlayer(PlayerViewModel viewModel)
        {
            if (viewModel.IsEnemy)
            {
                _enemyPlayer.UpdateHp(viewModel);
                _enemyPlayer.UpdateBlocks(viewModel);
            }
            else
            {
                _selfPlayer.UpdateHp(viewModel);
                _selfPlayer.UpdateBlocks(viewModel);
            }

        }

        public void UpdateEnableHandle(GameTypes.PlayerType playerType, bool enable)
        {
            if (playerType == GameTypes.PlayerType.SELF)
            {
                _selfPlayer.UpdateHandleBoard(enable);
            }
            else
            {
                _enemyPlayer.UpdateHandleBoard(enable);
            }
        }

    }
}
