using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleApp
{
	public class MainPageViewModel : ViewModelBase
	{
		public MainPageViewModel()
		{
			Items = new ObservableCollection<SampleObject>();
			for (int i = 0; i < 100; i++)
			{
				Items.Add(new SampleObject() { Id = i, Name = i.ToString() });
			}
		}
		private IList<SampleObject> _items;
		public IList<SampleObject> Items
		{
			get
			{
				return _items;
			}
			set
			{
				if (value == null)
					return;
				_items = value;
				OnPropertyChanged();
			}
		}

		private SampleObject _selectedItem;
		public SampleObject SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				if (value == null)
					return;
				_selectedItem = value; OnPropertyChanged();
			}
		}
	}
}
