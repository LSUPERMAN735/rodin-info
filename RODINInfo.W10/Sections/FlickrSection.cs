using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Flickr;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using RODINInfo.Navigation;
using RODINInfo.ViewModels;

namespace RODINInfo.Sections
{
    public class FlickrSection : Section<FlickrSchema>
    {
		private FlickrDataProvider _dataProvider;	

		public FlickrSection()
		{
			_dataProvider = new FlickrDataProvider();
		}

		public override async Task<IEnumerable<FlickrSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new FlickrDataConfig
            {
                QueryType = FlickrQueryType.Tags,
                Query = @"rodin",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<FlickrSchema>> GetNextPageAsync()
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

        public override ListPageConfig<FlickrSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<FlickrSchema>
                {
                    Title = "Flickr",

                    Page = typeof(Pages.FlickrListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.FlickrDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<FlickrSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, FlickrSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<FlickrSchema>>
                {
                    ActionConfig<FlickrSchema>.Link("Go To Source", (item) => item.FeedUrl.ToSafeString()),
                };

                return new DetailPageConfig<FlickrSchema>
                {
                    Title = "Flickr",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
