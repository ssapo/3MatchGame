public interface IState<T> where T : class
{
	void OnEntry(T owner);
	void OnExit(T owner);
	void OnRoutine(T owner);
}
