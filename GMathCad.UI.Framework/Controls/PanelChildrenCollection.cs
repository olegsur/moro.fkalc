using System;
using System.Collections.Generic;
using System.Collections;

namespace GMathCad.UI.Framework
{
	public class PanelChildrenCollection<T> : IList<T> where T:UIElement
	{
		private Visual parent;
		private List<T> children = new List<T>();	
		
		public PanelChildrenCollection (Visual parent)
		{
			this.parent = parent;
		}

		#region IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return children.GetEnumerator ();
		}
		#endregion

		#region IEnumerable implementation
		public IEnumerator<T> GetEnumerator ()
		{
			return children.GetEnumerator ();
		}
		#endregion

		#region ICollection implementation
		public void Add (T item)
		{
			parent.AddVisualChild (item);
			children.Add (item);
		}

		public void Clear ()
		{
			foreach (var item in children)
				parent.RemoveVisualChild (item);
			
			children.Clear ();
		}

		public bool Contains (T item)
		{
			return children.Contains (item);
		}

		public void CopyTo (T[] array, int arrayIndex)
		{
			children.CopyTo (array, arrayIndex);
		}

		public bool Remove (T item)
		{
			var removed = children.Remove (item);
			
			if (removed)
				parent.RemoveVisualChild (item);
			
			return removed; 
		}

		public int Count {
			get {
				return children.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion

		#region IList implementation
		public int IndexOf (T item)
		{
			return children.IndexOf (item);
		}

		public void Insert (int index, T item)
		{
			parent.AddVisualChild (item);
			children.Insert (index, item);
		}

		public void RemoveAt (int index)
		{
			parent.RemoveVisualChild (this [index]);
			children.RemoveAt (index);
		}

		public T this [int index] {
			get {
				return children [index];
			}
			set {
				Insert (index, value);
			}
		}
		#endregion		
	}
}