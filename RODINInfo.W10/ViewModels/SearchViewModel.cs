using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RODINInfo.Sections;
namespace RODINInfo.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "RODIN info";
            Fournitures = ViewModelFactory.NewList(new FournituresSection());
            UTILES = ViewModelFactory.NewList(new UTILESSection());
            RechercheBingIntegre = ViewModelFactory.NewList(new RechercheBingIntegreSection());
            Flickr = ViewModelFactory.NewList(new FlickrSection());
            YouTube = ViewModelFactory.NewList(new YouTubeSection());
            Twitter = ViewModelFactory.NewList(new TwitterSection());
            PresentationGenerale = ViewModelFactory.NewList(new PresentationGeneraleSection());
            ConseilDeLecture = ViewModelFactory.NewList(new ConseilDeLectureSection());
            CDI = ViewModelFactory.NewList(new CDISection());
            Allemand = ViewModelFactory.NewList(new AllemandSection());
            AnglaisEtSectionEuropeenne = ViewModelFactory.NewList(new AnglaisEtSectionEuropeenneSection());
            Italien = ViewModelFactory.NewList(new ItalienSection());
            SectionEuropeenneEspagnol = ViewModelFactory.NewList(new SectionEuropeenneEspagnolSection());
            ArtsPlastiques = ViewModelFactory.NewList(new ArtsPlastiquesSection());
            CinemaAudiovisuel = ViewModelFactory.NewList(new CinemaAudiovisuelSection());
            Theatre = ViewModelFactory.NewList(new TheatreSection());
            HistoireDesArts = ViewModelFactory.NewList(new HistoireDesArtsSection());
            LanguesEtCultures = ViewModelFactory.NewList(new LanguesEtCulturesSection());
            LitteratureEtSociete = ViewModelFactory.NewList(new LitteratureEtSocieteSection());
            SVT = ViewModelFactory.NewList(new SVTSection());
            SES = ViewModelFactory.NewList(new SESSection());
            SciencesPhysiques = ViewModelFactory.NewList(new SciencesPhysiquesSection());
            TERMINALESNUMERIQUE = ViewModelFactory.NewList(new TERMINALESNUMERIQUESection());
            TEMINALEESMATHS = ViewModelFactory.NewList(new TEMINALEESMATHSSection());
            TERMINALESMATHS = ViewModelFactory.NewList(new TERMINALESMATHSSection());
            EPS = ViewModelFactory.NewList(new EPSSection());
            CONSEILDELECTURE1 = ViewModelFactory.NewList(new CONSEILDELECTURE1Section());
            SectionEuropeenneEspagnol1 = ViewModelFactory.NewList(new SectionEuropeenneEspagnol1Section());
            EMEANGLAISALLEMAND = ViewModelFactory.NewList(new EMEANGLAISALLEMANDSection());
            EMEANGLASITALIEN = ViewModelFactory.NewList(new EMEANGLASITALIENSection());
            CLASSESHORAIRESAMENAGES = ViewModelFactory.NewList(new CLASSESHORAIRESAMENAGESSection());
            ENSA = ViewModelFactory.NewList(new ENSASection());
            CANDIDATUREPROCEDURE = ViewModelFactory.NewList(new CANDIDATUREPROCEDURESection());
            INSCRIPTIONENCPGE = ViewModelFactory.NewList(new INSCRIPTIONENCPGESection());
            Bibliographies = ViewModelFactory.NewList(new BibliographiesSection());
            PRESENTATION = ViewModelFactory.NewList(new PRESENTATIONSection());
            VUEDELACITESCOLAIRE = ViewModelFactory.NewList(new VUEDELACITESCOLAIRESection());
            HISTOIREDERODIN = ViewModelFactory.NewList(new HISTOIREDERODINSection());
            ORGANIGRAMME = ViewModelFactory.NewList(new ORGANIGRAMMESection());
            INJS = ViewModelFactory.NewList(new INJSSection());
            LESREPAS = ViewModelFactory.NewList(new LESREPASSection());
            ASSOCIATIONSPORTIVE = ViewModelFactory.NewList(new ASSOCIATIONSPORTIVESection());
            Actu = ViewModelFactory.NewList(new ActuSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Fournitures { get; private set; }
        public ListViewModel UTILES { get; private set; }
        public ListViewModel RechercheBingIntegre { get; private set; }
        public ListViewModel Flickr { get; private set; }
        public ListViewModel YouTube { get; private set; }
        public ListViewModel Twitter { get; private set; }
        public ListViewModel PresentationGenerale { get; private set; }
        public ListViewModel ConseilDeLecture { get; private set; }
        public ListViewModel CDI { get; private set; }
        public ListViewModel Allemand { get; private set; }
        public ListViewModel AnglaisEtSectionEuropeenne { get; private set; }
        public ListViewModel Italien { get; private set; }
        public ListViewModel SectionEuropeenneEspagnol { get; private set; }
        public ListViewModel ArtsPlastiques { get; private set; }
        public ListViewModel CinemaAudiovisuel { get; private set; }
        public ListViewModel Theatre { get; private set; }
        public ListViewModel HistoireDesArts { get; private set; }
        public ListViewModel LanguesEtCultures { get; private set; }
        public ListViewModel LitteratureEtSociete { get; private set; }
        public ListViewModel SVT { get; private set; }
        public ListViewModel SES { get; private set; }
        public ListViewModel SciencesPhysiques { get; private set; }
        public ListViewModel TERMINALESNUMERIQUE { get; private set; }
        public ListViewModel TEMINALEESMATHS { get; private set; }
        public ListViewModel TERMINALESMATHS { get; private set; }
        public ListViewModel EPS { get; private set; }
        public ListViewModel CONSEILDELECTURE1 { get; private set; }
        public ListViewModel SectionEuropeenneEspagnol1 { get; private set; }
        public ListViewModel EMEANGLAISALLEMAND { get; private set; }
        public ListViewModel EMEANGLASITALIEN { get; private set; }
        public ListViewModel CLASSESHORAIRESAMENAGES { get; private set; }
        public ListViewModel ENSA { get; private set; }
        public ListViewModel CANDIDATUREPROCEDURE { get; private set; }
        public ListViewModel INSCRIPTIONENCPGE { get; private set; }
        public ListViewModel Bibliographies { get; private set; }
        public ListViewModel PRESENTATION { get; private set; }
        public ListViewModel VUEDELACITESCOLAIRE { get; private set; }
        public ListViewModel HISTOIREDERODIN { get; private set; }
        public ListViewModel ORGANIGRAMME { get; private set; }
        public ListViewModel INJS { get; private set; }
        public ListViewModel LESREPAS { get; private set; }
        public ListViewModel ASSOCIATIONSPORTIVE { get; private set; }
        public ListViewModel Actu { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Fournitures;
            yield return UTILES;
            yield return RechercheBingIntegre;
            yield return Flickr;
            yield return YouTube;
            yield return Twitter;
            yield return PresentationGenerale;
            yield return ConseilDeLecture;
            yield return CDI;
            yield return Allemand;
            yield return AnglaisEtSectionEuropeenne;
            yield return Italien;
            yield return SectionEuropeenneEspagnol;
            yield return ArtsPlastiques;
            yield return CinemaAudiovisuel;
            yield return Theatre;
            yield return HistoireDesArts;
            yield return LanguesEtCultures;
            yield return LitteratureEtSociete;
            yield return SVT;
            yield return SES;
            yield return SciencesPhysiques;
            yield return TERMINALESNUMERIQUE;
            yield return TEMINALEESMATHS;
            yield return TERMINALESMATHS;
            yield return EPS;
            yield return CONSEILDELECTURE1;
            yield return SectionEuropeenneEspagnol1;
            yield return EMEANGLAISALLEMAND;
            yield return EMEANGLASITALIEN;
            yield return CLASSESHORAIRESAMENAGES;
            yield return ENSA;
            yield return CANDIDATUREPROCEDURE;
            yield return INSCRIPTIONENCPGE;
            yield return Bibliographies;
            yield return PRESENTATION;
            yield return VUEDELACITESCOLAIRE;
            yield return HISTOIREDERODIN;
            yield return ORGANIGRAMME;
            yield return INJS;
            yield return LESREPAS;
            yield return ASSOCIATIONSPORTIVE;
            yield return Actu;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
