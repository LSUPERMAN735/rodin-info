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
    public class UTILESSection : Section<UTILES1Schema>
    {
		private DynamicStorageDataProvider<UTILES1Schema> _dataProvider;		

		public UTILESSection()
		{
			_dataProvider = new DynamicStorageDataProvider<UTILES1Schema>();
		}

		public override async Task<IEnumerable<UTILES1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new DynamicStorageDataConfig
            {
                Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=ee2d3dc9-7014-44da-9432-056585f062ae&appId=c78fe7fb-d633-43d7-806d-a3f5209ea0cb"),
                AppId = "c78fe7fb-d633-43d7-806d-a3f5209ea0cb",
                StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string,
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<UTILES1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<UTILES1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<UTILES1Schema>
                {
                    Title = "UTILES",

                    Page = typeof(Pages.UTILESListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
						viewModel.Header = item.catégories.ToSafeString();
                        viewModel.Title = item.texte.ToSafeString();
                        viewModel.SubTitle = item.catégories.ToSafeString();

						viewModel.GroupBy = item.catégories.SafeType();

						viewModel.OrderBy = item.texte;
                    },
					OrderType = OrderType.Descending,
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.UTILESDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<UTILES1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, UTILES1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.catégories.ToSafeString();
                    viewModel.Title = item.texte.ToSafeString();
                    viewModel.Description = item.liens.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<UTILES1Schema>>
                {
                    ActionConfig<UTILES1Schema>.Link("LIENS", (item) => item.liens.ToSafeString()),
                };

                return new DetailPageConfig<UTILES1Schema>
                {
                    Title = "UTILES",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
