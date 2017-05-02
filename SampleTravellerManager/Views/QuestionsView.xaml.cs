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
using NWCSampleManager;
using SampleTravellerManager.Messages;

namespace SampleTravellerManager.Views
{
    /// <summary>
    /// Interaction logic for QuestionsView.xaml
    /// </summary>
    public partial class QuestionsView : Window
    {
        private Question question;

        public QuestionsView()
        {
            InitializeComponent();
            Messenger.Default.Register<RequestCloseQuestionsWindow>(this, (action) => CloseWindow());
            Messenger.Default.Register<RequestLoadQuestionDialog>(this, (action) => OpenLoadQuestionsDialog());
            
            
        }

        public QuestionsView(Question question)
        {
            this.question = question;
            InitializeComponent();
            Messenger.Default.Register<RequestCloseQuestionsWindow>(this, (action) => CloseWindow());
            Messenger.Default.Register<RequestLoadQuestionDialog>(this, (action) => OpenLoadQuestionsDialog());
            ((ViewModel.QuestionsViewModel)DataContext).SelectedQuestion = question;
            ((ViewModel.QuestionsViewModel)DataContext).OpenSelectedQuestion();
            
        }

        private void OpenLoadQuestionsDialog()
        {
            var a = new LoadQuestionView();
            a.Show();
        }

        private void CloseWindow()
        {
            this.Close();
        }
    }
}
