using System;
using GMathCad.UI.Framework;

namespace GMathCad.UI
{
	public class TextArea : Area
	{
		private string text = string.Empty;
		
		private TextBlock textBlock = new TextBlock ();
		
		public TextArea ()
		{
			Content = textBlock;
			textBlock.Text = "▪";			
		}
		
		public void Append (char c)
		{
			text += c;
			
			textBlock.Text = text;
		}
	}
}

