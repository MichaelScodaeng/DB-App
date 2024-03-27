using DB_App.Core;
using DB_App.MVMM.View;
using System;
using System.Windows;
namespace DB_App.MVMM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _currentView;
        public HomeViewModel HomeVm { get; set; }
        public DB1ViewModel DB1View { get; set; }
        public DB2ViewModel DB2View { get; set; }
        public DB3ViewModel DB3View { get; set; }
        public DB4ViewModel DB4View { get; set; }
        public DB5ViewModel DB5View { get; set; }
        public AboutViewModel AboutView { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DB1ViewCommand { get; set; }
        public RelayCommand DB2ViewCommand { get; set; }
        public RelayCommand DB3ViewCommand { get; set; }
        public RelayCommand DB4ViewCommand { get; set; }
        public RelayCommand DB5ViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }

        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            DB1View = new DB1ViewModel();
            DB2View = new DB2ViewModel();
            DB3View = new DB3ViewModel();
            DB4View = new DB4ViewModel();
            DB5View = new DB5ViewModel();
            AboutView = new AboutViewModel();
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVm; });
            DB1ViewCommand = new RelayCommand(o => { CurrentView = DB1View; });
            DB2ViewCommand = new RelayCommand(o => { CurrentView = DB2View; });
            DB3ViewCommand = new RelayCommand(o => { CurrentView = DB3View; });
            DB4ViewCommand = new RelayCommand(o => { CurrentView = DB4View; });
            DB5ViewCommand = new RelayCommand(o => { CurrentView = DB5View; });
            AboutViewCommand = new RelayCommand(o => { CurrentView = AboutView; });

        }
    }

}
