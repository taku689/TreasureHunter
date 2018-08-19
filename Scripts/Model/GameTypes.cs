using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.Model
{
    public class GameTypes
    {
        public enum PlayerType
        {
            SELF,
            ENEMY,
        }

        public enum ResultType
        {
            SELF_WIN,
            ENEMY_WIN,
            DRAW,
        }

        public enum BlockSizeType
        {
            Large,
            Medium,
            Small,
        }
    }
}
