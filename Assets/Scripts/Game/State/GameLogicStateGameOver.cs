using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicStateGameOver : IState<GameLogicManager>
{
	public void OnEntry(GameLogicManager _owner)
	{
		if (_owner.PresentationPlayer.GetClip("GameOver"))
		{
			_owner.PresentationPlayer.Stop("GameOver");
			_owner.PresentationPlayer.CrossFade("GameOver");
		}
	}

	public void OnExit(GameLogicManager _owner)
	{
	}

	public void OnRoutine(GameLogicManager _owner)
	{
		if (_owner.PresentationPlayer.IsPlaying("GameOver"))
		{
		}
		else
		{
			// 씬 재시작 , 무한 루프
			SceneManager.LoadScene("Game");
		}
	}
}