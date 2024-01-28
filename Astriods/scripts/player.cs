using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	public float MaxSpeed = 1000.0f;
	
	[Export]
	public float Acceleration = 20.0f;

	[Export]
	public float RotationSpeed = 10.0f;
	
	[Export]
	public PackedScene SceneLoader { get; set; }

	public Node2D Muzzle = new Node2D();

	[Signal]
	public delegate void LaserFiredEventHandler(laser laser);

	public override void _Ready()
	{
		Muzzle = GetNode<Node2D>("Muzzle");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Fire"))
		{
			ShootLaser();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		int direction = Input.IsActionPressed("Forward") ? -1 : 0;
		Vector2 inputVector = new Vector2(0, direction);

		Velocity += inputVector.Rotated(Rotation) * Acceleration;
		Velocity = Velocity.LimitLength(MaxSpeed);

		if (direction == 0)
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, 3);
		}

		if (Input.IsActionPressed("TurnLeft"))
		{
			Rotate(-(RotationSpeed * (float)delta));
		}
		if (Input.IsActionPressed("TurnRight"))
		{
			Rotate(RotationSpeed * (float)delta);
		}
		
		Vector2 screenSize = GetViewportRect().Size;
		var playerPosition = GlobalPosition;
		
		if (playerPosition.X < 0)
		{
			GlobalPosition = new Vector2(screenSize.X, playerPosition.Y);
		}
		if (playerPosition.X > screenSize.X)
		{
			GlobalPosition = new Vector2(0, playerPosition.Y);
		}

		if (playerPosition.Y < 0)
		{
			GlobalPosition = new Vector2(playerPosition.X, screenSize.Y);
		}
		if (playerPosition.Y > screenSize.Y)
		{
			GlobalPosition = new Vector2(playerPosition.X, 0);
		}

		MoveAndSlide();
	}
	
	public void ShootLaser()
	{
		laser laser = SceneLoader.Instantiate<laser>();
		laser.GlobalPosition = Muzzle.GlobalPosition;
		laser.Rotation = Rotation;
		EmitSignal(SignalName.LaserFired, laser);
	}
}
