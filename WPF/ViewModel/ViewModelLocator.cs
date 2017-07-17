/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPF"
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
using WPF.Implement;
using WPF.Interface;
using Core.Interface;
using Core.Implement;


namespace WPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
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

            SimpleIoc.Default.Register<IDataService, DataService>(true);
            SimpleIoc.Default.Register<ICoreOcr, CoreOcr>(true);
            SimpleIoc.Default.Register<IDataExchangeViewModel,DataExchangeViewModel>(true);


            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public SearchViewModel Sea
        {
            get { return ServiceLocator.Current.GetInstance<SearchViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        public static void UnRegisterSearchViewModel()
        {
            SimpleIoc.Default.Unregister<SearchViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
        }
    }
}