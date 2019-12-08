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
        }

        public void HandleOrderClick(object sender, EventArgs e)
        {
            Services.Product p = BindingContext as Services.Product;
            Navigation.PushAsync(new OrderForm(
                new Services.Order { Product = p, Quantity = 1 }));
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
