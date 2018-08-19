using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TreasureHunter.Model;

namespace TreasureHunter.Application
{
    public class StartState : StateBase
    {
        private MainFsm _mainFsm;
        public StartState(MainFsm mainFsm) : base(mainFsm)
        {
            _mainFsm = mainFsm;
        }
        public override void InState()
        {
            Locator.TurnManager.SetTurn(GameTypes.PlayerType.SELF);
            _mainFsm.ChangeState((int)MainFsm.State.Main);
        }
        public override void Update()
        {
        }
        public override void OutState()
        {
        }
    }
}