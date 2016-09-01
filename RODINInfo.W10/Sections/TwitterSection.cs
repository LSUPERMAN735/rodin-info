using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using RODINInfo.Navigation;
using RODINInfo.ViewModels;

namespace RODINInfo.Sections
{
    public class TwitterSection : Section<TwitterSchema>
    {
		private TwitterDataProvider _dataProvider;

		public TwitterSection()
		{
			_dataProvider = new TwitterDataProvider(new TwitterOAuthTokens
			{
				ConsumerKey = "xxxx",//use your own key go to https://apps.twitter.com/ beginners learn more here https://appstudio.windows.com/fr-fr/home/howto#twitter //
                ConsumerSecret = "xxxx",//use your own key go to https://apps.twitter.com/ beginners learn more here https://appstudio.windows.com/fr-fr/home/howto#twitter //
                AccessToken = "xxxx",//use your own key go to https://apps.twitter.com/ beginners learn more here https://appstudio.windows.com/fr-fr/home/howto#twitter //
                AccessTokenSecret = "xxxx" //use your own key go to https://apps.twitter.com/ beginners learn more here https://appstudio.windows.com/fr-fr/home/howto#twitter //
            });
		}

		public override async Task<IEnumerable<TwitterSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new TwitterDataConfig
            {
                QueryType = TwitterQueryType.Search,
                Query = @"#rodin"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<TwitterSchema>> GetNextPageAsync()
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

        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "Twitter",

                    Page = typeof(Pages.TwitterListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.UserProfileImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return new NavInfo
                        {
                            NavigationType = NavType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage
        {
            get { return null; }
        }
    }
}
