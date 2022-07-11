using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    internal class MenuStrategy : IStrategy
    {
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new StarterControl();
        }
    }
}
