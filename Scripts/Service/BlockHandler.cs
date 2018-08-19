using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;

namespace TreasureHunter.Service
{
    public class BlockHandler
    {

        public static bool PutBlock(GameTypes.PlayerType playerType, long id, CellModel cellModel)
        {
            if (cellModel == null) return false;
            var player = Locator.PlayerManager.GetPlayer(playerType);
            var block = player.PickBlock(id);
            if (!block.CanPut(cellModel.Position)) return false;
            PutBlock(player, block, cellModel.Position);
            return true;
        }

        public static void PutBlock(PlayerModel player, BlockModel block, CellPosition targetCell)
        {
            block.Put(targetCell);
            player.UseBlock(block.Id);
            var occupyCells = block.OccupyCells;
            var cellModels = Locator.CellManager.Occupy(occupyCells);
            var selfPlayer = Locator.PlayerManager.GetPlayer(GameTypes.PlayerType.SELF);
            var enemyPlayer = Locator.PlayerManager.GetPlayer(GameTypes.PlayerType.ENEMY);
            Locator.InGamePresenter.BlockPresenter.PutBlock(player.PlayerType, block);
            player.SetBlocks(new List<BlockModel>() { BlockCreator.GetRandomBlock(block.BlockSizeType) });
            UpdatePlayerBlocks();
            foreach (var cellModel in cellModels)
            {
                if (cellModel.HasSelfTreasure)
                {
                    selfPlayer.DecreaseHp();
                    Locator.OutGamePresenter.BoardUIPresenter.DisappearLife(cellModel.SelfTreasureId);
                }

                if (cellModel.HasEnemyTreasure)
                {
                    enemyPlayer.DecreaseHp();
                    Locator.OutGamePresenter.BoardUIPresenter.DisappearLife(cellModel.EnemyTreasureId);
                }
            }
            {
                var viewModel = new ViewModel.PlayerViewModel(selfPlayer);
                Locator.OutGamePresenter.PlayerUIPresenter.UpdatePlayer(viewModel);
            }
            {
                var viewModel = new ViewModel.PlayerViewModel(enemyPlayer);
                Locator.OutGamePresenter.PlayerUIPresenter.UpdatePlayer(viewModel);
            }
            Locator.TurnManager.FinishTurn(player.PlayerType);
        }

        public static void UpdatePlayerBlocks()
        {
            foreach (var player in Locator.PlayerManager.GetPlayers())
            {
                player.UpdateBlockEnablePosition();
            }
        }
    }
}
