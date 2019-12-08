using HPlusSports.Services;
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

            if (Services.ProductService.WishList.Contains(product.Id))
            {
                favoriteBtn.Text = "- Wishlist";
            }

            if (!ProductService.OrderHistory.ContainsKey(product.Id))
            {
                historyBtn.IsEnabled = false;
            }
        }

        public void HandleOrderClick(object sender, EventArgs e)
        {
            Services.Product p = BindingContext as Services.Product;
            Navigation.PushAsync(new OrderForm(
                new Services.Order { Product = p, Quantity = 1 }));
        }

        public void HandleHistoryClick(object sender, EventArgs e)
        {
            Product p = BindingContext as Product;
            Navigation.PushAsync(new OrderHistory(p));
        }

        public async void HandleFavoriteClick(object sender, EventArgs e)
        {
            Services.Product p = BindingContext as Services.Product;

            if (!Services.ProductService.WishList.Contains(p.Id))
            {
                favoriteBtn.Text = "- Wishlist";
                Services.ProductService.WishList.Add(p.Id);
            }
            else
            {
                favoriteBtn.Text = "+ Wishlist";
                Services.ProductService.WishList.Remove(p.Id);
            }

            await Services.ProductService.SaveWishList();
        }
    }
}
