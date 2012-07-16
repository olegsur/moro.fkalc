//
// BindingOperations.cs
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

namespace fkalc.UI.Framework
{
	public class PathBindingStrategy
	{
		private string SubPath { get; set; }
		private IDependencyProperty Target { get; set; }
		private IValueConverter Converter { get; set; }

		public PathBindingStrategy (DependencyObject source, string path, IDependencyProperty target, IValueConverter converter)
		{
			if (string.IsNullOrEmpty (path))
				throw new ArgumentException ("invalid path", path);

			var paths = path.Split (new [] { '.'}, 2);

			if (paths.Length == 1) {
				BindingOperations.SetBinding (source.GetProperty (path), target, converter);
				return;
			}

			Target = target;
			Converter = converter;

			SubPath = paths [1];

			var property = source.GetProperty (paths [0]);
			property.DependencyPropertyValueChanged += HandleDependencyPropertyValueChanged;

			if (property.Value != null) {
				BindingOperations.SetBinding (property.Value, SubPath, target, converter);
				return;
			}

		}

		private void HandleDependencyPropertyValueChanged (object sender, DPropertyValueChangedEventArgs e)
		{
			if (e.NewValue == null) {
				BindingOperations.ClearBinding (Target);
				return;
			}

			BindingOperations.SetBinding (e.NewValue, SubPath, Target, Converter);
		}
	}

}

