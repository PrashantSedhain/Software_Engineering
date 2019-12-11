using System;
using System.Collections.Generic;
using System.IO;

using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

namespace HPlusSports.Services
{
    public static class ProductService
    {
        static HttpClient Client;

        public static List<int> WishList
        {
            get;
            set;
        }

        public static Dictionary<int, List<Tuple<DateTime, int>>> OrderHistory { get; set; }

        static ProductService()
		{
            Client = new HttpClient { BaseAddress = new Uri("https://hplussport.com/api/") };
            WishList = new List<int>();
            OrderHistory = new Dictionary<int, List<Tuple<DateTime, int>>>();
		}

        public static async Task<List<Product>> GetProductsAsync()
		{
            var productsRaw = Client.GetStringAsync("products/").Result;

            var serializer = new JsonSerializer();
            using(var tReader = new StringReader(productsRaw))
            {
                using(var jReader = new JsonTextReader(tReader))
                {
                    var products = serializer.Deserialize<List<Product>>(jReader);
                    await SetProductPrices(products);
                    return products;
                }
            }
		}

        private static async Task SetProductPrices(List<Product> products)
        {
            foreach (Product p in products)
            {
                p.Price = GetProductPrice(p.Id);
            }
        }

        public static List<Product> GetWishListProducts()
        {
            var products = GetProductsAsync().Result;
            var wishListProducts = new List<Product>();

            
            foreach (Product p in products)
            {
                if (WishList.Contains(p.Id))
                {
                    wishListProducts.Add(p);
                }
            }

            return wishListProducts;

        }

        public static async Task SaveWishList()
        {
            if (WishList != null && WishList.Count > 0)
            {
                Application.Current.Properties["wishlist"] = JsonConvert.SerializeObject(WishList); ;
                await Application.Current.SavePropertiesAsync();
            }
        }

        public static async Task LoadWishList()
        {
            WishList = JsonConvert.DeserializeObject<List<int>>((string)Application.Current.Properties["wishlist"]);
        }

        public static async Task SaveOrderHistory()
        {
            Application.Current.Properties["order_history"] = JsonConvert.SerializeObject(OrderHistory);
            await Application.Current.SavePropertiesAsync();
        }

        public static async Task LoadOrderHistory()
        {
            OrderHistory = JsonConvert.DeserializeObject<Dictionary<int, List<Tuple<DateTime, int>>>>((string)Application.Current.Properties["order_history"]);
        }

        public static double GetProductPrice(int Id)
        {
            var productsRaw = Client.GetStringAsync("products/id/" + Id).Result;
            var products = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(productsRaw);

            return Double.Parse(products[0]["price"]);
        }
    }
}
