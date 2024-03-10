using Godot;

public partial class GameOverScreen : Control
{
	public void Restart()
	{
		GetTree().ReloadCurrentScene();
	}
}
