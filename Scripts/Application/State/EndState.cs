using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TreasureHunter.Application
{
    public class EndState : StateBase
    {
        private MainFsm _mainFsm;
        public EndState(MainFsm mainFsm) : base(mainFsm)
        {
            _mainFsm = mainFsm;
        }
        public override void InState()
        {
            Locator.OutGamePresenter.DisplayResult();
        }
        public override void Update()
        {
        }
        public override void OutState()
        {
        }
    }
}