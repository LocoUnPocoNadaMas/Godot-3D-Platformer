using Godot;

namespace GodotPlatformer3D.Assets.scripts;

public partial class Hub : Control
{
	private Label _lblScore;

	private EventBus _eventBus;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lblScore = GetNode<Label>("lblScore");
		_eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBus.UpdateLabelScore += OnUpdateLabelScore;
	}

	private void OnUpdateLabelScore(int amount)
	{
		_lblScore.Text = "Score: " + amount;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}