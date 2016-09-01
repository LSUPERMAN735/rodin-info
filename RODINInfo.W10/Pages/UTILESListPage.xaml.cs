//---------------------------------------------------------------------------
//
// <copyright file="UTILESListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/31/2016 2:26:24 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.DynamicStorage;
using RODINInfo.Sections;
using RODINInfo.ViewModels;
using AppStudio.Uwp;

namespace RODINInfo.Pages
{
    public sealed partial class UTILESListPage : Page
    {
		public GroupedListViewModel ViewModel { get; set; }
        public UTILESListPage()
        {
			ViewModel = ViewModelFactory.NewListGrouped(new UTILESSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("4c48d6af-5ce4-4569-aaf8-ccc19496d806");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
