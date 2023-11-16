using Godot;

namespace GodotPlatformer3D.scripts;

public partial class Enemy : Godot.Area3D
{
	[Export] private float _moveSpeed = 2f;
	[Export] private Vector3 _moveDirection = new Vector3();

	private Vector3 _startPosition = new Vector3();
	private Vector3 _targetPosition = new Vector3();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_startPosition = GlobalPosition;
		_targetPosition = _startPosition + _moveDirection;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_targetPosition, _moveSpeed * (float)delta);
		if(GlobalPosition == _targetPosition)
			if (GlobalPosition == _startPosition)
				_targetPosition = _startPosition + _moveDirection;
			else
				_targetPosition = _startPosition;
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body is not Player bodyInstance) return;
		bodyInstance.GameOver();
	}
}