using System;
using Godot;

public class Coin : IUpdatable
{
    private Node2D _body;
    private Sprite2D _sprite;
    private Node _root;
    private string _spritePath;
    private Player _player;
    private bool _enabled = true;

    public Coin(Vector2 position, string spritePath, Node root, Player player)
    {
        _root = root;
        _player = player;
        _spritePath = spritePath;
        _body = CreateBody(position);
        _sprite = CreateSprite();
    }

    public void Update(float deltaTime)
    {
        if (!_enabled) return;

        if ((_body.Position - _player.GetPosition()).Length() < 16)
        {
            PickCoin();
        }
    }

    private void PickCoin()
    {
        _player.Inventory.AddCoin();
        _body.QueueFree();
        _enabled = false;
        GD.Print("You picked a coin!");
    }

    private Node2D CreateBody(Vector2 position)
    {
        var body = new Node2D();
        body.Name = "Coin";
        body.Position = position;
        _root.AddChild(body);
        return body;
    }

    private Sprite2D CreateSprite()
    {
        var sprite = new Sprite2D();
        sprite.Name = "CoinSprite";
        sprite.Texture = GD.Load<Texture2D>(_spritePath);
        _body.AddChild(sprite);
        return sprite;
    }
}