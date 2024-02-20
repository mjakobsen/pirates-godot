using Godot;

public partial class Main : Node
{
	Node Lasers = new Node();
	player Player = new player();
	
	Node Asteroids = new Node();

	[Export]
	PackedScene AsteroidScene { get; set; }
	
	public override void _Ready()
	{
		Lasers = GetNode<Node>("Lasers");
		Asteroids = GetNode<Node>("Asteroids");
	
		Player = GetNode<player>("Player");
		Player.LaserFired += OnLaserFired;
		
		OnAsteroidTimerExpired();
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Restart"))
		{
			GetTree().ReloadCurrentScene();
		}
	}
	
	public void OnLaserFired(laser laser)
	{
		Lasers.AddChild(laser);
	}
	
	public void OnAsteroidTimerExpired()
	{
		GD.Print("Spawn asteroid");
		Vector2 screenSize = GetTree().Root.Size;
		float x = (float)GD.RandRange(0, screenSize.X);
		float y = (float)GD.RandRange(0, screenSize.Y);
		SpawnAsteroid(new Vector2(x, y), AsteroidSize.Large);
	}
	
	public void SpawnAsteroid(Vector2 position, AsteroidSize size)
	{
		Asteroid asteroid = AsteroidScene.Instantiate<Asteroid>();
		asteroid.GlobalPosition = position;
		asteroid.Size = size;
		Asteroids.AddChild(asteroid);
	}
}
