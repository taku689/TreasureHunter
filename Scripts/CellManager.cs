using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using System;

namespace TreasureHunter
{
    public class CellManager
    {
        public float MIN_POS = -2.5f;
        public float MAX_POS = 2.5f;
        public float CELL_SIZE = 0.5f;
        public float ADJUST_SIZE = 0.25f;

        public const int CELL_MAX_NUM = 11;
        private Dictionary<int, Dictionary<int, CellModel>> _cellDict = new Dictionary<int, Dictionary<int, CellModel>>() { };
        public List<CellModel> CellModels { get; private set; } = new List<CellModel>();

        public CellManager()
        {
            for (var y = 0; y < CELL_MAX_NUM; y++)
            {
                for (var x = 0; x < CELL_MAX_NUM; x++)
                {
                    var cellModel = new CellModel(x, y);
                    if (!_cellDict.ContainsKey(x))
                    {
                        _cellDict[x] = new Dictionary<int, CellModel>();
                    }

                    _cellDict[x][y] = cellModel;
                    CellModels.Add(cellModel);
                }
            }
        }

        public CellModel GetCell(int x, int y)
        {
            if (!_cellDict.ContainsKey(x)) return null;
            if (!_cellDict[x].ContainsKey(y)) return null;
            return _cellDict[x][y];
        }

        public bool EnablePut(int x, int y)
        {
            var cell = GetCell(x, y);
            if (cell == null) return false;
            return !cell.IsOccupied;
        }

        public List<CellModel> Occupy(List<CellPosition> cellPositions)
        {
            var cells = new List<CellModel>();
            foreach (var cellPos in cellPositions)
            {
                var cell = GetCell(cellPos.X, cellPos.Y);
                if (cell == null)
                {
                    throw new Exception(string.Format("cant occupy. X:{0}, Y:{1}", cellPos.X, cellPos.Y));
                }
                cell.Occupy();
                cells.Add(cell);
            }
            return cells;
        }

        public void SetTreasure(GameTypes.PlayerType playerType, long id, CellPosition pos)
        {
            var cellModel = GetCell(pos.X, pos.Y);
            cellModel.PutTreasure(playerType, id);
        }

        public void SetTreasures(GameTypes.PlayerType playerType, Dictionary<long, CellPosition> dict)
        {
            foreach (var pos in dict)
            {
                SetTreasure(playerType, pos.Key, pos.Value);
            }
        }

        public CellModel GetCellModelByPosition(Vector2 position)
        {
            var x = position.x;
            var y = position.y;
            if (!_IsValidPosition(x)) return null;
            if (!_IsValidPosition(y)) return null;
            var cellNumX = _GetCellNumber(x);
            var cellNumY = _GetCellNumber(y);
            return GetCell(cellNumX, cellNumY);
        }

        private bool _IsValidPosition(float num)
        {
            if (num <= MIN_POS - ADJUST_SIZE || num >= MAX_POS + ADJUST_SIZE) return false;
            return true;
        }

        public Vector3 CellPositionToWorldPosition(CellPosition cellPos)
        {
            return new Vector3(((float)(cellPos.X - 5)) / 2.0f, 0.5f, ((float)(cellPos.Y - 5)) / 2.0f);
        }

        private int _GetCellNumber(float num)
        {
            var _num = num + MAX_POS;
            var cellNum = -1;
            for (var i = 0; i < CELL_MAX_NUM; i++)
            {
                if (_num > (CELL_SIZE * i) - ADJUST_SIZE && _num < (CELL_SIZE * (i + 1)) - ADJUST_SIZE)
                {
                    cellNum = i;
                    break;
                }
            }
            return cellNum;
        }
    }
}
