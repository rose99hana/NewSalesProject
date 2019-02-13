using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewSalesProject.Views
{
    public class MainViewModel : ViewModelBase
    {
        //#region Fields
        //private IViewModel _currentViewModel;
        //private List<IViewModel> _viewModels;
        //private ICommand _changeViewCommand;
        //private int indexOfCurrentViewModel;
        //#endregion

        public MainViewModel()
        {
            ViewModels.Add(new OverviewViewModel());
            ViewModels.Add(new OrderViewModel());
            ViewModels.Add(new DataUpdateViewModel());
            CurrentViewModel = ViewModels[0];
            CurrentViewModel.ChangeViewCommandIsEnable = false;
        }

        //public IViewModel CurrentViewModel
        //{
        //    get
        //    {
        //        return _currentViewModel;
        //    }
        //    set
        //    {
        //        _currentViewModel = value;
        //        OnPropertyChanged("CurrentViewModel");
        //    }
        //}

        //public List<IViewModel> ViewModels
        //{
        //    get
        //    {
        //        if (_viewModels == null)
        //            _viewModels = new List<IViewModel>();

        //        return _viewModels;
        //    }
        //}

        //public ICommand ChangeViewCommand
        //{
        //    get
        //    {
        //        if (_changeViewCommand == null)
        //        {
        //            _changeViewCommand = new RelayCommand(
        //                p => ChangeViewModel((IViewModel)p),
        //                p => p is IViewModel);
        //        }

        //        return _changeViewCommand;
        //    }
        //}

        //protected override void ChangeViewModel(IViewModel viewModel)
        //{
        //    ViewModels[indexOfCurrentViewModel].ChangeViewCommandIsEnable = true;
        //    MainViewMode = ViewModeType.Busy;

        //    if (!ViewModels.Contains(viewModel))
        //        ViewModels.Add(viewModel);

        //    CurrentViewModel = ViewModels
        //        .FirstOrDefault(vm => vm == viewModel);

        //    indexOfCurrentViewModel = ViewModels.IndexOf(CurrentViewModel);

        //    ViewModels[indexOfCurrentViewModel].ChangeViewCommandIsEnable = false;

        //    MainViewMode = ViewModeType.Default;
        //}
    }
}
