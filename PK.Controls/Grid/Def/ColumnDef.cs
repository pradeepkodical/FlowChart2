using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PK.Controls.Grid.Editors;

namespace PK.Grid.Def
{
    public class ColumnDef<T>
    {
        public string HeaderText { get; set; }
        public float Width { get; set; }
        public float Flex { get; set; }
        public Func<T, string> CellRenderer { get; set; }
        public Editor<T> Editor { get; set; }
    }
}
