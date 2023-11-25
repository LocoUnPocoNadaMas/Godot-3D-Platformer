using Godot;

namespace GodotPlatformer3D.scripts;

public partial class EventBus : Node
{
	[Signal]
	public delegate void UpdateScoreEventHandler(int amount);

	[Signal]
	public delegate void ChangeLevelEventHandler(string level);

	[Signal]
	public delegate void RestartEventHandler();
}