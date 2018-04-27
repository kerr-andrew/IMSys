using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSys
{
    public abstract class IInventoryColumnEnum
    {
        public sealed class liId : IInventoryColumnEnum
        {
            public static string Name { get { return "liId"; } }
        }
        public sealed class itemName : IInventoryColumnEnum
        {
            public static string Name { get { return "itemName"; } }
        }
        public sealed class Price : IInventoryColumnEnum
        {
            public static string Name { get { return "Price"; } }
        }
        public sealed class Quantity : IInventoryColumnEnum
        {
            public static string Name { get { return "Quantity"; } }
        }
        public sealed class Unit : IInventoryColumnEnum
        {
            public static string Name { get { return "Unit"; } }
        }
    }
}
