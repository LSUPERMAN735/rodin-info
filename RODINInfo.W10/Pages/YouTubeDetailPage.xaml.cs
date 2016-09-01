//---------------------------------------------------------------------------
//
// <copyright file="YouTubeDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/31/2016 2:26:24 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.YouTube;
using RODINInfo.Sections;
using RODINInfo.Navigation;
using RODINInfo.ViewModels;
using AppStudio.Uwp;

namespace RODINInfo.Pages
{
    public sealed partial class YouTubeDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public YouTubeDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new YouTubeSection());
			this.ViewModel.ShowInfo = false;
            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;
            ShellPage.Current.SupportFullScreen = true;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;
            ShellPage.Current.SupportFullScreen = false;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
