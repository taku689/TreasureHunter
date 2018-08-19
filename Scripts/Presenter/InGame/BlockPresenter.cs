using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using TreasureHunter.View;

namespace TreasureHunter.Presenter
{
    public class BlockPresenter
    {
        private const float BLOCK_POS_Y = -1.0f;
        private Transform _blockTransform;

        public void Initialze(Transform blockTransform)
        {
            _blockTransform = blockTransform;
        }

        public void PutBlock(GameTypes.PlayerType playerType, BlockModel blockModel)
        {
            var positions = new List<Vector3>();
            var occupiedCells = blockModel.OccupyCells;
            foreach (var cell in occupiedCells)
            {
                positions.Add(new Vector3(cell.X, BLOCK_POS_Y, cell.Y));
            }
            var materialType = PlayerTypeToMaterialType(playerType);
            Block.Create(materialType, positions, _blockTransform);
        }

        public Block.MaterialType PlayerTypeToMaterialType(GameTypes.PlayerType playerType)
        {
            var materialType = playerType == GameTypes.PlayerType.SELF ?
                Block.MaterialType.RED : Block.MaterialType.BLUE;
            return materialType;
        }

    }
}
