//---------------------------------------------------------------------------
//
// <copyright file="SearchPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/31/2016 2:26:24 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using RODINInfo.ViewModels;

namespace RODINInfo.Pages
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            ViewModel = new SearchViewModel();
            this.InitializeComponent();
        }
        public SearchViewModel ViewModel { get; private set; }
		protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.SearchDataAsync(e.Parameter.ToString());
        }
    }    
}
