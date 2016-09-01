using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.DynamicStorage;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using Windows.ApplicationModel.Appointments;
using System.Linq;
using Windows.Storage;

using RODINInfo.Navigation;
using RODINInfo.ViewModels;

namespace RODINInfo.Sections
{
    public class FournituresSection : Section<Fournitures1Schema>
    {
		private DynamicStorageDataProvider<Fournitures1Schema> _dataProvider;		

		public FournituresSection()
		{
			_dataProvider = new DynamicStorageDataProvider<Fournitures1Schema>();
		}

		public override async Task<IEnumerable<Fournitures1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new DynamicStorageDataConfig
            {
                Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=c05afe4b-61ea-4574-b8bd-5d63af7e5b76&appId=c78fe7fb-d633-43d7-806d-a3f5209ea0cb"),
                AppId = "c78fe7fb-d633-43d7-806d-a3f5209ea0cb",
                StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string,
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Fournitures1Schema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<Fournitures1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Fournitures1Schema>
                {
                    Title = "fournitures",

                    Page = typeof(Pages.FournituresListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.texte.ToSafeString();
                        viewModel.SubTitle = item.lien.ToSafeString();
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.FournituresDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Fournitures1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Fournitures1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.texte.ToSafeString();
                    viewModel.Title = item.texte.ToSafeString();
                    viewModel.Description = item.lien.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Fournitures1Schema>>
                {
                    ActionConfig<Fournitures1Schema>.Link("lien", (item) => item.lien.ToSafeString()),
                };

                return new DetailPageConfig<Fournitures1Schema>
                {
                    Title = "fournitures",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
