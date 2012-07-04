// 
// PlusAction.cs
//  
// Author:
//       Oleg Sur <oleg.sur@gmail.com>
// 
// Copyright (c) 2012 Oleg Sur
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using fkalc.Tokens;

namespace fkalc.UI
{
	public class PlusAction
	{
		private MathRegion Region { get; set; }
		
		public PlusAction (MathRegion region)
		{
			Region = region;
		}
		
		public void Execute ()
		{
			var operation = new PlusToken ();			
				
			var right = new TextToken ();

			if (Region.Selection.SelectedToken is TextToken) {
				var textToken = Region.Selection.SelectedToken as TextToken;

				if (textToken.Parent is HBoxToken) {
					var parent = Region.Selection.SelectedToken.Parent as HBoxToken;

					var l = GetLeftString (textToken.Text);
					var left = new TextToken ();
					if (!string.IsNullOrEmpty (l))
						left.Text = l;

					var r = GetRightString (textToken.Text);
					if (!string.IsNullOrEmpty (r))
						right.Text = r;

					var index = parent.Tokens.IndexOf (textToken);

					parent.Tokens [index] = left;
					parent.Tokens.Insert (index + 1, operation);	
					parent.Tokens.Insert (index + 2, right);

				} else {
					var parent = Region.Selection.SelectedToken.Parent;
				
					var container = new HBoxToken ();
					parent.Replace (textToken, container);

					var left = new TextToken ()
					{
						Text = textToken.Text.Substring(0, Region.Selection.Position)
					};

					container.Add (left);
					container.Add (operation);
					container.Add (right);
				}
			} else if (Region.Selection.SelectedToken.Parent is HBoxToken) {				
				var parent = Region.Selection.SelectedToken.Parent as HBoxToken;	

				parent.Add (operation);	
				parent.Add (right);

				//parent.Insert ();
			} else {				
				var parent = Region.Selection.SelectedToken.Parent;
				
				var container = new HBoxToken ();				
				var token = Region.Selection.SelectedToken;
				
				parent.Replace (token, container);
				
				container.Add (token);
				container.Add (operation);
				container.Add (right);
			}
				
			Region.Selection.SelectedToken = right;
		}

		private string GetLeftString (string text)
		{
			if (string.IsNullOrEmpty (text))
				return null;

			return text.Substring (0, Region.Selection.Position);
		}

		private string GetRightString (string text)
		{
			if (string.IsNullOrEmpty (text))
				return null;

			return text.Substring (Region.Selection.Position, text.Length - Region.Selection.Position);
		}
	}
}