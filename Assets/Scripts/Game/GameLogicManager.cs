using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogicManager : Singleton<GameLogicManager>
{
	[SerializeField]
	public Animation cachedAnimation;

	[SerializeField]
	private Sprite[] blockSprites;

	[SerializeField]
	private Block blockPrefab;

	public Animation PresentationPlayer { get { return cachedAnimation; } }

	public List<Block> ActiveBlocks { get; private set; }
	public List<Block> InactiveBlocks { get; private set; }

	public StateMachine<GameLogicManager> FSM { get; private set; }

	public delegate void Func();
	public Func UpdateFunc { get; private set; }

	public bool IsTutorial { get; private set; }
	public bool IsGameOver { get; private set; }

	private int[,] premadeBlockTypeArray;

	private void Start()
	{
		UpdateFunc = null;

		FSM = new StateMachine<GameLogicManager>(this);
		FSM.Transition(new GameLogicStateInit());

		UpdateFunc = FSM.Routine;
	}

	private void Update()
	{
		UpdateFunc?.Invoke();
	}

	public void Initialize()
	{
		InitializeBlocks();
	}

	public bool GetTcouhedBlock(out Block _result)
	{
		var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit && hit.collider)
		{
			var hitBlock = hit.collider.GetComponent<Block>();
			if (hitBlock != null)
			{
				_result = hitBlock;
				return true;
			}
		}

		_result = null;
		return false;
	}

	public bool IsNeighborhood(Block prevHitBlock, Block hit)
	{
		return Block.GetManhattanDistance(prevHitBlock, hit) == 1;
	}

	public void SwapPoistion(Block _aBlock, Block _bBlock)
	{
		Block.SwapPosition(_aBlock, _bBlock);
		_aBlock.ApplyPosition();
		_bBlock.ApplyPosition();
	}

	private void InitializeBlocks()
	{
		SweepInactiveBlocks();

		PremadeBlockTypes();

		CreateBlocks();
	}

	private void PremadeBlockTypes()
	{
		var B = Constants.Blue;
		var G = Constants.Green;
		var O = Constants.Orange;
		var P = Constants.Pink;
		var R = Constants.Red;

		premadeBlockTypeArray = new int[,]
		{
			{ B, B, G, O, G, O, B, R },
			{ B, R, G, O, G, B, O, R },
			{ R, B, R, P, P, O, B, O },
			{ B, B, G, O, G, B, O, R },
			{ R, R, G, B, G, O, O, P },
			{ B, B, R, O, R, B, P, P },
			{ R, R, G, O, G, O, R, O },
			{ B, B, G, B, O, O, B, R },
			{ R, R, B, B, G, G, O, R },
			{ B, B, G, O, G, O, R, O },
			{ B, R, G, G, O, O, P, R },
			{ R, B, R, O, G, P, P, R },
		};
	}

	private void SweepInactiveBlocks()
	{
		InactiveBlocks = new List<Block>();
		transform.GetComponentsInChildren(true, InactiveBlocks);
	}

	private void CreateBlocks()
	{
		var rows = Constants.Rows;
		var columns = Constants.Columns;
		var startPos = new Vector2(20f + 65f, ((rows - 1) * 130f) + 100f);
		ActiveBlocks = new List<Block>(rows * columns);

		for (int i = 0; i < Constants.Rows; ++i)
		{
			for (int j = 0; j < Constants.Columns; ++j)
			{
				var type = premadeBlockTypeArray[i, j];
				var sprite = blockSprites[type];

				Block block = null;
				if (InactiveBlocks != null && InactiveBlocks.Count > 0)
				{
					block = InactiveBlocks.First();
					block.Recycle(type, sprite, i, j, startPos);

					InactiveBlocks.Remove(block);
				}
				else
				{
					block = Instantiate(blockPrefab, Vector2.zero, Quaternion.identity, transform);
					block.Initialize(type, sprite, i, j, startPos);
				}

				ActiveBlocks.Add(block);
			}
		}
	}

	public void TutorialOver()
	{
		IsTutorial = false;
	}
}
