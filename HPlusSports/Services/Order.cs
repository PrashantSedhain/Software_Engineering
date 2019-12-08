using System;
using System.ComponentModel;

namespace HPlusSports.Services
{
    public class Order : INotifyPropertyChanged
    {
        public Order()
        {
            Time = DateTime.Now;
        }

        public Product Product { get; set; }

        public DateTime Time { get; set; }

        private int qty;

        public int Quantity
        {
            get { return qty; }
            set{
                if(value != qty)
                {
                    qty = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Quantity"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
