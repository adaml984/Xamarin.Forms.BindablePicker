using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.BindablePicker.Helpers;

namespace Xamarin.Forms.BindablePicker
{
	public class BindablePicker : Picker
	{
		public BindablePicker()
		{
			this.SelectedIndexChanged += OnSelectedIndexChanged;
		}

		private IList<Tuple<string, object>> _internalItemsSource;
		private IList<Tuple<string, object>> InternalItemsSource
		{
			get
			{
				if (_internalItemsSource == null)
					_internalItemsSource = new List<Tuple<string, object>>();
				return _internalItemsSource;
			}
		}

		public static BindableProperty DisplayedNameProperty = BindableProperty.Create("DisplayedName", typeof(string), typeof(BindablePicker));
		public static BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(BindablePicker), defaultBindingMode:BindingMode.TwoWay);
		public static BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(BindablePicker), propertyChanged: OnItemsSourceChanged);

		public IEnumerable ItemsSource
		{
			get { return GetValue(ItemsSourceProperty) as IEnumerable; }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public string DisplayedName
		{
			get { return GetValue(DisplayedNameProperty) as string; }
			set { SetValue(DisplayedNameProperty, value); }
		}

		private void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedIndex >= InternalItemsSource.Count)
				return;
			SelectedItem = InternalItemsSource.ElementAt(SelectedIndex).Item2;
		}

		private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var xfPicker = bindable as BindablePicker;
			if (xfPicker == null || xfPicker.ItemsSource == null)
				return;
			HandleObservableCollection(xfPicker, oldValue, newValue);
			xfPicker.BuildItems();
		}

		private static void HandleObservableCollection(BindablePicker xfPicker, object oldValue, object newValue)
		{
			var collection = oldValue?.As<INotifyCollectionChanged>();
			xfPicker.RemoveEvents(collection);
			collection = newValue?.As<INotifyCollectionChanged>();
			xfPicker.AddEvents(collection);
		}

		private void AddEvents(INotifyCollectionChanged collection)
		{
			if (collection == null)
				return;
			collection.CollectionChanged += OnCollectionChanged;
		}

		private void RemoveEvents(INotifyCollectionChanged collection)
		{
			if (collection == null)
				return;
			collection.CollectionChanged -= OnCollectionChanged;
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BuildItems();
		}

		private void BuildItems()
		{
			BuildInternalItems();
			Items.Clear();
			foreach (var item in InternalItemsSource)
			{
				Items.Add(item.Item1);
			}
			SelectedIndex = 0;
		}

		private void BuildInternalItems()
		{
			InternalItemsSource.Clear();
			foreach (var item in ItemsSource)
			{
				InternalItemsSource.Add(Tuple.Create(item.GetPropertyValue(DisplayedName).ToString(), item));
			}
		}
	}
}
