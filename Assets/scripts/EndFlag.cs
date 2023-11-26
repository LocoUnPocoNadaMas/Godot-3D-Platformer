using Godot;

namespace GodotPlatformer3D.Assets.scripts;

public partial class EndFlag : Node
{
	private EventBus _eventBus;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node3D body)
	{
		_eventBus.EmitSignal(EventBus.SignalName.ChangeLevel, "level_3.tscn");
	}
}