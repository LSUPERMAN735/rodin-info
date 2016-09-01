using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using RODINInfo.Navigation;

namespace RODINInfo.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));

            var menuActions1Items = new List<NavigationItem>();

            menuActions1Items.Add(AppNavigation.NodeFromAction(
				"4c48d6af-5ce4-4569-aaf8-ccc19496d806",
                "fournitures",
				AppNavigation.ActionFromPage("FournituresListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/LOGOMARRODIN.png")) }));

            menuActions1Items.Add(AppNavigation.NodeFromAction(
				"4c48d6af-5ce4-4569-aaf8-ccc19496d806",
                "UTILES",
				AppNavigation.ActionFromPage("UTILESListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/useful-1.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"4c48d6af-5ce4-4569-aaf8-ccc19496d806",
                "actions",                
                menuActions1Items,
				AppNavigation.IconFromGlyph("\ue10c")));            

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"a4ca0885-7702-465f-8cee-3c26abd86d5f",
                "recherche Bing intégré",                
                AppNavigation.ActionFromPage("RechercheBingIntegreListPage"),
				AppNavigation.IconFromGlyph("\ue155")));


            var menuSOCIALItems = new List<NavigationItem>();

            menuSOCIALItems.Add(AppNavigation.NodeFromAction(
				"1df2b84d-d429-48c5-a103-ac7dd3bed0e4",
                "Flickr",
				AppNavigation.ActionFromPage("FlickrListPage"),
                AppNavigation.IconFromGlyph("\ue114")));

            menuSOCIALItems.Add(AppNavigation.NodeFromAction(
				"1df2b84d-d429-48c5-a103-ac7dd3bed0e4",
                "YouTube",
				AppNavigation.ActionFromPage("YouTubeListPage"),
                AppNavigation.IconFromGlyph("\ue173")));

            menuSOCIALItems.Add(AppNavigation.NodeFromAction(
				"1df2b84d-d429-48c5-a103-ac7dd3bed0e4",
                "Twitter",
				AppNavigation.ActionFromPage("TwitterListPage"),
                AppNavigation.IconFromGlyph("\ue134")));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"1df2b84d-d429-48c5-a103-ac7dd3bed0e4",
                "SOCIAL",                
                menuSOCIALItems,
				AppNavigation.IconFromGlyph("\ue10c")));            


            var menuLyceeItems = new List<NavigationItem>();

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Présentation générale",
				AppNavigation.ActionFromPage("PresentationGeneraleListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/prsetation.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "conseil de lecture",
				AppNavigation.ActionFromPage("ConseilDeLectureListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/livtreee.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "CDI",
				AppNavigation.ActionFromPage("CDIListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/CDI.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Allemand",
				AppNavigation.ActionFromPage("AllemandListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/CDI-4.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Anglais et section européenne",
				AppNavigation.ActionFromPage("AnglaisEtSectionEuropeenneListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/angletrere-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Italien",
				AppNavigation.ActionFromPage("ItalienListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/italien.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Section européenne espagnol",
				AppNavigation.ActionFromPage("SectionEuropeenneEspagnolListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/espgn-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Arts plastiques",
				AppNavigation.ActionFromPage("ArtsPlastiquesListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/artpls-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Cinéma-audiovisuel",
				AppNavigation.ActionFromPage("CinemaAudiovisuelListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/auuddc-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Théâtre",
				AppNavigation.ActionFromPage("TheatreListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/theattre.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Histoire des arts",
				AppNavigation.ActionFromPage("HistoireDesArtsListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/hodza-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Langues et cultures ",
				AppNavigation.ActionFromPage("LanguesEtCulturesListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/lannsguescultures-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Littérature et société",
				AppNavigation.ActionFromPage("LitteratureEtSocieteListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/litesoc-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "SVT",
				AppNavigation.ActionFromPage("SVTListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/svtlogo-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "SES",
				AppNavigation.ActionFromPage("SESListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/sesss.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Sciences physiques",
				AppNavigation.ActionFromPage("SciencesPhysiquesListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/sciences_physiques.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "TERMINALE S NUMERIQUE",
				AppNavigation.ActionFromPage("TERMINALESNUMERIQUEListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/termsnumeric-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "TEMINALE ES MATHS",
				AppNavigation.ActionFromPage("TEMINALEESMATHSListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/termesmaths-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "TERMINALE S MATHS",
				AppNavigation.ActionFromPage("TERMINALESMATHSListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/terms-1.png")) }));

            menuLyceeItems.Add(AppNavigation.NodeFromAction(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "EPS",
				AppNavigation.ActionFromPage("EPSListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/sport.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"b475fae9-de27-4242-8db7-4388fef4ca64",
                "Lycée",                
                menuLyceeItems,
				AppNavigation.IconFromGlyph("\ue10c")));            


            var menuCOLLEGEItems = new List<NavigationItem>();

            menuCOLLEGEItems.Add(AppNavigation.NodeFromAction(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "CONSEIL DE LECTURE",
				AppNavigation.ActionFromPage("CONSEILDELECTURE1ListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/livre-1.png")) }));

            menuCOLLEGEItems.Add(AppNavigation.NodeFromAction(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "Section européenne Espagnol",
				AppNavigation.ActionFromPage("SectionEuropeenneEspagnol1ListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/espgn-3.png")) }));

            menuCOLLEGEItems.Add(AppNavigation.NodeFromAction(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "6EME ANGLAIS ALLEMAND",
				AppNavigation.ActionFromPage("EMEANGLAISALLEMANDListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/anglienne-3.png")) }));

            menuCOLLEGEItems.Add(AppNavigation.NodeFromAction(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "6EME ANGLAS ITALIEN",
				AppNavigation.ActionFromPage("EMEANGLASITALIENListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/anglienne-1.png")) }));

            menuCOLLEGEItems.Add(AppNavigation.NodeFromAction(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "CLASSES HORAIRES AMENAGES",
				AppNavigation.ActionFromPage("CLASSESHORAIRESAMENAGESListPage"),
                AppNavigation.IconFromGlyph("\ue121")));

            menuCOLLEGEItems.Add(AppNavigation.NodeFromAction(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "ENSA",
				AppNavigation.ActionFromPage("ENSAListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/ensa-1.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"a1723904-e302-4bda-8535-5044a9ceff43",
                "COLLEGE",                
                menuCOLLEGEItems,
				AppNavigation.IconFromGlyph("\ue10c")));            


            var menuCLASSESPREPASItems = new List<NavigationItem>();

            menuCLASSESPREPASItems.Add(AppNavigation.NodeFromAction(
				"454d981e-ffa9-4c88-ae1c-03f1b6ef8063",
                "CANDIDATURE PROCEDURE",
				AppNavigation.ActionFromPage("CANDIDATUREPROCEDUREListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/addm.png")) }));

            menuCLASSESPREPASItems.Add(AppNavigation.NodeFromAction(
				"454d981e-ffa9-4c88-ae1c-03f1b6ef8063",
                "INSCRIPTION EN CPGE",
				AppNavigation.ActionFromPage("INSCRIPTIONENCPGEListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/cpgee-3.png")) }));

            menuCLASSESPREPASItems.Add(AppNavigation.NodeFromAction(
				"454d981e-ffa9-4c88-ae1c-03f1b6ef8063",
                "bibliographies",
				AppNavigation.ActionFromPage("BibliographiesListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/bigra.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"454d981e-ffa9-4c88-ae1c-03f1b6ef8063",
                "CLASSES PREPAS",                
                menuCLASSESPREPASItems,
				AppNavigation.IconFromGlyph("\ue10c")));            


            var menuCITESCOLAIREItems = new List<NavigationItem>();

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "PRESENTATION",
				AppNavigation.ActionFromPage("PRESENTATIONListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/prsetation-2.png")) }));

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "VUE DE LA CITE SCOLAIRE",
				AppNavigation.ActionFromPage("VUEDELACITESCOLAIREListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/lycee_rodin_rue_corv.png")) }));

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "HISTOIRE DE RODIN",
				AppNavigation.ActionFromPage("HISTOIREDERODINListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/603px-auguste_rodin_1893_nadar.png")) }));

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "ORGANIGRAMME",
				AppNavigation.ActionFromPage("ORGANIGRAMMEListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/organigramme.png")) }));

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "INJS",
				AppNavigation.ActionFromPage("INJSListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/injs-1.png")) }));

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "LES REPAS",
				AppNavigation.ActionFromPage("LESREPASListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/iammisc_dinner_plate.png")) }));

            menuCITESCOLAIREItems.Add(AppNavigation.NodeFromAction(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "ASSOCIATION SPORTIVE",
				AppNavigation.ActionFromPage("ASSOCIATIONSPORTIVEListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/unsss.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"57287760-1297-43b6-955b-65f8b9caac54",
                "CITE SCOLAIRE",                
                menuCITESCOLAIREItems,
				AppNavigation.IconFromGlyph("\ue10c")));            

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"18544529-e758-4567-8444-e74b2eb11d28",
                "actu",                
                AppNavigation.ActionFromPage("ActuListPage"),
				AppNavigation.IconFromGlyph("\ue1d7")));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
