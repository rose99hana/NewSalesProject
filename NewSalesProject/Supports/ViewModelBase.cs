using MaterialDesignThemes.Wpf;
using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NewSalesProject.Supports
{
    public class ViewModelBase : NotifyUIBase
    {
        #region Fields
        protected IViewModel _currentViewModel;
        protected ObservableCollection<IViewModel> _viewModels;
        protected ICommand _changeViewCommand;
        protected int indexOfCurrentViewModel;
        #endregion

        public ViewModelBase()
        {
        }

        public PackIcon PackIconImage { get; set; } = new PackIcon();

        protected void SetMenuImage(PackIconKind packIconKind)
        {
            PackIconImage.Height = 30;
            PackIconImage.Width = 30;
            PackIconImage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            PackIconImage.Kind = packIconKind;
        }


        #region IViewModel Interface implement

        public string Name { get; set; }

        protected bool _changeViewCommandIsEnable = true;
        public bool ChangeViewCommandIsEnable
        {
            get
            {
                return _changeViewCommandIsEnable;
            }
            set
            {
                _changeViewCommandIsEnable = value;
                OnPropertyChanged("ChangeViewCommandIsEnable");
            }
        }

        #endregion



        public IViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
            }
        }

        public ObservableCollection<IViewModel> ViewModels
        {
            get
            {
                if (_viewModels == null)
                    _viewModels = new ObservableCollection<IViewModel>();

                return _viewModels;
            }
        }

        public ICommand ChangeViewCommand
        {
            get
            {
                if (_changeViewCommand == null)
                {
                    _changeViewCommand = new RelayCommand(
                        async p => await ChangeViewModel((IViewModel)p));
                }

                return _changeViewCommand;
            }
        }

        protected async Task ChangeViewModel(IViewModel viewModel)
        {
            ViewModels[indexOfCurrentViewModel].ChangeViewCommandIsEnable = true;
            MainViewMode = ViewModeType.Busy;
            if (!ViewModels.Contains(viewModel))
                ViewModels.Add(viewModel);

            CurrentViewModel = ViewModels
                .FirstOrDefault(vm => vm == viewModel);

            indexOfCurrentViewModel = ViewModels.IndexOf(CurrentViewModel);
            ViewModels[indexOfCurrentViewModel].ChangeViewCommandIsEnable = false;
            await Task.Delay(200);
            OnPropertyChanged("CurrentViewModel");
            MainViewMode = ViewModeType.Default;
        }

        protected ViewModeType _mainViewMode = ViewModeType.Default;
        public ViewModeType MainViewMode
        {
            get { return _mainViewMode; }
            set
            {
                if (_mainViewMode != value)
                {
                    _mainViewMode = value;
                    OnPropertyChanged("MainViewMode");
                }
            }
        }
    }
}
