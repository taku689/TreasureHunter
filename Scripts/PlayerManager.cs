using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using TreasureHunter.Service;
using System.Linq;
using TreasureHunter.Util;

namespace TreasureHunter
{
    public class PlayerManager
    {
        private const int FIRST_BLOCK_NUM = 3;
        private const int TREASURE_NUM = 14;
        private Dictionary<GameTypes.PlayerType, PlayerModel> _playerModelDict
            = new Dictionary<GameTypes.PlayerType, PlayerModel>();
        public void Initialize()
        {
            var lifes = _LotLifePositions();
            var selfLifes = new Dictionary<long, CellPosition>();
            foreach (var life in lifes)
            {
                selfLifes.Add(life.Key, life.Value);
                if (selfLifes.Count == TREASURE_NUM / 2)
                {
                    break;
                }
            }
            foreach (var life in selfLifes)
            {
                lifes.Remove(life.Key);
            }
            _Initialize(GameTypes.PlayerType.SELF, selfLifes);
            _Initialize(GameTypes.PlayerType.ENEMY, lifes);
            var self = _playerModelDict[GameTypes.PlayerType.SELF];
            var enemy = _playerModelDict[GameTypes.PlayerType.ENEMY];
            UpdatePlayerBlocks();
        }

        private void _Initialize(GameTypes.PlayerType playerType, Dictionary<long, CellPosition> lifes)
        {
            var playerModel = new PlayerModel(playerType, _GetInitialBlocks());
            _playerModelDict[playerType] = playerModel;
            var player = GetPlayer(playerType);
            Locator.CellManager.SetTreasures(playerType, lifes);
            player.SetTreasures(lifes);
        }

        public void Update()
        {
            if (Locator.TurnManager.IsSelfTurn)
            {
                var self = _playerModelDict[GameTypes.PlayerType.SELF];

                if (!self.CanPut())
                {
                    Locator.TurnManager.FinishTurn(GameTypes.PlayerType.SELF);
                }
                else
                {
                    // isAI
                    if (false)
                    {
                        var block = self.PickRandomBlock();
                        var cellPos = block.PickRandomEnableCellPosition();
                        BlockHandler.PutBlock(GameTypes.PlayerType.SELF, block.Id, Locator.CellManager.GetCell(cellPos.X, cellPos.Y));
                    }
                }
            }
            if (Locator.TurnManager.IsEnemyTurn)
            {
                var enemy = _playerModelDict[GameTypes.PlayerType.ENEMY];
                if (!enemy.CanPut())
                {
                    Locator.TurnManager.FinishTurn(enemy.PlayerType);
                }
                else
                {
                    // isAI
                    if (false)
                    {
                        var block = enemy.PickRandomBlock();
                        var cellPos = block.PickRandomEnableCellPosition();
                        BlockHandler.PutBlock(GameTypes.PlayerType.ENEMY, block.Id, Locator.CellManager.GetCell(cellPos.X, cellPos.Y));
                    }
                }
            }
        }

        private List<BlockModel> _GetInitialBlocks()
        {
            var blocks = new List<BlockModel>();
            var smallBlock = BlockCreator.GetRandomBlock(GameTypes.BlockSizeType.Small);
            var mediumBlock = BlockCreator.GetRandomBlock(GameTypes.BlockSizeType.Medium);
            var largeBlock = BlockCreator.GetRandomBlock(GameTypes.BlockSizeType.Large);
            blocks.Add(smallBlock);
            blocks.Add(mediumBlock);
            blocks.Add(largeBlock);
            return blocks;
        }

        public void UpdatePlayerBlocks()
        {
            foreach (var player in _playerModelDict.Values)
            {
                var blocks = player.OwnBlockDict.Values;
                foreach (var block in blocks)
                {
                    block.UpdateEnableCells();
                }
            }
        }

        public List<PlayerModel> GetPlayers()
        {
            return _playerModelDict.Values.ToList();
        }

        public PlayerModel GetPlayer(GameTypes.PlayerType playerType)
        {
            return _playerModelDict[playerType];
        }

        public void RandomSetTreasure(GameTypes.PlayerType playerType)
        {
            var player = GetPlayer(playerType);
            int setNum = 0;
            var idToCellPositionDict = new Dictionary<long, CellPosition>();
            while (setNum < TREASURE_NUM)
            {
                var r = new System.Random();
                var x = r.Next(CellManager.CELL_MAX_NUM);
                r = new System.Random();
                var y = r.Next(CellManager.CELL_MAX_NUM);
                var canSet = true;
                foreach (var cellPos in idToCellPositionDict.Values)
                {
                    if (cellPos.X == x && cellPos.Y == y)
                    {
                        canSet = false;
                        break;
                    }
                }
                if (!canSet) continue;

                idToCellPositionDict.Add(LifeUniqueId.GetUniqueId(), new CellPosition(x, y));
                setNum++;
            }

            player.SetTreasures(idToCellPositionDict);
            Locator.CellManager.SetTreasures(playerType, idToCellPositionDict);
        }

        private Dictionary<long, CellPosition> _LotLifePositions()
        {
            int setNum = 0;
            var idToCellPositionDict = new Dictionary<long, CellPosition>();
            while (setNum < TREASURE_NUM)
            {
                var x = UnityEngine.Random.Range(0, CellManager.CELL_MAX_NUM);
                var y = UnityEngine.Random.Range(0, CellManager.CELL_MAX_NUM);
                var canSet = true;
                foreach (var cellPos in idToCellPositionDict.Values)
                {
                    if (cellPos.X == x && cellPos.Y == y)
                    {
                        canSet = false;
                        break;
                    }
                }
                if (!canSet) continue;

                idToCellPositionDict.Add(LifeUniqueId.GetUniqueId(), new CellPosition(x, y));
                setNum++;
            }
            return idToCellPositionDict;
        }

        public GameTypes.ResultType GetResult()
        {
            var self = GetPlayer(GameTypes.PlayerType.SELF);
            var enemy = GetPlayer(GameTypes.PlayerType.ENEMY);
            if (self.Hp > enemy.Hp)
            {
                return GameTypes.ResultType.SELF_WIN;
            }
            if (self.Hp < enemy.Hp)
            {
                return GameTypes.ResultType.ENEMY_WIN;
            }
            return GameTypes.ResultType.DRAW;
        }

        public class LifeUniqueId
        {
            private static LifeUniqueId _instance;
            private long _id = 0;

            public static long GetUniqueId()
            {
                if (_instance == null)
                {
                    _instance = new LifeUniqueId();
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
}
