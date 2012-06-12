using System;

namespace GMathCad.UI
{
	public class InsertCharacterCommand
	{
		private Gdk.Key Key { get; set; }

		private MathRegion Region { get; set; }

		public InsertCharacterCommand (Gdk.Key key, MathRegion region)
		{
			Key = key;
			Region = region;
		}
		
		public void Execute ()
		{
			switch (Key) {
			case Gdk.Key.Key_1:
				Region.ActiveArea.Append ('1');
				break;				
			case Gdk.Key.Key_2:
				Region.ActiveArea.Append ('2');
				break;
			case Gdk.Key.Key_3:
				Region.ActiveArea.Append ('3');
				break;
			case Gdk.Key.Key_4:
				Region.ActiveArea.Append ('4');
				break;
			case Gdk.Key.Key_5:
				Region.ActiveArea.Append ('5');
				break;
			case Gdk.Key.Key_6:
				Region.ActiveArea.Append ('6');
				break;
			case Gdk.Key.Key_7:
				Region.ActiveArea.Append ('7');
				break;
			case Gdk.Key.Key_8:
				Region.ActiveArea.Append ('8');
				break;
			case Gdk.Key.Key_9:
				Region.ActiveArea.Append ('9');
				break;
			case Gdk.Key.Key_0:
				Region.ActiveArea.Append ('0');
				break;
			case Gdk.Key.plus:
				//var area = Region.ActiveArea;
				var parent = Region.ActiveArea.Parent as HBoxArea;
				
				var operation = new TextArea ();
				operation.Append ('+');
				
				var right = new TextArea ();
				
				if (parent == null) {
					/*var container = new HBoxArea ();
					container.AddArea(area);
					container.AddArea(operation);
					container.AddArea(right); */
					
					return;
				} else {
					parent.AddArea (operation);	
					parent.AddArea (right);
				}
				
				Region.ActiveArea = right;
				
				break;
			case Gdk.Key.slash:			
				ProcessSlash ();				
				break;
			}			
		}
		
		private void ProcessSlash ()
		{
			var parent = Region.ActiveArea.Parent;
			var dividend = Region.ActiveArea;
			var divisor = new TextArea ();
				
			var divideArea = new DevideArea (dividend, divisor);
				
			if (parent is HBoxArea) {
				var hbox = parent as HBoxArea;
				hbox.Replace (Region.ActiveArea, divideArea);
			}
				
			Region.ActiveArea = divisor;
		}
	}
}

