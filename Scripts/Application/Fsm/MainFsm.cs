using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunter.Application
{
    public class MainFsm : FsmBase
    {

        public enum State
        {
            Load = 1,
            LifeSetup,
            Start,
            Main,
            End,
        }

        public MainFsm()
        {
            AddState(new LoadState(this), (int)State.Load);
            AddState(new LifeSetupState(this), (int)State.LifeSetup);
            AddState(new StartState(this), (int)State.Start);
            AddState(new MainState(this), (int)State.Main);
            AddState(new EndState(this), (int)State.End);
            ChangeState((int)State.Load);
        }

    }
}
