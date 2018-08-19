public class StateBase
{
    protected FsmBase _fsm;

    public StateBase(FsmBase fsm)
    {
        _fsm = fsm;
    }

    public virtual void InState()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OutState()
    {
    }
}