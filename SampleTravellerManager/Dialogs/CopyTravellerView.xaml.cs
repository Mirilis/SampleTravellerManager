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
using GalaSoft.MvvmLight.Messaging;
using SampleTravellerManager.Messages;

namespace SampleTravellerManager.Dialogs
{
    /// <summary>
    /// Interaction logic for LoadTravellerView.xaml
    /// </summary>
    public partial class CopyTravellerView : Window
    {
        public Converters.DialogType DialogType;
        public CopyTravellerView(Converters.DialogType t)
        {
            InitializeComponent();
            Messenger.Default.Register<RequestCloseCopyTravellerDialog>(this, (action) => this.Close());
            this.DialogType = t;
        }

        public CopyTravellerView()
        {
            InitializeComponent();
            Messenger.Default.Register<RequestCloseCopyTravellerDialog>(this, (action) => this.Close());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
