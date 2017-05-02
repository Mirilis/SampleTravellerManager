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

namespace SampleTravellerManager.Views
{
    /// <summary>
    /// Interaction logic for LoadTravellerView.xaml
    /// </summary>
    public partial class DeleteTravellerView : Window
    {
        public DeleteTravellerView()
        {
            InitializeComponent();
            Messenger.Default.Register<RequestCloseDeleteTravellerDialog>(this, (action) => ReceiveMessage(action));
        }

        private void ReceiveMessage(object action)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
