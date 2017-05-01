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
    /// Interaction logic for LoadQuestionView.xaml
    /// </summary>
    public partial class LoadQuestionView : Window
    {
        public LoadQuestionView()
        {
            InitializeComponent();
            Messenger.Default.Register<RequestCloseQuestionsDialog>(this, (action) => CloseWindow());
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
