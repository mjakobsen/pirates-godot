using Godot;

public partial class Main : Node
{
	Node Lasers = new Node();
	player Player = new player();	
	
	public override void _Ready()
	{
		Lasers = GetNode<Node>("Lasers");
	
		Player = GetNode<player>("Player");
		Player.LaserFired += OnLaserFired;
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
}
