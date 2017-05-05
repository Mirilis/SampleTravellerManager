using GalaSoft.MvvmLight.Messaging;
using SampleTravelerManager.Dialogs;
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

namespace SampleTravelerManager.Views
{
    /// <summary>
    /// Interaction logic for MilestonesView.xaml
    /// </summary>
    public partial class MilestonesView : Window
    {
        #region Public Constructors

        public MilestonesView()
        {
            InitializeComponent();

            Messenger.Default.Register<RequestOpenLoadtravelerDialog>(this, (action) => { var v = new LoadTravelerView(); v.Show(); });
            Messenger.Default.Register<RequestCloseTravelersWindow>(this, (action) => { Messenger.Default.Unregister(this); this.Close(); });
            Messenger.Default.Register<RequestOpenDeleteTravelerDialog>(this, (action) => { var v = new DeleteTravelerView(); v.Show(); });
            Messenger.Default.Register<RequestOpenCopyTravelerDialog>(this, (action) => { var v = new CopyTravelerView(); v.Show(); });
            Messenger.Default.Register<RequestOpenQuestionsWindow>(this, (action) => LoadQuestionsViewExecute(action));
            Messenger.Default.Register<MinimizeWindows>(this, (action) => { this.WindowState = WindowState.Minimized; });
        }

        #endregion Public Constructors

        #region Private Methods

        private static void LoadQuestionsViewExecute(RequestOpenQuestionsWindow action)
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

        #endregion Private Methods
    }
}