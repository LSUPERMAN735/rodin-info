//---------------------------------------------------------------------------
//
// <copyright file="Bootstrap.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/31/2016 2:26:24 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Storage;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;

using RODINInfo.Services;

namespace RODINInfo
{
    static class Bootstrap
    {
        private static readonly Guid APP_ID = new Guid("c78fe7fb-d633-43d7-806d-a3f5209ea0cb");

		public static void Init()
        {
			InitializeTelemetry();
			InitializeTilesAsync().FireAndForget();

			BitmapCache.ClearCacheAsync(TimeSpan.FromHours(48)).FireAndForget();
		}

        private static async Task InitializeTilesAsync()
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily.ToLower() != "windows.iot")
            {
                var init = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.TilesInitialized];
                if (init == null || (init is bool && !(bool)init))
                {
                    await TileServices.CreateLiveTile(@"Assets\Tiles\tiles.xml");
                    ApplicationData.Current.LocalSettings.Values[LocalSettingNames.TilesInitialized] = true;
                }
            }
        }

		private static void InitializeTelemetry()
        {
            var init = !string.IsNullOrEmpty(ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string);
            if (!init)
            {
                string deviceType = IsOnPhoneExecution() ? LocalSettingNames.PhoneValue : LocalSettingNames.WindowsValue;
                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] = deviceType;
                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] = ValidateStoreId();
            }
        }

        private static bool IsOnPhoneExecution()
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            return (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
        }

        private static Guid ValidateStoreId()
        {
            try
            {
                Guid storeId = CurrentApp.AppId;
                if (storeId != Guid.Empty && storeId != APP_ID)
                {
                    return storeId;
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}