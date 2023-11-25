using Godot;

namespace GodotPlatformer3D.scripts;

public partial class Coin : Area3D
{
    [Export] private float _spinSpeed = 2.0f;
    [Export] private float _bobHeight = 0.2f;
    [Export] private float _bobSpeed = 5.0f;
    [Export] private float _time;

    [Export] private float _startY;
    private EventBus eb;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _startY = GlobalPosition.Y;
        eb = GetNode<EventBus>("/root/EventBus");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Rotate
        Rotate(Vector3.Up, _spinSpeed * (float)delta);
        // Up and down
        _time += (float)delta;
        var distance = (Mathf.Sin(_time * _bobSpeed) + 1) / 2;
        var aux = GlobalPosition;
        aux.Y = _startY + (distance * _bobHeight);
        GlobalPosition = aux;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is not Player bodyInstance) return;
        eb.EmitSignal(EventBus.SignalName.UpdateScore, 1);
        QueueFree();
    }
}