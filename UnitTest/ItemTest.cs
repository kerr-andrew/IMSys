using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMSys;
using System.Collections.ObjectModel;

namespace IMSysTests
{
    [TestClass]
    public class AddItemTest
    {
        [TestMethod]
        public void GetItemsReturnsCorrectObject()
        {
            //arrange
            ObservableCollection<Item> item = Item.GetItems();
        }
    }
}
