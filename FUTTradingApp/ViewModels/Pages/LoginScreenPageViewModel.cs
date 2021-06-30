using FUTAutomationAPI.Helpers;
using FUTAutomationAPI.Interfaces;
using FUTTradingApp.Commands;
using FUTTradingApp.Helpers;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Windows;

namespace FUTTradingApp.ViewModels.Pages
{
    public class LoginScreenPageViewModel : BindableBase
    {
        private string _email;
        private string _password;
        private RelayCommand _loginCommand;

        private IFutAPI _futAPI;
        private readonly IRegionManager _regionManager;

        public LoginScreenPageViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;

            _futAPI = (Application.Current as PrismApplication).Container.Resolve<IFutAPI>();
            _futAPI.LoginPage.GoTo();
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    RaisePropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged(nameof(Password));
                }
            }
        }

        public RelayCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new RelayCommand(LoginToApp));

        private void LoginToApp()
        {
            if (string.IsNullOrEmpty(Email))
                return;

            if (string.IsNullOrEmpty(Password))
                return;

            _futAPI.LoginPage.Login(new LoginOptions { Email = Email, Password = Password, Token = string.Empty });

             NavigateTo2FAuth();
        }

        private void NavigateTo2FAuth()
        {
            this._regionManager.RequestNavigate(Regions.MainPageContentRegion, nameof(Views.Pages.TwoFactorAuthPage));
        }
    }
}