using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    public interface IStrategy
    {
        void DisplayContent(ContentControl contentRef);    
    }
}
