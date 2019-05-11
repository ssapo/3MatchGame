public class StateMachine<T> where T : class
{
	public T Owner { private set; get; }
	public IState<T> CurrState { set; get; }
	public IState<T> PrevState { set; get; }

	public StateMachine(T _owner)
	{
		this.Owner = _owner;
	}

	public void Transition(IState<T> _state)
	{
		PrevState = CurrState;

		PrevState?.OnExit(Owner);

		CurrState = _state;

		CurrState?.OnEntry(Owner);
	}

	public void Routine() => CurrState?.OnRoutine(Owner);
}
