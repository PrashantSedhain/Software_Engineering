using System;
using System.Collections.Generic;
using System.Diagnostics;
using HPlusSports.Services;
using Xamarin.Forms;

namespace HPlusSports
{
    public partial class OrderForm : TabbedPage
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        public OrderForm(Order target)
        {
            InitializeComponent();
            BindingContext = target;
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            Order o = BindingContext as Order;

            o.Quantity = Int32.Parse(QuantityStepper.Text);
            if (!ProductService.OrderHistory.ContainsKey(o.Product.Id))
            {
                ProductService.OrderHistory.Add(o.Product.Id, new List<Tuple<DateTime, int>>());
            }
            ProductService.OrderHistory[o.Product.Id].Add(new Tuple<DateTime, int>(o.Time, o.Quantity));
            await ProductService.SaveOrderHistory();
            
            await DisplayAlert("Order Placed", $"Order placed for {o.Quantity} of {o.Product.Name}", "OK");
            
            await Navigation.PopToRootAsync();
            
        }
    }
}
