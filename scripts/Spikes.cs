using Godot;

namespace GodotPlatformer3D.scripts;

public partial class Spikes : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnBodyEntered(Node3D body)
	{
		if (body is not Player bodyInstance) return;
		bodyInstance.GameOver();
	}
}