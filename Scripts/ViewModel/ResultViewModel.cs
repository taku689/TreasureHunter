using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TreasureHunter.Model;
using UnityEngine.Events;

namespace TreasureHunter.ViewModel
{
    public class ResultViewModel
    {
        const string WIN_TEXT = "WIN！！";
        const string LOSE_TEXT = "LOSE...";
        const string DRAW_TEXT = "DRAW";
        public UnityAction ToTitle { get; private set; }
        public UnityAction ToRetry { get; private set; }
        public string ResultText { get; private set; }

        public ResultViewModel(UnityAction toTitle, UnityAction toRetry, GameTypes.ResultType resultType)
        {
            ToTitle = toTitle;
            ToRetry = toRetry;
            _SetResult(resultType);
        }

        private void _SetResult(GameTypes.ResultType resultType)
        {
            switch (resultType)
            {
                case GameTypes.ResultType.SELF_WIN:
                    ResultText = WIN_TEXT;
                    break;
                case GameTypes.ResultType.ENEMY_WIN:
                    ResultText = LOSE_TEXT;
                    break;
                default:
                    ResultText = DRAW_TEXT;
                    break;
            }
        }
    }
}
