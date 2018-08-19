using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using TreasureHunter.Service;

namespace TreasureHunter
{
    public class TurnManager
    {
        public bool IsSelfTurn { get; private set; }
        public bool IsEnemyTurn { get; private set; }

        public void FinishTurn(GameTypes.PlayerType playerType)
        {
            if (playerType == GameTypes.PlayerType.SELF)
            {
                SetTurn(GameTypes.PlayerType.ENEMY);
            }
            else if (playerType == GameTypes.PlayerType.ENEMY)
            {
                SetTurn(GameTypes.PlayerType.SELF);
            }
        }

        public void SetTurn(GameTypes.PlayerType playerType)
        {
            var b = playerType == GameTypes.PlayerType.SELF;
            IsSelfTurn = b;
            IsEnemyTurn = !b;
            Locator.OutGamePresenter.PlayerUIPresenter.UpdateEnableHandle(GameTypes.PlayerType.SELF, IsSelfTurn);
            Locator.OutGamePresenter.PlayerUIPresenter.UpdateEnableHandle(GameTypes.PlayerType.ENEMY, IsEnemyTurn);
            var p = Locator.PlayerManager.GetPlayer(GameTypes.PlayerType.SELF);
        }

    }
}