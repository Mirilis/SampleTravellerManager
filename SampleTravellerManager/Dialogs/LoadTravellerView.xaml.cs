using GalaSoft.MvvmLight.Messaging;
using SampleTravelerManager.Messages;
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

namespace SampleTravelerManager.Dialogs
{
    /// <summary>
    /// Interaction logic for LoadtravelerView.xaml
    /// </summary>
    public partial class LoadTravelerView : Window
    {
        #region Public Constructors

        public LoadTravelerView()
        {
            InitializeComponent();

            Messenger.Default.Register<RequestClosetravelersDialog>(this, (action) => Button_Click(null, null));
        }

        #endregion Public Constructors

        #region Private Methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this);
            this.Close();
        }

        #endregion Private Methods
    }
}