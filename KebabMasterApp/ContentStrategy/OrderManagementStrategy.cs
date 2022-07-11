using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    internal class OrderManagementStrategy : IStrategy
    {
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new OrderManagementControl();
        }
    }
}
