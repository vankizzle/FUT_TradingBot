using FUTAutomationAPI;
using FUTAutomationAPI.Interfaces;
using FUTTradingApp.Helpers;
using FUTTradingApp.ViewModels.Pages;
using FUTTradingApp.Views.Pages;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace FUTTradingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            Views.Windows.MainWindow mainWindow = this.Container.Resolve<Views.Windows.MainWindow>();
            return mainWindow;
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            IRegionManager regionManager = this.Container.Resolve<IRegionManager>();

            regionManager.RequestNavigate(Regions.MainRegion, nameof(LoginScreenPage));
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            // Adjusted convention mapping:
            // The "Views" and "ViewModels" folders should have the same structure (namespace structure to be correct),
            // but also allow for subfolders, and View folder nesting. Valid mappings:
            // Views.Pages.MainPage => ViewModels.Pages.MainPageViewModel
            // Views.Pages.MainPage => ViewModels.Pages.MainPage.MainPageViewModel
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                string viewTypeFullName = viewType.FullName;

                // ReSharper disable once PossibleNullReferenceException
                if (viewTypeFullName.EndsWith("View"))
                {
                    viewTypeFullName = viewTypeFullName.Substring(0, viewTypeFullName.Length - "View".Length);
                }

                var regex = new Regex(Regex.Escape(".Views."));
                string viewModelNameBase = regex.Replace(viewTypeFullName, ".ViewModels.", 1);

                var controlsRegex = new Regex(Regex.Escape(".Controls."));
                viewModelNameBase = controlsRegex.Replace(viewModelNameBase, ".ViewModels.", 1);

                string directMapViewModelTypeName = viewModelNameBase + "ViewModel";
                string directMapViewModelTypeFullName = string.Format(CultureInfo.InvariantCulture,
                                                                   $"{directMapViewModelTypeName}, {viewAssemblyName}");

                int lastDotIndex = viewModelNameBase.LastIndexOf(".", StringComparison.Ordinal);
                string insert = viewModelNameBase.Insert(lastDotIndex, $".{viewType.Name}");
                string subFolderViewModelTypeName = insert + "ViewModel";
                string subFolderViewModelTypeFullName = string.Format(CultureInfo.InvariantCulture,
                                                                   $"{subFolderViewModelTypeName}, {viewAssemblyName}");

                Type resultType = Type.GetType(directMapViewModelTypeFullName) ??
                                  Type.GetType(subFolderViewModelTypeFullName);

                return resultType;
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LoginScreenPageViewModel>();

            containerRegistry.Register<IFutAPI, FutAPI>();

            containerRegistry.RegisterForNavigation<LoginScreenPage>();
        }
    }
}