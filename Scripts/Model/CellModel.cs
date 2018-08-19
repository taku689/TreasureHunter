using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.Model
{
    public class CellPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public CellPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Add(int x, int y)
        {
            X += x;
            Y += y;
        }
    }

    public class CellModel
    {
        public CellPosition Position { get; private set; }
        public bool HasSelfTreasure { get; private set; } = false;
        public long SelfTreasureId { get; private set; }
        public bool HasEnemyTreasure { get; private set; } = false;
        public long EnemyTreasureId { get; private set; }
        public bool IsOccupied { get; private set; } = false;

        public CellModel(int x, int y)
        {
            Position = new CellPosition(x, y);
        }

        public void Occupy()
        {
            IsOccupied = true;
        }

        public void PutTreasure(GameTypes.PlayerType playerType, long id)
        {
            if (playerType == GameTypes.PlayerType.SELF)
            {
                HasSelfTreasure = true;
                SelfTreasureId = id;
            }
            if (playerType == GameTypes.PlayerType.ENEMY)
            {
                HasEnemyTreasure = true;
                EnemyTreasureId = id;
            }
        }
    }
}