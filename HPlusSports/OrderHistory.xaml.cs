using HPlusSports.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HPlusSports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderHistory : ContentPage
    {
        public ObservableCollection<Tuple<DateTime, int>> Items { get; set; }

        public OrderHistory(Product p)
        {
            InitializeComponent();

            Items = new ObservableCollection<Tuple<DateTime, int>>(ProductService.OrderHistory[p.Id]);

            MyListView.ItemsSource = Items;
        }
    }
}
