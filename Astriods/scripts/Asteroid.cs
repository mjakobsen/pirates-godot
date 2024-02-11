using Godot;

public partial class Asteroid : Area2D
{
	[Export]
	public AsteroidSize Size = AsteroidSize.Large;
	
	public Vector2 Movement = new Vector2(0, -1);
	public float Speed = 60;
	public Sprite2D Sprite;
	public CollisionShape2D Shape;
	
	public override void _Ready()
	{
		RotationDegrees = (float)GD.RandRange(0d, 360d);
		
		Sprite = GetNode<Sprite2D>("Sprite2D");
		Shape = GetNode<CollisionShape2D>("CollisionShape2D");

		switch (Size)
		{
			case AsteroidSize.Large:
				Sprite.Texture = GD.Load<Texture2D>("res://assets/sprites/meteorGrey_big4.png");
				Shape.Shape = GD.Load<Shape2D>("res://resources/cshape_asteroid_large.tres");
				break;
			case AsteroidSize.Medium:
				Sprite.Texture = GD.Load<Texture2D>("res://assets/sprites/meteorGrey_med2.png");
				Shape.Shape = GD.Load<Shape2D>("res://resources/cshape_asteroid_medium.tres");
				break;
			case AsteroidSize.Small:
				Sprite.Texture = GD.Load<Texture2D>("res://assets/sprites/meteorGrey_tiny1.png");
				Shape.Shape = GD.Load<Shape2D>("res://resources/cshape_asteroid_small.tres");
				break;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		float x = GlobalPosition.X + Movement.Rotated(RotationDegrees).X * Speed * (float)delta;
		float y = GlobalPosition.Y + Movement.Rotated(RotationDegrees).Y * Speed * (float)delta;
		GlobalPosition = new Vector2(x, y);
		
		Vector2 screenSize = GetViewportRect().Size;
		var astroidPosition = GlobalPosition;
		
		if (astroidPosition.X < 0)
		{
			GlobalPosition = new Vector2(screenSize.X, astroidPosition.Y);
		}
		if (astroidPosition.X > screenSize.X)
		{
			GlobalPosition = new Vector2(0, astroidPosition.Y);
		}

		if (astroidPosition.Y < 0)
		{
			GlobalPosition = new Vector2(astroidPosition.X, screenSize.Y);
		}
		if (astroidPosition.Y > screenSize.Y)
		{
			GlobalPosition = new Vector2(astroidPosition.X, 0);
		}
	}
	
	public enum AsteroidSize
	{
		Small,
		Medium,
		Large
	}
}
