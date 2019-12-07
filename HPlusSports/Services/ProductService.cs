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
        static HttpClient client;

        public static List<int> WishList
        {
            get;
            set;
        }

        static ProductService()
		{
            client = new HttpClient { BaseAddress = new Uri("https://hplussport.com/api/") };
            WishList = new List<int>();

		}

        public static async Task<List<Product>> GetProductsAsync()
		{
            var productsRaw = await client.GetStringAsync("products/");

            var serializer = new JsonSerializer();
            using(var tReader = new StringReader(productsRaw))
            {
                using(var jReader = new JsonTextReader(tReader))
                {
                    var products = serializer.Deserialize<List<Product>>(
                        jReader);

                    return products;
                }
            }
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
    }
}
