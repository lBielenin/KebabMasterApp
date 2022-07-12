﻿using System;
using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    internal class OrderViewerStrategy : IStrategy
    {
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new OrderViewerControl();
        }
    }
}
