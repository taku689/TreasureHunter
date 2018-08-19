using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: プッシュダウンオートマトンの実装が必要そう
public class FsmBase
{

    protected StateBase currentState;
    protected StateBase nextState;

    // memo: 子クラスで下記のようなenumを実装する
    // public enum State {
    //      Astate = 1,
    //      Bstate = 2,
    //      ...
    // }

    protected Dictionary<int, StateBase> states = new Dictionary<int, StateBase>();

    public virtual void ChangeState(int stateId)
    {
        nextState = states[stateId];
        if (currentState != null)
        {
            currentState.OutState();
        }
    }

    protected void AddState(StateBase state, int stateId)
    {
        int stateNum = states.Count;
        states.Add(stateNum + 1, state);
    }

    public void Update()
    {
        if (nextState != null)
        {
            currentState = nextState;
            nextState = null;
            currentState.InState();
        }
        currentState.Update();
    }

}