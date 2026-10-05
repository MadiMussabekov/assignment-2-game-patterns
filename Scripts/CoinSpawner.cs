using System;
using System.Collections.Generic;
using Godot;

public struct CoinSpawnerSettings
{
    public Vector2 spawnCenter;
    public Vector2 spawnBoundsSize;
    public float spawnInterval;
    public string spritePath;
}

public class CoinSpawner : IUpdatable
{
    private CoinSpawnerSettings _settings;
    private Node _root;

    private float _time;
    private List<Coin> _spawnedCoins = new ();
    private Player _player;

    public CoinSpawner(CoinSpawnerSettings settings, Node root, Player player)
    {
        _settings = settings;
        _root = root;
        _player = player;
    }

    public void Update(float deltaTime)
    {
        SpawnCoin(deltaTime);

        foreach(var coin in _spawnedCoins)
        {
            coin?.Update(deltaTime);
        }
    }

    private void SpawnCoin(float deltaTime)
    {
        if (IsTimeToSpawn(deltaTime) == false)
            return;
        
        _spawnedCoins.Add(new Coin(GetRandomPosition(), _settings.spritePath, _root, _player));
    }

    private Vector2 GetRandomPosition()
    {
        var randomX = GD.Randf() * _settings.spawnBoundsSize.X;
        var randomY = GD.Randf() * _settings.spawnBoundsSize.Y;

        return _settings.spawnCenter 
            + new Vector2(randomX, randomY)
            - _settings.spawnBoundsSize / 2;
    }

    private bool IsTimeToSpawn(float deltaTime)
    {
        _time += deltaTime;
        if (_time > _settings.spawnInterval)
        {
            _time = 0;
            return true;
        }
        return false;
    }
}