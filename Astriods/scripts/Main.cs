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
	
	public void OnLaserFired(laser laser)
	{
		Lasers.AddChild(laser);
	}
}
