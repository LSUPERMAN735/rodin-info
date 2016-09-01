using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp;
using System.Linq;

using RODINInfo.Navigation;
using RODINInfo.ViewModels;

namespace RODINInfo.Sections
{
    public class EMEANGLAISALLEMANDSection : Section<HtmlSchema>
    {
		private HtmlDataProvider _dataProvider;	

		public EMEANGLAISALLEMANDSection()
		{
			_dataProvider = new HtmlDataProvider();
		}

		public override async Task<IEnumerable<HtmlSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/EMEANGLAISALLEMAND.htm"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<HtmlSchema>> GetNextPageAsync()
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

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<HtmlSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<HtmlSchema>
                {
                    Title = "6EME ANGLAIS ALLEMAND",

                    Page = typeof(Pages.EMEANGLAISALLEMANDListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Content = item.Content;
                    },
                    DetailNavigation = (item) =>
                    {
                        return null;
                    }
                };
            }
        }

        public override DetailPageConfig<HtmlSchema> DetailPage
        {
            get { return null; }
        }
    }
}
