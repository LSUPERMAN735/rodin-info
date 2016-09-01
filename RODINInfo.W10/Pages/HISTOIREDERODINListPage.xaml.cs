//---------------------------------------------------------------------------
//
// <copyright file="HISTOIREDERODINListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/31/2016 2:26:24 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Html;
using RODINInfo.Sections;
using RODINInfo.ViewModels;
using AppStudio.Uwp;

namespace RODINInfo.Pages
{
    public sealed partial class HISTOIREDERODINListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }

        private DataTransferManager _dataTransferManager;

		#region	HtmlContent
		public string HtmlContent
        {
            get { return (string)GetValue(HtmlContentProperty); }
            set { SetValue(HtmlContentProperty, value); }
        }

		public static readonly DependencyProperty HtmlContentProperty = DependencyProperty.Register("HtmlContent", typeof(string), typeof(HISTOIREDERODINListPage), new PropertyMetadata(string.Empty));
		#endregion
        public HISTOIREDERODINListPage()
        {
			ViewModel = ViewModelFactory.NewList(new HISTOIREDERODINSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("57287760-1297-43b6-955b-65f8b9caac54");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
			
			if (ViewModel.Items != null && ViewModel.Items.Count > 0)
			{
                HtmlContent = ViewModel.Items[0].Content;
            }			
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;		
            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
