using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureHunter.Model;
using System;
using System.Linq;

namespace TreasureHunter.Service
{
    public class BlockCreator
    {
        public enum BlockType
        {
            ShortTBlock = 1,
            LongTBlock,
            ShortLBlock,
            LongLBlock,
            OneFragmentBlock,
            TwoSquareBlock,
            HorizontalTwoFragmentBlock,
            HorizontalThreeFragmentBlock,
            VerticalTwoFragmentBlock,
            VerticalThreeFragmentBlock,
        }

        public enum SmallBlockType
        {
            LBlock,
            IBlock,
        }

        public enum MediumBlockType
        {
            TBlock,
            LBlock,
            IBlock,
        }

        public enum LargeBlockType
        {
            TBlock,
            LBlock,
            IBlock,
        }

        public static Dictionary<SmallBlockType, List<CellPosition>> smallBlockGeomertyDict = new Dictionary<SmallBlockType, List<CellPosition>>()
        {
            {SmallBlockType.LBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(1, 0),
                new CellPosition(0, 1),
            }},
            {SmallBlockType.IBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
            }},
        };

        public static Dictionary<MediumBlockType, List<CellPosition>> mediumBlockGeomertyDict = new Dictionary<MediumBlockType, List<CellPosition>>()
        {
            {MediumBlockType.TBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
                new CellPosition(1, 1),
                new CellPosition(2, 1),
            }},
            {MediumBlockType.LBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
                new CellPosition(1, 0),
                new CellPosition(2, 0),
            }},
            {MediumBlockType.IBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
                new CellPosition(0, 3),
                new CellPosition(0, 4),
            }},

        };

        public static Dictionary<LargeBlockType, List<CellPosition>> largeBlockGeomertyDict = new Dictionary<LargeBlockType, List<CellPosition>>()
        {
            {LargeBlockType.TBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
                new CellPosition(0, 3),
                new CellPosition(0, 4),
                new CellPosition(1, 2),
                new CellPosition(2, 2),
            }},
            {LargeBlockType.LBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
                new CellPosition(0, 3),
                new CellPosition(1, 0),
                new CellPosition(2, 0),
                new CellPosition(3, 0),
            }},
            {LargeBlockType.IBlock, new List<CellPosition>(){
                new CellPosition(0, 0),
                new CellPosition(0, 1),
                new CellPosition(0, 2),
                new CellPosition(0, 3),
                new CellPosition(0, 4),
                new CellPosition(0, 5),
                new CellPosition(0, 6),
            }},

        };

        public static BlockModel GetRandomBlock(GameTypes.BlockSizeType sizeType)
        {
            List<CellPosition> geometry = new List<CellPosition>();
            if (sizeType == GameTypes.BlockSizeType.Small)
            {
                var blockNum = Enum.GetValues(typeof(SmallBlockType)).Length;
                var vals = Enum.GetValues(typeof(SmallBlockType)).OfType<SmallBlockType>().ToList();
                var r = new System.Random();
                var blockType = vals[r.Next(vals.Count)];
                geometry = smallBlockGeomertyDict[blockType];
            }

            if (sizeType == GameTypes.BlockSizeType.Medium)
            {
                var blockNum = Enum.GetValues(typeof(MediumBlockType)).Length;
                var vals = Enum.GetValues(typeof(MediumBlockType)).OfType<MediumBlockType>().ToList();
                var r = new System.Random();
                var blockType = vals[r.Next(vals.Count)];
                geometry = mediumBlockGeomertyDict[blockType];
            }

            if (sizeType == GameTypes.BlockSizeType.Large)
            {
                var blockNum = Enum.GetValues(typeof(LargeBlockType)).Length;
                var vals = Enum.GetValues(typeof(LargeBlockType)).OfType<LargeBlockType>().ToList();
                var r = new System.Random();
                var blockType = vals[r.Next(vals.Count)];
                geometry = largeBlockGeomertyDict[blockType];
            }
            geometry = _RandomRotateAndReverse(geometry);

            return new BlockModel(geometry, sizeType);
        }

        private static List<List<int>> _rotateList = new List<List<int>>()
        {
            new List<int>(){1, 0, 0, 1},
            new List<int>(){0, -1, 1, 0},
            new List<int>(){-1, 0, 0, -1},
            new List<int>(){0, 1, -1, 0},
        };

        private static List<List<int>> _reverseList = new List<List<int>>()
        {
            new List<int>(){1, 1},
            new List<int>(){-1, 1},
            new List<int>(){1, -1},
        };

        private static List<CellPosition> _RandomRotateAndReverse(List<CellPosition> geometry)
        {
            var _rotateGeometry = new List<CellPosition>();

            // 回転
            var r = new System.Random();
            var rotateParam = _rotateList[r.Next(_rotateList.Count)];
            foreach (var cellPos in geometry)
            {
                var x = cellPos.X * rotateParam[0] + cellPos.Y * rotateParam[1];
                var y = cellPos.X * rotateParam[2] + cellPos.Y * rotateParam[3];
                _rotateGeometry.Add(new CellPosition(x, y));
            }

            var _geometry = new List<CellPosition>();

            // 反転
            var reverseParam = _reverseList[r.Next(_reverseList.Count)];
            foreach (var cellPos in _rotateGeometry)
            {
                var x = cellPos.X * reverseParam[0];
                var y = cellPos.Y * reverseParam[1];
                _geometry.Add(new CellPosition(x, y));
            }

            CellPosition basePosition;
            int maxX = 0;
            int maxY = 0;
            foreach (var cellPos in _geometry)
            {
                if (Math.Abs(cellPos.X) > maxX)
                {
                    maxX = cellPos.X;
                }
                if (Math.Abs(cellPos.Y) > maxY)
                {
                    maxY = cellPos.Y;
                }
            }
            if (maxX < 0)
            {
                var x = Math.Abs(maxX);
                foreach (var cellPos in _geometry)
                {
                    cellPos.Add(x, 0);
                }
            }
            if (maxY < 0)
            {
                var y = Math.Abs(maxY);
                foreach (var cellPos in _geometry)
                {
                    cellPos.Add(0, y);
                }
            }
            return _geometry;
        }
    }
}