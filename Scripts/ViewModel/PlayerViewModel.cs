using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using System.Linq;
using UnityEngine.UI;
using TreasureHunter.Service;

namespace TreasureHunter.ViewModel
{
    public class PlayerViewModel
    {
        public const float UI_BLOCK_SIZE = 25;
        public const float UI_BLOCK_GAP = 3;
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }
        public Dictionary<long, Vector3> LifeIdToPosDict { get; private set; }
        public bool IsEnemy { get; private set; }
        public long SmallBlockId { get; private set; }
        public long MediumBlockId { get; private set; }
        public long LargeBlockId { get; private set; }
        public List<Vector3> SmallBlockPositions { get; private set; }
        public List<Vector3> MediumBlockPositions { get; private set; }
        public List<Vector3> LargeBlockPositions { get; private set; }

        public PlayerViewModel(PlayerModel playerModel)
        {
            Hp = playerModel.Hp;
            MaxHp = playerModel.MaxHp;
            IsEnemy = playerModel.PlayerType == GameTypes.PlayerType.ENEMY;
            _SetLifeInfo(playerModel);
            _SetBlocksInfo(playerModel);
        }

        private void _SetBlocksInfo(PlayerModel playerModel)
        {
            foreach (var block in playerModel.OwnBlockDict.Values)
            {
                _SetBlockInfo(block);
            }
        }
        private void _SetLifeInfo(PlayerModel playerModel)
        {
            var dict = playerModel.LifeIdToCellPositionDict;
            var lifeIdToPosDict = new Dictionary<long, Vector3>();
            foreach (var val in dict)
            {
                lifeIdToPosDict.Add(val.Key, Locator.CellManager.CellPositionToWorldPosition(val.Value));
            }
            LifeIdToPosDict = lifeIdToPosDict;
        }

        private void _SetBlockInfo(BlockModel block)
        {
            var geometry = block.Geometry;
            var positions = new List<Vector3>();
            foreach (var cellPos in geometry)
            {
                var x = cellPos.X * UI_BLOCK_SIZE + cellPos.X * UI_BLOCK_GAP;
                var y = cellPos.Y * UI_BLOCK_SIZE + cellPos.Y * UI_BLOCK_GAP;
                positions.Add(new Vector3(x, y, 0));
            }

            if (block.BlockSizeType == GameTypes.BlockSizeType.Small)
            {
                SmallBlockId = block.Id;
                SmallBlockPositions = positions;
            }
            else if (block.BlockSizeType == GameTypes.BlockSizeType.Medium)
            {
                MediumBlockId = block.Id;
                MediumBlockPositions = positions;
            }
            else if (block.BlockSizeType == GameTypes.BlockSizeType.Large)
            {
                LargeBlockId = block.Id;
                LargeBlockPositions = positions;
            }
        }

        public bool OnBlockDragEnd(Vector2 position, long blockId)
        {
            var cellModel = Locator.CellManager.GetCellModelByPosition(position);
            if (IsEnemy)
            {
                return BlockHandler.PutBlock(GameTypes.PlayerType.ENEMY, blockId, cellModel);
            }
            else
            {
                return BlockHandler.PutBlock(GameTypes.PlayerType.SELF, blockId, cellModel);
            }
        }

    }
}
