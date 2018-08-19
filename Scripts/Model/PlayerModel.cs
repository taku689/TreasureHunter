using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Service;
using System.Linq;

namespace TreasureHunter.Model
{
    public class PlayerModel
    {
        public Dictionary<long, BlockModel> OwnBlockDict { get; private set; } = new Dictionary<long, BlockModel>();
        public GameTypes.PlayerType PlayerType { get; private set; }
        public Dictionary<long, CellPosition> LifeIdToCellPositionDict { get; private set; } = new Dictionary<long, CellPosition>();
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }

        public PlayerModel(GameTypes.PlayerType playerType, List<BlockModel> blockModels)
        {
            PlayerType = playerType;
            foreach (var block in blockModels)
            {
                OwnBlockDict.Add(block.Id, block);
            }
        }

        public bool CanPut()
        {
            foreach (var block in OwnBlockDict.Values)
            {
                if (block.HasEnablePutCell()) return true;
            }
            return false;
        }

        public void SetBlocks(List<BlockModel> blocks)
        {
            foreach (var block in blocks)
            {
                OwnBlockDict.Add(block.Id, block);
            }
        }

        public void UpdateBlockEnablePosition()
        {
            foreach (var block in OwnBlockDict.Values)
            {
                block.UpdateEnableCells();
            }
        }

        public BlockModel PickRandomBlock()
        {
            var blocks = OwnBlockDict.Values.ToList().Where(_ => _.HasEnablePutCell()).ToList();
            var r = new System.Random();
            return blocks[r.Next(blocks.Count)];
        }

        public BlockModel PickBlock(long id)
        {
            return OwnBlockDict[id];
        }

        public void UseBlock(long id)
        {
            OwnBlockDict.Remove(id);
        }

        public void SetTreasures(Dictionary<long, CellPosition> dict)
        {
            LifeIdToCellPositionDict = dict;
            Hp = LifeIdToCellPositionDict.Count;
            MaxHp = Hp;
        }

        public void DecreaseHp()
        {
            Hp--;
        }

        public bool IsDead()
        {
            return Hp <= 0;
        }

        public void TestDebug()
        {
            Debug.Log("*******Test Debug************");
            foreach (var block in OwnBlockDict.Values)
            {
                Debug.Log("======blockType======:" + block.BlockSizeType);
                foreach (var pos in block.EnablePositions)
                {
                    Debug.Log(string.Format("X:{0}, Y:{1}", pos.X, pos.Y));
                }
                Debug.Log("===========end===============");
            }
            Debug.Log("*******Test Debug End************");
        }

    }
}
