using System;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class Block : MonoBehaviour
{
	public SpriteRenderer SpriteRenderer { get; private set; }
	public Vector2 StartPosition { get; private set; }
	public Vector2Int SpriteSize { get; private set; } = new Vector2Int(130, 130);
	public Vector2Int Coord { get; private set; }
	public int Type { get; private set; }

	public bool IsActive
	{
		get => isActiveAndEnabled;
	}

	private void Awake()
	{
		SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Initialize(int _type, Sprite _sprite, int _row, int _column, Vector2 _pos)
	{
		Assign(_type, _sprite, _row, _column, _pos);
	}

	public void Recycle(int _type, Sprite _sprite, int _row, int _column, Vector2 _pos)
	{
		Activate();

		Assign(_type, _sprite, _row, _column, _pos);
	}

	private void Assign(int _type, Sprite _sprite, int _row, int _column, Vector2 _pos)
	{
		Type = _type;
		SpriteRenderer.sprite = _sprite;

		Coord = new Vector2Int(_column, _row);
		StartPosition = _pos;

		ApplyPosition();
	}

	public static void SwapPosition(Block _a, Block _b)
	{
		var tmp = _a.Coord;
		_a.Coord = _b.Coord;
		_b.Coord = tmp;
	}

	public static int GetManhattanDistance(Block _a, Block _b) => Mathf.Abs((_a.Coord.x - _b.Coord.x) + (_a.Coord.y - _b.Coord.y));
	public int GetManhattanDistance(Block _b) => GetManhattanDistance(this, _b);
	public static bool IsSameType(Block _a, Block _b) => Equals(_a.Type, _b.Type);
	public bool IsSameType(Block _block) => IsSameType(this, _block);
	public void ApplyPosition() => transform.position = StartPosition + new Vector2Int(Coord.x * SpriteSize.x, -(Coord.y * SpriteSize.y));
	private void Activate() => gameObject.SetActive(true);
}



