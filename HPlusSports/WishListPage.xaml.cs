using HPlusSports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HPlusSports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishListPage : ContentPage
    {
        public List<Product> Products { get; set; }

        public WishListPage()
        {
            InitializeComponent();
            Products = ProductService.GetWishListProducts();
            BindingContext = Products;
        }

        public void Item_Selected(object sender, SelectionChangedEventArgs e)
        {
            Product product = e.CurrentSelection.First() as Product;
            Navigation.PushAsync(new ProductDetail(product));
        }
    }
}