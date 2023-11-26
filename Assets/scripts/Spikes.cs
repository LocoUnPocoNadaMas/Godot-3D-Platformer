using Godot;

namespace GodotPlatformer3D.Assets.scripts;

public partial class Spikes : Area3D
{
	private void OnBodyEntered(Node3D body)
	{
		if (body is not Player bodyInstance) return;
		bodyInstance.GameOver();
	}
}