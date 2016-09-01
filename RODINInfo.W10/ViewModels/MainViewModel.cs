using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Bing;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using RODINInfo.Sections;


namespace RODINInfo.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel Actions1 { get; private set; }
        public ListViewModel RechercheBingIntegre { get; private set; }
        public ListViewModel SOCIAL { get; private set; }
        public ListViewModel Lycee { get; private set; }
        public ListViewModel COLLEGE { get; private set; }
        public ListViewModel CLASSESPREPAS { get; private set; }
        public ListViewModel CITESCOLAIRE { get; private set; }
        public ListViewModel Actu { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "RODIN info";
            Actions1 = ViewModelFactory.NewList(new Actions1Section());
            RechercheBingIntegre = ViewModelFactory.NewList(new RechercheBingIntegreSection(), visibleItems);
            SOCIAL = ViewModelFactory.NewList(new SOCIALSection());
            Lycee = ViewModelFactory.NewList(new LyceeSection());
            COLLEGE = ViewModelFactory.NewList(new COLLEGESection());
            CLASSESPREPAS = ViewModelFactory.NewList(new CLASSESPREPASSection());
            CITESCOLAIRE = ViewModelFactory.NewList(new CITESCOLAIRESection());
            Actu = ViewModelFactory.NewList(new ActuSection(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Actions1;
            yield return RechercheBingIntegre;
            yield return SOCIAL;
            yield return Lycee;
            yield return COLLEGE;
            yield return CLASSESPREPAS;
            yield return CITESCOLAIRE;
            yield return Actu;
        }
    }
}
