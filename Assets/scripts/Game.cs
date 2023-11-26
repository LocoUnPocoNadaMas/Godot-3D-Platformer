using Godot;

namespace GodotPlatformer3D.Assets.scripts;

public partial class Game : Node
{
	private int _score = 0;
	private int _currentScore;
	
	private Node _currentScene;
	private string _currentLvl;
	
	private EventBus _eventBus;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.PrintErr("inicio");
		LoadingScene("level_1.tscn");
		_eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBus.UpdateMainScore += CoinOnGiveMeYourMoney;
		_eventBus.Restart += OnRestart;
	}

	private void OnRestart()
	{
		// Rollback score
		_currentScore = _score;
		// Restart Scene
		GotoScene(_currentLvl);
		// Update the Score Label
		_eventBus.EmitSignal(EventBus.SignalName.UpdateLabelScore, _score);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CoinOnGiveMeYourMoney(int amount)
	{
		_currentScore += amount;
		GD.PrintErr(_currentScore);
		_eventBus.EmitSignal(EventBus.SignalName.UpdateLabelScore, _currentScore);
	}
	
	private void GotoScene(string path)
	{
		_score = _currentScore;
		// This function will usually be called from a signal callback,
		// or some other function from the current scene.
		// Deleting the current scene at this point is
		// a bad idea, because it may still be executing code.
		// This will result in a crash or unexpected behavior.

		// The solution is to defer the load to a later time, when
		// we can be sure that no code from the current scene is running:

		CallDeferred(MethodName.DeferredGotoScene, path);
	}

	private void DeferredGotoScene(string path)
	{
		// It is now safe to remove the current scene
		_currentScene.Free();
		LoadingScene(path);
	}

	private void LoadingScene(string path)
	{
		_currentLvl = path;
		// Load a new scene.
		// I don't understand what is the difference between ResourceLoader and GD
		var nextScene = ResourceLoader.Load<PackedScene>("res://Assets/scenes/" + path);
		
		// Instance the new scene.
		_currentScene = nextScene.Instantiate();

		// Add it to the active scene, as child of root.
		//GetTree().Root.AddChild(CurrentScene);

		// Optionally, to make it compatible with the SceneTree.change_scene_to_file() API.
		//GetTree().CurrentScene = CurrentScene;
		
		// Remove old Children
		var levelNode = GetNode<Node3D>("Level");
		if (levelNode.GetChildCount() > 1)
			levelNode.RemoveChild(levelNode.GetChild(1));
		// Add it to the active scene, as child of Level.
		levelNode.AddChild(_currentScene);
	}
}