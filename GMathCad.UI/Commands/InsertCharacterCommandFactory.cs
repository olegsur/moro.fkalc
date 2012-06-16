// 
// InsertCharacterCommandFactory.cs
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
namespace GMathCad.UI
{
	public class InsertCharacterCommandFactory
	{
		public InsertCharacterCommandFactory ()
		{
		}
		
		public bool IsSupported (uint keyval)
		{
			var name = Gdk.Keyval.Name (keyval);			
			
			if (name.Length != 1) return false;
			
			var key = name[0];
			
			return (key >= '0' && key <= '9') ||
				(key >= 'A' && key <= 'Z') ||
				(key >= 'a' && key <= 'z');
		}
		
		public InsertCharacterCommand Build (uint key, MathRegion region)
		{			
			return new InsertCharacterCommand (key, region);
		}
	}
}

