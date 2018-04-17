using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IMSys
{
    class Item
    {
        private string name;
        private int quantity;
        private decimal price;
        private string unit;
        private decimal value;

        Item(string itemName, int itemQuantity, decimal itemPrice, string itemUnit, decimal itemValue)
        {
            itemName = this.name;
            itemQuantity = this.quantity;
            itemPrice = this.price;
            itemUnit = this.unit;
            itemValue = this.value;           
        }
        

    }
}
