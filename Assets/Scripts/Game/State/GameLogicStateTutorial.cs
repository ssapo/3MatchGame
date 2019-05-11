using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicStateTutorial : IState<GameLogicManager>
{
	public void OnEntry(GameLogicManager _owner)
	{
	}

	public void OnExit(GameLogicManager _owner)
	{
	}

	public void OnRoutine(GameLogicManager _owner)
	{
		if (_owner.IsGameOver)
		{
			_owner.TutorialOver();
			SceneManager.LoadScene("Game");
		}
	}
}