using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class SelectionKeeper
    {
    }

    public class ParentItem
    {
        public ParentItem()
        {
            Children = new List<ChildItem>();
        }
        public string ItemName { get; set; }
        public List<ChildItem> Children { get; set; }
        public bool IsAnyChildSelected
        {
            get
            {
                return Children != null && Children.Exists(c => c.Selected);
            }
        }
    }
    public class ChildItem
    {
        public string ItemName { get; set; }
        public bool Selected { get; set; }
    }
}
