/*
       Copyright 2020. Huawei Technologies Co., Ltd. All rights reserved.

       Licensed under the Apache License, Version 2.0 (the "License");
       you may not use this file except in compliance with the License.
       You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing, software
       distributed under the License is distributed on an "AS IS" BASIS,
       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
       See the License for the specific language governing permissions and
       limitations under the License.
*/

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Huawei.Agconnect.Config;
using XHmsAgconnectCore_1._0._0._300.Additions;

namespace Xamarin_Hms_Site_Demo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        private static readonly string TAG = "MainActivity";

        private string apiKey;
        private Button startPlaceSearchActivityButton;
        private Button startPlaceDetailSearchActivityButton;
        private Button startPlaceSuggestionSearchActivityButton;

        protected override void AttachBaseContext(Context context)
        {
            base.AttachBaseContext(context);
            AGConnectServicesConfig config = AGConnectServicesConfig.FromContext(context);
            config.OverlayWith(new HmsLazyInputStream(context));

            // Read api_key entry from config
            // and assign to apiKey property
            apiKey = ReadApiKeyFromConfig(config);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            startPlaceSearchActivityButton = FindViewById<Button>(Resource.Id.btn_start_place_search_activity);
            startPlaceDetailSearchActivityButton = FindViewById<Button>(Resource.Id.btn_start_place_detail_search_activity);
            startPlaceSuggestionSearchActivityButton = FindViewById<Button>(Resource.Id.btn_start_place_suggestion_search_activity);

            startPlaceSearchActivityButton.SetOnClickListener(this);
            startPlaceDetailSearchActivityButton.SetOnClickListener(this);
            startPlaceSuggestionSearchActivityButton.SetOnClickListener(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnClick(View view)
        {
            Intent intent = CreateIntentWithApiKey(apiKey);

            switch (view.Id)
            {
                case Resource.Id.btn_start_place_search_activity:
                    StartActivity(intent.SetClass(this, typeof(PlaceSearchActivity)));
                    break;

                case Resource.Id.btn_start_place_detail_search_activity:
                    StartActivity(intent.SetClass(this, typeof(PlaceDetailSearchActivity)));
                    break;

                case Resource.Id.btn_start_place_suggestion_search_activity:
                    StartActivity(intent.SetClass(this, typeof(PlaceSuggestionSearchActivity)));
                    break;

                default:
                    break;
            }
        }

        private string ReadApiKeyFromConfig(AGConnectServicesConfig config)
        {
            string clientConfig = config.GetString("client");
            return new GoogleGson.JsonParser()
                .Parse(clientConfig)
                .AsJsonObject
                .Get("api_key")
                .AsString;
        }

        private Intent CreateIntentWithApiKey(string apiKey)
        {
            return new Intent().PutExtra("apiKey", apiKey);
        }
    }
}