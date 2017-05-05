using GalaSoft.MvvmLight.Messaging;
using NWCSampleManager;
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
    /// Interaction logic for QuestionsView.xaml
    /// </summary>
    public partial class QuestionsView : Window
    {
        #region Public Constructors

        public QuestionsView()
        {
            InitializeComponent();

            Messenger.Default.Register<RequestCloseQuestionsWindow>(this, (action) => CloseWindow());
            Messenger.Default.Register<RequestOpenLoadQuestionDialog>(this, (action) => OpenLoadQuestionsDialog());
        }

        public QuestionsView(Question question) : this()
        {
            ((ViewModel.QuestionsViewModel)DataContext).SelectedQuestion = question;
            ((ViewModel.QuestionsViewModel)DataContext).CommandOpenSelectedQuestion.Execute(null);
        }

        #endregion Public Constructors

        #region Private Methods

        private static void OpenLoadQuestionsDialog()
        {
            var a = new LoadQuestionView();
            a.Show();
        }

        private void CloseWindow()
        {
            Messenger.Default.Unregister(this);
            this.Close();
        }

        #endregion Private Methods
    }
}