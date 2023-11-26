using Godot;

namespace GodotPlatformer3D.Assets.scripts;

public partial class EventBus : Node
{
	[Signal]
	public delegate void UpdateMainScoreEventHandler(int amount);
	
	[Signal]
	public delegate void UpdateLabelScoreEventHandler(int amount);

	[Signal]
	public delegate void ChangeLevelEventHandler(string level);

	[Signal]
	public delegate void RestartEventHandler();
}