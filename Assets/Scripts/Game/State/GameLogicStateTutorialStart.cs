using UnityEngine;

public class GameLogicStateTutorialStart : IState<GameLogicManager>
{
	public void OnEntry(GameLogicManager _owner)
	{
		if (_owner.PresentationPlayer.GetClip("TutorialStart"))
		{
			_owner.PresentationPlayer.Stop("TutorialStart");
			_owner.PresentationPlayer.CrossFade("TutorialStart");
		}
	}

	public void OnExit(GameLogicManager _owner)
	{
	}

	public void OnRoutine(GameLogicManager _owner)
	{
		if (_owner.PresentationPlayer.IsPlaying("TutorialStart"))
		{
		}
		else
		{
			_owner.FSM.Transition(new GameLogicStateTutorial());
		}
	}
}