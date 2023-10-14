using Godot;

public class TypeBox : Node
{
	TextEdit editor;
	RichTextLabel label;

	private string Text = "It was the best of times, it was the worst of times, it was the age of wisdom, it was the age of foolishness, it was the epoch of belief, it was the epoch of incredulity, it was the season of Light, it was the season of Darkness, it was the spring of hope, it was the winter of despair, we had everything before us, we had nothing before us, we were all going direct to Heaven, we were all going direct the other way-in short, the period was so far like the present period, that some of its noisiest authorities insisted on its being received, for good or for evil, in the superlative degree of comparison only.";

	private int CompletedIndex = 0;
	private int CompletedInCurrentWord = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RenderElements();
		GetTree().Root.Connect("size_changed", this, "RenderElements");
	}

	private void RenderElements()
	{
		var screenSize = GetViewport().Size;
		int width = 100;
		int labelHeight = 175;

		if (editor != null)
		{
			editor.RectPosition = new Vector2(screenSize.x / 2 - width / 2, labelHeight);
			editor.RectSize = new Vector2(500, 25);
		}
		else
		{
			editor = new TextEdit
			{
				RectPosition = new Vector2(screenSize.x / 2 - width / 2, labelHeight),
				RectSize = new Vector2(500, 25),
			};
			AddChild(editor);
		}

		if (label != null)
		{
			label.RectPosition = new Vector2(screenSize.x / 2 - width / 2, 0);
			label.RectSize = new Vector2(500, labelHeight);
		}
		else
		{
			label = new RichTextLabel
			{
				RectPosition = new Vector2(screenSize.x / 2 - width / 2, 0),
				RectSize = new Vector2(500, labelHeight),
				BbcodeEnabled = true,
			};
			AddChild(label);
		}

		label.Text = Text;
	}

	public override void _Process(float delta)
	{
		int partialCompletion = 0;
		while (
			editor.Text.Length > partialCompletion &&
			editor.Text[partialCompletion] == Text[CompletedIndex + partialCompletion])
		{
			if (editor.Text[partialCompletion] == ' ')
			{
				CompletedInCurrentWord = 0;
				CompletedIndex += partialCompletion + 1;
				editor.Text = "";
				break;
			}

			partialCompletion++;
			while (partialCompletion > CompletedInCurrentWord)
			{
				CompletedInCurrentWord += 1;
				IncrementCharactersCompleteForSoldiers();
			}
		}

		var text = Text.Substring(0, CompletedIndex);
		var remaining = Text.Substring(CompletedIndex);
		label.BbcodeText = "[b][color=green]" + text + "[/color][/b]" + remaining;
	}

	private void IncrementCharactersCompleteForSoldiers()
	{
		var soldiers = GetTree().GetNodesInGroup("soldiers");
		foreach (Soldier soldier in soldiers)
		{
			soldier.OnCharacterComplete();
		}
	}
}
