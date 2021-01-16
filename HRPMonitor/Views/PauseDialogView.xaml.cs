using Caliburn.Micro;
using HRPMonitor.ViewModels;
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

namespace HRPMonitor.Views
{
    /// <summary>
    /// Interaction logic for PauseDialogView.xaml
    /// </summary>
    public partial class PauseDialogView : UserControl
    {
        public PauseDialogView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DataContext = new PauseDialogViewModel(eventAggregator);
        }
    }
}
