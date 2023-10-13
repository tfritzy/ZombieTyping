using Godot;

public class TypeBox : Node
{
	TextEdit editor;
	RichTextLabel label;

	private string Text = "It was the best of times, it was the worst of times, it was the age of wisdom, it was the age of foolishness, it was the epoch of belief, it was the epoch of incredulity, it was the season of Light, it was the season of Darkness, it was the spring of hope, it was the winter of despair, we had everything before us, we had nothing before us, we were all going direct to Heaven, we were all going direct the other way-in short, the period was so far like the present period, that some of its noisiest authorities insisted on its being received, for good or for evil, in the superlative degree of comparison only.";

	private int CompletedIndex = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var screenSize = OS.GetScreenSize();

		editor = new TextEdit
		{
			RectPosition = new Vector2(0, 300),
			RectSize = new Vector2(500, 100),
		};
		AddChild(editor);

		label = new RichTextLabel
		{
			RectPosition = new Vector2(0, 0),
			RectSize = new Vector2(500, 300),
			BbcodeEnabled = true,
		};
		AddChild(label);

		label.Text = Text;
	}

	public override void _Process(float delta)
	{
		int partialCompletion = 0;
		while (
			editor.Text.Length > partialCompletion &&
			editor.Text[partialCompletion] == Text[CompletedIndex + partialCompletion])
		{
			GD.Print("Matched " + editor.Text[partialCompletion]);

			if (editor.Text[partialCompletion] == ' ')
			{
				CompletedIndex += partialCompletion + 1;
				editor.Text = "";
				break;
			}

			partialCompletion++;
		}
		GD.Print("Matches up to " + partialCompletion + " characters.");

		var text = Text.Substring(0, CompletedIndex);
		var remaining = Text.Substring(CompletedIndex);
		label.BbcodeText = "[b][color=green]" + text + "[/color][/b]" + remaining;
	}
}
