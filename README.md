The KebabMasterApp:

The purposue of KebabMaster application is to manage KebabMaster restaurant orders's kiosk, along with order status viewer for customers to see and order manager for staff, which are preparing meals.
After installation, every subsequent has it's own shortcut, which should be used by staff to run on dedicated screen.
Modes are:
- Menu screen: for customers for making order.
- Order management screen (arg: -management): for staff to update order's status. 
  Updating "Complete" status results in deleting order and moving it to database archive.
- Order Viewer (arg: -viewer): for customers for checking order status.

Program requires MS SQL Server database to work, set up script is in folder ./KebabSetUpScript
