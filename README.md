# IMSys

Our Goal

To create a simplified version of an inventory management system at first that can be expanded upon in future updates to our program. 
Scalability will be a big part and we ewill be trying to add in scalability features where ever possible. This was designed for Alex's 
fathers startup electrical company to help him manage his inventory and his spending better. 

Requirements

1.Display current inventory(Amount, Item Name, Price, Unit, Value, Category) on the screen.2.Adding New Items to the Inventory3.Deleting Items From inventory4.Changing Items Quantities5.Save Values to the Database6.Retrieve Values on Program Startup using Non-Persistent Database Connection7.Sort Columns By Highest to Lowest/Alphabetically Or Reverse8.Search Bar That Filters Results As You Type

It is currently not finished. 

How to use. Currently it takes a bit to setup we will be making this process easier over the next few weeks. (Last edited May 16th 2018)

STEPS TO SETUP
1. Create an mdf file. 
2. Run all of the stored procedures
3. Create querys in the xsd file to make the stored procedures actual methods. 
4. GetCategory needs to return a singular value the rest return notihing. Use existing stored proceudres to create the method and 
give them the default name. 
5. (Optional)Run generate test data. 
It should now work.

TO ADD AN ITEM: Add an item by clicking invetory in the menu bar and selecting add item in the drop down. Then you can fill out a table 
with all the items information. So far we have name, price, quntity, unit, and value which is a price * quantity calculation as well as 
category. Add as many items as you ned then click add at the bottom to add them to your inventory. 

TO DELETE AN ITEM: Delete an item by clicking inventory in the top menu and clicking delete item in the drop down. This brings up a 
list box with all items and a search bar to search the items you need to delete for larger inventories. Select items from this 
listbox and click the >> button to add them to the delete list box. When you have all the items in the delete lsitbox you wish 
to delete click delete. If you need to remove an item from the delete hit the x button after sleecting it in the delete list box.

TO EDIT AN ITEM: Edit an item by selecting the cell you wish to edit and putting in the new name/amount or selecting the new categroy 
from the drop down. You can also edit by right clicking on a cell. This will bring up a custom control that allows you to add a
new amount in the top and see the amount the value changed in the bottom or allow you to enter the amount the value changed by 
and the the new amount in the top. Hitting enter closes the control and sets the value.

TO ADD A CATEGORY: Select manage categories from the inventory drop down menu then click the button that says add category. Enter a
name for the category and click add.

TO DELETE A CATEGORY: Select manage categories from the inventroy drop down menu then select a categroy you wish to edit from the 
dropdown on the top right. After this the delete button is enabled click it. Enter a new categroy from the dropdown. (This is to give
to all the items that had the category you are deleteing so you don't have to manually edit them.) Then clikc the button.

TO RENAME A CATEGORY: Select the manage catgories from the inventory drop down menu then select a category you wish to edit. After 
selecting a category click the now enabled rename button. Enter a new name for the category and click the ok button.

TO SEARCH AN ITEM: Enter text in the search bar in the top right.
