using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace SampleTravellerManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
            private RelayCommand _CreateNewSample;

        /// <summary>
        /// Gets the.
        /// </summary>
        public RelayCommand CreateNewSample
        {
            get
            {
                return _CreateNewSample
                    ?? (_CreateNewSample = new RelayCommand(
                    () =>
                    {

                    },
                    () => true));
            }
        }

        private RelayCommand _ManageQuestions;

        /// <summary>
        /// Gets the ManageQuestions.
        /// </summary>
        public RelayCommand ManageQuestions
        {
            get
            {
                return _ManageQuestions
                    ?? (_ManageQuestions = new RelayCommand(
                    () =>
                    {

                    },
                    () => true));
            }
        }
        
    }
}