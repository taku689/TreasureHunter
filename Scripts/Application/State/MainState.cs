using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TreasureHunter.Application
{
    public class MainState : StateBase
    {
        private MainFsm _mainFsm;
        public MainState(MainFsm mainFsm) : base(mainFsm)
        {
            _mainFsm = mainFsm;
        }
        public override void InState()
        {
        }
        public override void Update()
        {
            if (_IsGameEnd())
            {
                _mainFsm.ChangeState((int)MainFsm.State.End);
            }
            Locator.PlayerManager.Update();
        }

        private bool _IsGameEnd()
        {
            var players = Locator.PlayerManager.GetPlayers();
            var canContiue = false;
            foreach (var player in players)
            {
                if (player.IsDead()) return true;
                canContiue = canContiue || player.CanPut();
            }
            return !canContiue;
        }
        public override void OutState()
        {
        }
    }
}