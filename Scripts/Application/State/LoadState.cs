using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TreasureHunter.Application
{
    public class LoadState : StateBase
    {
        private MainFsm _mainFsm;
        public LoadState(MainFsm mainFsm) : base(mainFsm)
        {
            _mainFsm = mainFsm;
        }
        public override void InState()
        {
            Locator.PlayerManager.Initialize();
            Locator.InGamePresenter.Initialize();
            Locator.OutGamePresenter.Initialize();
            _mainFsm.ChangeState((int)MainFsm.State.Start);
        }
        public override void Update()
        {
        }
        public override void OutState()
        {
        }
    }
}