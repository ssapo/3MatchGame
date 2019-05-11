using UnityEngine;

public class GameLogicStateGameStart : IState<GameLogicManager>
{
	public void OnEntry(GameLogicManager _owner)
	{
		// 게임이 시작되기전 애니메이션 연출
		if (_owner.PresentationPlayer.GetClip("GameStart"))
		{ 
			_owner.PresentationPlayer.Stop("GameStart");
			_owner.PresentationPlayer.CrossFade("GameStart");
		}
	}

	public void OnExit(GameLogicManager _owner)
	{
	}

	public void OnRoutine(GameLogicManager _owner)
	{
		// 연출 애니메이션이 끝나면 다음 로직 상태로 진행
		if (_owner.PresentationPlayer.IsPlaying("GameStart"))
		{
		}
		else
		{
			_owner.FSM.Transition(new GameLogicStateGame());
		}
	}
}