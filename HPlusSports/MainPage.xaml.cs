using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPlusSports.Services;
using Xamarin.Forms;

namespace HPlusSports
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public List<Product> Products { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Products = ProductService.GetProductsAsync().Result;
            BindingContext = Products;
        }

        public void Item_Selected(object sender, SelectionChangedEventArgs e)
        {
            Product product = e.CurrentSelection.First() as Product;
            Navigation.PushAsync(new ProductDetail(product));
        }

        public void HandleWishListClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WishListPage());
        }
    }
}
