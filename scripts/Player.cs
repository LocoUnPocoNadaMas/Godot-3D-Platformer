using System;
using Godot;

namespace GodotPlatformer3D.scripts;

public partial class Player : CharacterBody3D
{
    [Export] private const float Speed = 4.0f;
    [Export] private const float JumpVelocity = 8.0f;

    [Export] private const float Gravity = 20.0f;

    private float _facingAngle = 0.0f;

    private MeshInstance3D _model;

    public override void _Ready()
    {
        try
        {
            _model = GetNode<MeshInstance3D>("ModelPlayer");
            /*
            _rocketScene = ResourceLoader.Load<PackedScene>("res://prefabs/rocket.tscn");
            */
        }
        catch (ArgumentException e)
        {
            GD.PrintErr(e);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // Apply gravity if it in the air
        if (!IsOnFloor())
        {
            Fall((float)delta);
        }
        // If it on the floor, it can Jump
        else
        {
            Jump();
        }
        // Get key/button pressed
        var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        //var dir0 = new Vector3(input.X, 0, input.Y);
        Move(input);
        if (input != Vector2.Zero)
        {
            Rotate(input);
        }
    }

    private void Fall(float delta)
    {
        var velocity = Velocity;
        velocity.Y -= Gravity * delta;
        Velocity = velocity;
        if(GlobalPosition.Y < -5.0f)
            GameOver();
    }

    private void Jump()
    {
        if (Input.IsActionPressed("jump"))
        {
            var velocity = Velocity;
            velocity.Y = JumpVelocity;
            Velocity = velocity;
        }
    }

    private void Move(Vector2 input)
    {
        var velocity = Velocity;
        // Assign direction to velocity
        velocity.Z = input.Y * Speed;
        velocity.X = input.X * Speed;
        Velocity = velocity;
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

    private void GameOver()
    {
        GetTree().ReloadCurrentScene();
    }

    /*
    [Export] private const float Speed = 5.0f;
    [Export] private const float JumpVelocity = 4.5f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    [Export] private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= _gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        var inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        var direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
    */
}