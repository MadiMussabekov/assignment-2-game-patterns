using System;
using Godot;

public struct PlayerSettings
{
	public float speed;
	public string spritePath;
}

public class Player : IUpdatable
{
	private PlayerSettings _settigns;
	private PlayerController _controller;
	private CharacterBody2D _body;
	private Sprite2D _sprite;
	private Node _root;
	private Inventory _inventory;

	public Inventory Inventory => _inventory;

	public Player(PlayerSettings settings, Node root)
	{
		_settigns = settings;
		_root = root;

		_controller = new PlayerController(_settigns.speed);
		_body = CreateBody();
		_sprite = CreateSprite();
		_inventory = new Inventory();
	}

	public Vector2 GetPosition() => _body.Position;

	public void Update(float deltaTime)
	{
		MoveBody();
	}

	private void MoveBody()
	{
		_body.Velocity = _controller.GetVelocity();
		_body.MoveAndSlide();
	}

	private CharacterBody2D CreateBody()
	{
		var character = new CharacterBody2D();
		character.Name = "Player";
		character.Position = Vector2.Zero;
		_root.AddChild(character);
		return character;
	}

	private Sprite2D CreateSprite()
	{
		var sprite = new Sprite2D();
		sprite.Name = "PlayerSprite";
		sprite.Texture = GD.Load<Texture2D>(_settigns.spritePath);
		_body.AddChild(sprite);
		return sprite;
	}
}
