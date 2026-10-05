using Godot;
using System;

public partial class GameManger : Node
{
	private Player _player;
	private CoinSpawner _coinSpawner;
	private AchievementSystem _achievementSystem;

	public override void _Ready()
	{
		_player = CreatePlayer();
		_coinSpawner = CreateCoinSpawner();
		_achievementSystem = CreateAchievementSystem();
	}

	private AchievementSystem CreateAchievementSystem()
	{
		var achievementSystem = new AchievementSystem();
		achievementSystem.Subscribe(_player.Inventory);
		return achievementSystem;
	}

	private Player CreatePlayer()
	{
		var playerSettings = new PlayerSettings()
		{
			speed = 80,
			spritePath = "res://Assets/player.png"
		};
		return new Player(playerSettings, this);
	}

	private CoinSpawner CreateCoinSpawner()
	{
		var coinSpawnerSettings = new CoinSpawnerSettings()
		{
			spawnCenter = Vector2.Zero,
			spawnBoundsSize = new Vector2(200, 200),
			spawnInterval = 1,
			spritePath = "res://Assets/coin.png"
		};
		return new CoinSpawner(coinSpawnerSettings, this, _player);
	}

	public override void _Process(double delta)
	{
		_player.Update((float)delta);
		_coinSpawner.Update((float)delta);
	}
}
