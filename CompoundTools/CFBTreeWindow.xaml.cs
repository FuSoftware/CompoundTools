using CompoundTools.CFB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompoundTools
{
    /// <summary>
    /// Interaction logic for CFBTreeWindow.xaml
    /// </summary>
    public partial class CFBTreeWindow : Window
    {
        public CFBTreeWindow(CFBFile file)
        {
            InitializeComponent();
            this.Load(file.RootElement);
        }

        void Load(CFBElement Root)
        {
            this.tvItem.Items.Clear();
            this.tvItem.Items.Add(LoadElement(Root));
        }

        TreeViewItem LoadElement(CFBElement Element)
        {
            TreeViewItem item = new TreeViewItem();

            item.Header = Element.Entry.Name;

            foreach(CFBElement elem in Element.Children)
            {
                item.Items.Add(LoadElement(elem));
            }

            return item;
        }
    }
}
