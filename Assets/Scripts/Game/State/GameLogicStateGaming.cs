using UnityEngine;

public class GameLogicStateGame : IState<GameLogicManager>
{
	private Block prevHitBlock;

	public GameLogicStateGame()
	{
		prevHitBlock = null;
	}

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
			_owner.FSM.Transition(new GameLogicStateGameOver());
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (_owner.GetTcouhedBlock(out var hit))
				{
					if (prevHitBlock == null)
					{
						// 처음이면 그냥 대입
						prevHitBlock = hit;
					}
					else if (prevHitBlock != hit)
					{
						if (_owner.IsNeighborhood(prevHitBlock, hit))
						{
							_owner.SwapPoistion(prevHitBlock, hit);

							prevHitBlock = null;
						}
					}
					else
					{
						prevHitBlock = null;
						// 같으면 취소? 
					}
				}
				else
				{
					// 다르면 터치 취소
				}
			}
		}
	}
}