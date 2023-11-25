using Godot;

namespace GodotPlatformer3D.scripts;

public partial class Spikes : Area3D
{
	private void OnBodyEntered(Node3D body)
	{
		if (body is not Player bodyInstance) return;
		bodyInstance.GameOver();
	}
}