/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SampletravelerManager"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace SampleTravelerManager.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<QuestionsViewModel>();
            SimpleIoc.Default.Register<MilestonesViewModel>();
        }

        #endregion Public Constructors

        #region Public Properties

        public static MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static MilestonesViewModel Milestones
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MilestonesViewModel>();
            }
        }

        public static QuestionsViewModel Questions
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionsViewModel>();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        #endregion Public Methods
    }
}