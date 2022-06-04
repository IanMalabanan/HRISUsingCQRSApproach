using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hcom.App.ViewModels.Base
{
    public abstract class ViewModelBase //: ExtendedBindableObject
    {
        //protected readonly IDialogService DialogService;
        //protected readonly INavigationService NavigationService;

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                //RaisePropertyChanged(() => IsBusy);
            }
        }
        private bool  _isOnline;

        public bool IsOnline
        {
            get
            {
                return _isOnline;
            }

            set
            {
                _isOnline = value;
                //RaisePropertyChanged(() => IsOnline);
            }
        }
        public ViewModelBase()
        {
            //DialogService = ViewModelLocator.Resolve<IDialogService>();
            //NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task PageAppearingAsync()
        {
            return Task.FromResult(false);
        }
    }
}
