using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Bing;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using RODINInfo.Navigation;
using RODINInfo.ViewModels;

namespace RODINInfo.Sections
{
	public class RechercheBingIntegreSection : Section<BingSchema>
    {
		private BingDataProvider _dataProvider;		

		public RechercheBingIntegreSection()
		{
			_dataProvider = new BingDataProvider();
		}

		public override async Task<IEnumerable<BingSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new BingDataConfig
            {
                Country = BingCountry.France,
                Query = @"college rodin"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<BingSchema>> GetNextPageAsync()
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

        public override ListPageConfig<BingSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<BingSchema>
                {
                    Title = "recherche Bing intégré",

					Page = typeof(Pages.RechercheBingIntegreListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                    },
                    DetailNavigation = (item) =>
                    {
                        return new NavInfo
                        {
                            NavigationType = NavType.DeepLink,
                            TargetUri = new Uri(item.Link)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<BingSchema> DetailPage
        {
            get { return null; }
        }
    }
}
