using Godot;

namespace GodotPlatformer3D.Assets.scripts;

public partial class Player : CharacterBody3D
{
	[Export] private float _speed = 4f;
	[Export] private float _jumpVelocity = 8.0f;
	[Export] private float _gravity = 20f;

	private float _facingAngle;

	private MeshInstance3D _model;
	private EventBus _eb;

	public override void _Ready()
	{
		_model = GetNode<MeshInstance3D>("ModelPlayer");
		_eb = GetNode<EventBus>("/root/EventBus");
	}

	public override void _PhysicsProcess(double delta)
	{
		//# Apply gravity if it in the air
		if(!IsOnFloor())
			Fall(delta);
		//# If it on the floor, it can Jump
		else
			Jump();
		//# Get key/button pressed
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//#var dir0 = new Vector3(input.X, 0, input.Y);
		Move(input);
		if (input != Vector2.Zero)
			Rotate(input);
	}

	private void Fall(double delta)
	{
		var aux = Velocity;
		aux.Y -= _gravity * (float)delta;
		Velocity = aux;
		if (GlobalPosition.Y < -5)
			GameOver();
	}

	public void GameOver()
	{
		//GetTree().ReloadCurrentScene();
		_eb.EmitSignal(EventBus.SignalName.Restart);
	}

	private void Jump()
	{
		if (Input.IsActionPressed("jump"))
		{
			var aux = Velocity;
			aux.Y = _jumpVelocity;
			Velocity = aux;
		}
	}

	private void Move(Vector2 input)
	{
		// Assign direction to velocity
		var aux = Velocity;
		aux.Z = input.Y * _speed;
		aux.X = input.X * _speed;
		Velocity = aux;
		MoveAndSlide();
	}
	
	private void Rotate(Vector2 input)
	{
		// Set facing direction
		_facingAngle = new Vector2(input.Y, input.X).Angle();
		var aux = _model.Rotation;
		// Clunky Rotation
		// aux.Y = _facingAngle;
	
		// Smooth Rotation
		aux.Y = Mathf.LerpAngle(aux.Y, _facingAngle, 0.5f);
		// Rotate
		_model.Rotation = aux;
	}
}