using System.Collections.Generic;
using Godot;

public class AchievementSystem
{
	private const int CoinCollectorGoal = 10;
	private const int SpeedGoalCoins = 5;
	private const float SpeedGoalSeconds = 10f;

	private readonly Queue<float> _recentPickupTimestamps = new();

	private bool _coinCollectorUnlocked;
	private bool _speedyGatherUnlocked;

	public void Subscribe(Inventory inventory)
	{
		inventory.CoinCollected += OnCoinCollected;
	}

	public void Unsubscribe(Inventory inventory)
	{
		inventory.CoinCollected -= OnCoinCollected;
	}

	private void OnCoinCollected(int totalCoins)
	{
		CheckCoinCollectorAchievement(totalCoins);
		CheckSpeedyGatherAchievement();
	}

	private void CheckCoinCollectorAchievement(int totalCoins)
	{
		if (_coinCollectorUnlocked) return;
		if (totalCoins < CoinCollectorGoal) return;

		_coinCollectorUnlocked = true;
		GD.Print($"Achievement unlocked: Coin Collector - collected {CoinCollectorGoal} coins!");
	}

	private void CheckSpeedyGatherAchievement()
	{
		if (_speedyGatherUnlocked) return;

		var now = Time.GetTicksMsec() / 1000f;
		_recentPickupTimestamps.Enqueue(now);

		while (_recentPickupTimestamps.Count > 0 && now - _recentPickupTimestamps.Peek() > SpeedGoalSeconds)
		{
			_recentPickupTimestamps.Dequeue();
		}

		if (_recentPickupTimestamps.Count < SpeedGoalCoins) return;

		_speedyGatherUnlocked = true;
		GD.Print($"Achievement unlocked: Speedy Gather - collected {SpeedGoalCoins} coins in {SpeedGoalSeconds} seconds!");
	}
}
