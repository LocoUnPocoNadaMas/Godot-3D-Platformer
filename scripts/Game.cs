using Godot;

namespace GodotPlatformer3D.scripts;

public partial class Game : Node
{
	private int _score = 0;
	private PackedScene _currentLevel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.PrintErr("inicio");
		ChangeLevel("level_1.tscn");
		var eb = GetNode<EventBus>("/root/EventBus");
		eb.UpdateScore += CoinOnGiveMeYourMoney;
	}

	private void ChangeLevel(string levelTscn)
	{
		_currentLevel = ResourceLoader.Load<PackedScene>("res://scenes/" + levelTscn);
		var newLevelIns = _currentLevel.Instantiate();
		var levelNode = GetNode<Node3D>("Level");
		if (levelNode.GetChildCount() > 0)
			levelNode.RemoveChild(levelNode.GetChild(0));
		levelNode.AddChild(newLevelIns);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CoinOnGiveMeYourMoney(int amount)
	{
		_score += amount;
		GD.PrintErr(_score);
	}
}