using KDASharedLibrary.DataAccess;
using KDASharedLibrary.Enums;
using KDASharedLibrary.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KDAFileViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var data = BinaryConnector.StaticLoad<KeystrokeData[]>(@"C:\Users\mhdb9\Desktop\data-file_21.kdf");
            foreach (var key in GetValues<KeysList>())
            {
                list.Items.Add(new TreeViewItem());
                ((TreeViewItem)list.Items[list.Items.Count - 1]).Header = key;
            }
            foreach (TreeViewItem item in list.Items)
            {
                item.Items.Add(new TextBlock().Text = "test");
            }
        }

        public static List<string> GetValues<T>()
        {
            List<string> values = new List<string>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(Enum.GetName(typeof(T), itemType));
            }
            return values;
        }
    }
}
