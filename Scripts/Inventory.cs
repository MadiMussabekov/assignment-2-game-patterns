using System.Collections.Generic;
using Godot;

public class Inventory : IInventorySubject
{
	private int _coinsAmount;
	private List<IInventoryObserver> _observers = new();

	public void Subscribe(IInventoryObserver observer)
	{
		_observers.Add(observer);
	}

	public void Unsubscribe(IInventoryObserver observer)
	{
		_observers.Remove(observer);
	}

	public void AddCoin()
	{
		_coinsAmount++;
		GD.Print("You have: " + _coinsAmount + " coins");
		NotifyObservers();
	}

	private void NotifyObservers()
	{
		foreach (var observer in _observers)
		{
			observer.OnCoinCollected(_coinsAmount);
		}
	}
}
