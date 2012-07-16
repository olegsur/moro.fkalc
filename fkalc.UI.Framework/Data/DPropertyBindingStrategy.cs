//
// DPropertyBindingStrategy.cs
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
	public class DPropertyBindingStrategy
	{
		private bool updating = false;
		private IDependencyProperty Source { get; set; }
		private IDependencyProperty Target { get; set; }
		private IValueConverter Converter { get; set; }

		public DPropertyBindingStrategy (IDependencyProperty source, IDependencyProperty target, IValueConverter converter)
		{
			Source = source;
			Target = target;
			Converter = converter;

			Target.Value = Converter.Convert (Source.Value);

			source.DependencyPropertyValueChanged += HandleSourceValueChanged;
			target.DependencyPropertyValueChanged += HandleTargetValueChanged;
		}

		private void HandleSourceValueChanged (object sender, DPropertyValueChangedEventArgs e)
		{
			if (updating)
				return;

			updating = true;

			Target.Value = Converter.Convert (e.NewValue);

			updating = false;
		}

		private void HandleTargetValueChanged (object sender, DPropertyValueChangedEventArgs e)
		{
			if (updating)
				return;

			updating = true;

			Source.Value = Converter.ConvertBack (e.NewValue);

			updating = false;
		}
	}
}

