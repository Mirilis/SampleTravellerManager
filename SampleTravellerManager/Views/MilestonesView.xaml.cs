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
            Messenger.Default.Register<RequestLoadTravellerDialog>(this, (action) => { var v = new LoadTravellerView(); v.Show(); });
            Messenger.Default.Register<RequestCloseTravellersWindow>(this, (action) => this.Close());
            Messenger.Default.Register<RequestLoadDeleteTravellerDialog>(this, (action) => { var v = new DeleteTravellerView(); v.Show(); });
            Messenger.Default.Register<RequestLoadCopyTravellerDialog>(this, (action) => { var v = new CopyTravellerView(); v.Show(); });
            Messenger.Default.Register<RequestOpenQuestionsWindow>(this, (action) => LoadQuestionsViewExecute(action));
            
        }

        private void LoadQuestionsViewExecute(RequestOpenQuestionsWindow action)
        {
            QuestionsView v;
            if (action.Question != null)
            {
                v = new QuestionsView(action.Question);
            }
            else
            {
                v = new QuestionsView();
            }
            v.Show();
        }

      
    }
}
