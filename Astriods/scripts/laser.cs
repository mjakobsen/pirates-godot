using Godot;

public partial class laser : Area2D
{
	public Vector2 Movement = new Vector2(0, -1);
	public float Speed = 750;

	public override void _PhysicsProcess(double delta)
	{
		float x = GlobalPosition.X + Movement.Rotated(Rotation).X * Speed * (float)delta;
		float y = GlobalPosition.Y + Movement.Rotated(Rotation).Y * Speed * (float)delta;
		GlobalPosition = new Vector2(x, y);
	}

	public void OnScreenExited()
	{
		QueueFree();
	}
}
