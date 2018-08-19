using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TreasureHunter.Application
{
    public class LifeSetupState : StateBase
    {
        private MainFsm _mainFsm;
        public LifeSetupState(MainFsm mainFsm) : base(mainFsm)
        {
            _mainFsm = mainFsm;
        }
        public override void InState()
        {
        }
        public override void Update()
        {
        }
        public override void OutState()
        {
        }
    }
}