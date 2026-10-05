using System;
using Godot;

public class Inventory
{
	private int _coinsAmount;

	public event Action<int> CoinCollected;

	public void AddCoin()
	{
		_coinsAmount++;
		GD.Print("You have: " + _coinsAmount + " coins");
		CoinCollected?.Invoke(_coinsAmount);
	}
}
