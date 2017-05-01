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
    /// Interaction logic for MilestonesView.xaml
    /// </summary>
    public partial class MilestonesView : Window
    {
        public MilestonesView()
        {
            InitializeComponent();
            Messenger.Default.Register<RequestLoadTravellerDialog>(this, (action) => OpenLoadTravellersView(action));
            Messenger.Default.Register<RequestOpenQuestionsWindow>(this, (action) => OpenQuestionsView(action));
            Messenger.Default.Register<RequestCloseTravellersWindow>(this, (action) => CloseWindow());
            
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private void OpenQuestionsView(object action)
        {
            var v = new QuestionsView();
            v.Show();
        }

        private void OpenLoadTravellersView(object action)
        {
            var v = new LoadTravellerView();
            v.Show();
            
        }
    }
}
