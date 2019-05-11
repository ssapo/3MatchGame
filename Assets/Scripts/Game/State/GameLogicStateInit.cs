public class GameLogicStateInit : IState<GameLogicManager>
{
	public void OnEntry(GameLogicManager _owner)
	{
		_owner.Initialize();

		if (_owner.IsTutorial)
		{
			_owner.FSM.Transition(new GameLogicStateTutorialStart());

		}
		else
		{
			_owner.FSM.Transition(new GameLogicStateGameStart());
		}
	}

	public void OnExit(GameLogicManager _owner)
	{
	}

	public void OnRoutine(GameLogicManager _owner)
	{
	}
}