using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.Model
{
    public class BlockModel
    {
        public long Id { get; private set; }
        public GameTypes.BlockSizeType BlockSizeType { get; private set; }
        //public List<CellPosition> OccupyCells { get; private set; } = new List<CellPosition>();
        public List<CellPosition> OccupyCells { get; private set; }
        private CellPosition _basePos;
        public List<CellPosition> Geometry { get; private set; }
        //public List<CellPosition> EnablePositions { get; private set; } = new List<CellPosition>();
        public List<CellPosition> EnablePositions { get; private set; }

        public BlockModel(List<CellPosition> geometry, GameTypes.BlockSizeType blockSizeType)
        {
            Id = BlockUniqueId.GetUniqueId();
            BlockSizeType = blockSizeType;
            Geometry = geometry;
            OccupyCells = new List<CellPosition>();
            EnablePositions = new List<CellPosition>();
        }

        public void UpdateEnableCells()
        {
            _UpdateEnablePutCells();
        }

        public void TestDebug()
        {
            Debug.Log("======blockType======:" + BlockSizeType);
            foreach (var pos in EnablePositions)
            {
                Debug.Log(string.Format("X:{0}, Y:{1}", pos.X, pos.Y));
            }
            Debug.Log("===========end===============");
        }

        private void _UpdateEnablePutCells()
        {
            List<CellPosition> enablePutCells = new List<CellPosition>();
            var cellModels = Locator.CellManager.CellModels;
            foreach (var cellModel in cellModels)
            {
                var canPut = true;
                foreach (var cellPos in Geometry)
                {
                    var x = cellModel.Position.X + cellPos.X;
                    var y = cellModel.Position.Y + cellPos.Y;
                    if (!Locator.CellManager.EnablePut(x, y))
                    {
                        canPut = false;
                        break;
                    };
                }
                if (canPut)
                {
                    enablePutCells.Add(cellModel.Position);
                }
            }
            EnablePositions = enablePutCells;
        }

        public bool HasEnablePutCell()
        {
            return EnablePositions.Count > 0;
        }

        public void Put(CellPosition basePos)
        {
            _basePos = basePos;
            foreach (var cellPos in Geometry)
            {
                OccupyCells.Add(new CellPosition(_basePos.X + cellPos.X, _basePos.Y + cellPos.Y));
            }
        }

        public bool CanPut(CellPosition pos)
        {
            foreach (var enablePos in EnablePositions)
            {
                if (pos.X == enablePos.X && pos.Y == enablePos.Y) return true;
            }
            Debug.Log("===========enable positions===============");
            foreach (var p in EnablePositions)
            {
                Debug.Log(string.Format("X:{0}, Y:{1}", p.X, p.Y));
            }
            Debug.Log("===========end===============");

            return false;
        }

        public CellPosition PickRandomEnableCellPosition()
        {
            var enableCells = EnablePositions;
            var r = new System.Random();
            var targetCell = enableCells[r.Next(enableCells.Count)];
            return targetCell;
        }
    }

    public class BlockUniqueId
    {
        private static BlockUniqueId _instance;
        private long _id = 0;

        public static long GetUniqueId()
        {
            if (_instance == null)
            {
                _instance = new BlockUniqueId();
            }
            _instance._IncrementId();
            return _instance._id;
        }

        private void _IncrementId()
        {
            _id++;
        }
    }
}