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
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Huawei.Hms.Site.Api;
using Com.Huawei.Hms.Site.Api.Model;
using System.Globalization;
using System.Text;
using Android.Net;

namespace Xamarin_Hms_Site_Demo
{
    [Activity(Label = "PlaceSuggestionSearchActivity")]
    public class PlaceSuggestionSearchActivity : AppCompatActivity, View.IOnClickListener
    {
        private static readonly string TAG = "PlaceSuggestionSearchActivity";

        private string apiKey;
        private static TextView resultTextView;
        private ISearchService searchService;
        private EditText queryInput;
        private EditText latitudeInput;
        private EditText longitudeInput;
        private EditText radiusInput;
        private EditText languageInput;
        private EditText countryCodeInput;
        private Button searchPlaceSuggestionButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_place_suggestion_search);

            apiKey = Intent.Extras.GetString("apiKey");
            searchService = SearchServiceFactory.Create(this, Uri.Encode(apiKey));

            resultTextView = FindViewById<TextView>(Resource.Id.textview_pss_results);
            queryInput = FindViewById<EditText>(Resource.Id.edittext_search_query_pss);
            latitudeInput = FindViewById<EditText>(Resource.Id.edittext_lat_pss);
            longitudeInput = FindViewById<EditText>(Resource.Id.edittext_lon_pss);
            radiusInput = FindViewById<EditText>(Resource.Id.edittext_radius_pss);
            languageInput = FindViewById<EditText>(Resource.Id.edittext_language_pss);
            countryCodeInput = FindViewById<EditText>(Resource.Id.edittext_country_code_pss);

            searchPlaceSuggestionButton = FindViewById<Button>(Resource.Id.btn_search_place_suggestion);
            searchPlaceSuggestionButton.SetOnClickListener(this);

        }

        public void OnClick(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.btn_search_place_suggestion:
                    double lat = double.Parse(latitudeInput.Text, CultureInfo.InvariantCulture);
                    double lon = double.Parse(longitudeInput.Text, CultureInfo.InvariantCulture);

                    QuerySuggestionRequest querySuggestionRequest = new QuerySuggestionRequest();
                    querySuggestionRequest.Query = queryInput.Text;
                    querySuggestionRequest.Language = languageInput.Text;
                    querySuggestionRequest.CountryCode = countryCodeInput.Text;
                    querySuggestionRequest.Location = new Coordinate(lat, lon);
                    querySuggestionRequest.Radius = Java.Lang.Integer.ValueOf(radiusInput.Text);

                    QuerySuggestionResultListener querySuggestionResultListener = new QuerySuggestionResultListener();
                    searchService.QuerySuggestion(querySuggestionRequest, querySuggestionResultListener);

                    break;

                default:
                    break;
            }
        }

        private class QuerySuggestionResultListener : Java.Lang.Object, ISearchResultListener
        {
            public void OnSearchError(SearchStatus searchStatus)
            {
                Log.Info(TAG, "Error Code:" +
                searchStatus.ErrorCode +
                "Error Message: " +
                searchStatus.ErrorMessage);
            }

            public void OnSearchResult(Java.Lang.Object resultObject)
            {
                int count = 0;
                QuerySuggestionResponse querySuggestionResponse = (QuerySuggestionResponse)resultObject;
                StringBuilder resultText = new StringBuilder();

                foreach (Site site in querySuggestionResponse.Sites)
                {
                    string item = "[{0}] name: {1}, siteId: {2}, formatAddress: {3}, country: {4}, countryCode: {5}";
                    string item_str = string.Format(item,
                        count++.ToString(),
                        site.Name, site.SiteId,
                        site.FormatAddress,
                        site.Address.Country,
                        site.Address.CountryCode);
                    resultText.AppendLine(item_str);
                }
                resultTextView.Text = resultText.ToString();
            }
        }
    }
}