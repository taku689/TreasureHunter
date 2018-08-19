using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using TreasureHunter.ViewModel;
using TreasureHunter.View.UI;

namespace TreasureHunter.Presenter
{
    public class OutGamePresenter
    {
        private UIManager _uiManager;
        public PlayerUIPresenter PlayerUIPresenter { get; private set; }
        public BoardUIPresenter BoardUIPresenter { get; private set; }

        public OutGamePresenter(UIManager uiManager)
        {
            _uiManager = uiManager;
            PlayerUIPresenter = new PlayerUIPresenter();
            BoardUIPresenter = new BoardUIPresenter();
        }

        public void Initialize()
        {
            var selfPlayer = Locator.PlayerManager.GetPlayer(GameTypes.PlayerType.SELF);
            var selfPlayerViewModel = new PlayerViewModel(selfPlayer);
            var enemyPlayer = Locator.PlayerManager.GetPlayer(GameTypes.PlayerType.ENEMY);
            var enemyPlayerViewModel = new PlayerViewModel(enemyPlayer);
            PlayerUIPresenter.Initialize(selfPlayerViewModel, enemyPlayerViewModel, _uiManager.Players);
            BoardUIPresenter.Initialize(selfPlayerViewModel, _uiManager.Lifes);
            BoardUIPresenter.Initialize(enemyPlayerViewModel, _uiManager.Lifes);
        }

        public void DisplayResult()
        {
            var viewModel = new ResultViewModel(
                Locator.GameManager.ToTile,
                Locator.GameManager.ToRetry,
                Locator.PlayerManager.GetResult()
            );
            var result = Result.Create(viewModel, _uiManager.Root);
        }
    }
}
