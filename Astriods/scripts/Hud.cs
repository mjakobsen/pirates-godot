using Godot;

public partial class Hud : Control
{
	Label Score = new Label();
	
	[Export]
	PackedScene LifeScene { get; set; }
	
	HBoxContainer Lives = new HBoxContainer();
	
	public override void _Ready()
	{
		Score = GetNode<Label>("Score");
		
		Lives = GetNode<HBoxContainer>("Lives");
	}
	
	public void SetScore(int score)
	{
		Score.Text = "SCORE: " + score.ToString();
	}
	
	public void SetLives(int lives)
	{
		foreach (TextureRect life in Lives.GetChildren())
		{
			life.QueueFree();
		}
		
		for (int i = 0; i < lives; i++)
		{
			TextureRect life = LifeScene.Instantiate<TextureRect>();
			Lives.CallDeferred("add_child", life);
		}
	}
}
