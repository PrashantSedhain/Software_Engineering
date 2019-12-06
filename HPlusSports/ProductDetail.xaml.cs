using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HPlusSports
{
    public partial class ProductDetail : ContentPage
    {
        public ProductDetail()
        {
            InitializeComponent();
        }
        public ProductDetail(Services.Product product)
        {
            InitializeComponent();
            BindingContext = product;
        }

        public void HandleOrderClick(object sender, EventArgs e)
        {
            Services.Product p = BindingContext as Services.Product;
            Navigation.PushAsync(new OrderForm(
                new Services.Order { ProductName = p.Name, Quantity = 1 }));
        }

        public void HandleFavoriteClick(object sender, EventArgs e)
        {
            Services.Product p = BindingContext as Services.Product;

            // TODO: Add product to favorites.

            DisplayAlert("Added to Favorites", $"Added {p.Name} to favorites", "OK");
        }
    }
}
