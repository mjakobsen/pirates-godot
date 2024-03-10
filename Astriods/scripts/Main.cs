using Godot;

public partial class Main : Node
{
	Node Lasers = new Node();
	player Player = new player();
	
	Node Asteroids = new Node();

	[Export]
	PackedScene AsteroidScene { get; set; }
	
	Hud Hud = new Hud();
	
	Node2D SpawnPosition = new Node2D();
	Control GameOverScreen = new Control();
	AudioStreamPlayer LaserSound = new AudioStreamPlayer();
	AudioStreamPlayer PlayerHitSound = new AudioStreamPlayer();
	AudioStreamPlayer AsteroidHitSound = new AudioStreamPlayer();
	
	int Score = 0;
	
	int Lives = 3;
	
	public override void _Ready()
	{
		Lasers = GetNode<Node>("Lasers");
		Asteroids = GetNode<Node>("Asteroids");
	
		Hud = GetNode<Hud>("UI/HUD");
		Hud.SetScore(Score);
		Hud.SetLives(Lives);
		
		SpawnPosition = GetNode<Node2D>("SpawnPosition");
		GameOverScreen = GetNode<Control>("UI/GameOverScreen");
		LaserSound = GetNode<AudioStreamPlayer>("LaserSound");
		PlayerHitSound = GetNode<AudioStreamPlayer>("PlayerHitSound");
		AsteroidHitSound = GetNode<AudioStreamPlayer>("AsteroidHitSound");
		
		Player = GetNode<player>("Player");
		Player.LaserFired += OnLaserFired;
		Player.Died += OnPlayerDied;
		
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
		LaserSound.Play();
		Lasers.AddChild(laser);
	}
	
	public void OnAsteroidTimerExpired()
	{
		Vector2 screenSize = GetTree().Root.Size;
		float x = (float)GD.RandRange(0, screenSize.X);
		float y = (float)GD.RandRange(0, screenSize.Y);
		SpawnAsteroid(new Vector2(x, y), AsteroidSize.Large);
	}
	
	public void OnAsteroidExploded(Asteroid asteroid)
	{
		AsteroidHitSound.Play();
		
		switch (asteroid.Size)
		{
			case AsteroidSize.Large:
				SpawnAsteroid(asteroid.Position, AsteroidSize.Medium);
				SpawnAsteroid(asteroid.Position, AsteroidSize.Medium);
				Score += 50;
				break;
			case AsteroidSize.Medium:
				SpawnAsteroid(asteroid.Position, AsteroidSize.Small);
				SpawnAsteroid(asteroid.Position, AsteroidSize.Small);
				Score += 100;
				break;
			case AsteroidSize.Small:
				Score += 150;
				break;
		}
		
		Hud.SetScore(Score);
	}
	
	public void OnPlayerDied()
	{
		PlayerHitSound.Play();
		Lives -= 1;
		Hud.SetLives(Lives);

		if (Lives <= 0) 
		{
			Player.QueueFree();
			GameOverScreen.Visible = true;
		}
		else 
		{
			Player.Respawn(SpawnPosition.GlobalPosition);
		}
		
	}
	
	public void SpawnAsteroid(Vector2 position, AsteroidSize size)
	{
		Asteroid asteroid = AsteroidScene.Instantiate<Asteroid>();
		asteroid.GlobalPosition = position;
		asteroid.Size = size;
		asteroid.AsteroidExploded += OnAsteroidExploded;
		Asteroids.CallDeferred("add_child", asteroid);
	}
}
