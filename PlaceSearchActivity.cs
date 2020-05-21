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
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Android.Net;

namespace Xamarin_Hms_Site_Demo
{
    [Activity(Label = "PlaceSearchActivity")]
    public partial class PlaceSearchActivity : AppCompatActivity, View.IOnClickListener
    {
        private static readonly string TAG = "PlaceSearchActivity";

        private string apiKey;
        private static TextView resultTextView;
        private ISearchService searchService;
        private EditText queryInput;
        private EditText latitudeInput;
        private EditText longitudeInput;
        private EditText radiusInput;
        private EditText languageInput;
        private EditText countryCodeInput;
        private EditText pageIndexInput;
        private EditText pageSizeInput;
        private Button searchPlaceButton;
        private Button searchNearbyPlaceButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_place_search);

            apiKey = Intent.Extras.GetString("apiKey");
            searchService = SearchServiceFactory.Create(this, Uri.Encode(apiKey));

            resultTextView = FindViewById<TextView>(Resource.Id.textview_text_search_results);
            queryInput = FindViewById<EditText>(Resource.Id.edittext_text_search_query);
            latitudeInput = FindViewById<EditText>(Resource.Id.edittext_lat);
            longitudeInput = FindViewById<EditText>(Resource.Id.edittext_lon);
            radiusInput = FindViewById<EditText>(Resource.Id.edittext_radius);
            languageInput = FindViewById<EditText>(Resource.Id.edittext_language);
            countryCodeInput = FindViewById<EditText>(Resource.Id.edittext_country_code);
            pageIndexInput = FindViewById<EditText>(Resource.Id.edittext_page_index);
            pageSizeInput = FindViewById<EditText>(Resource.Id.edittext_page_size);

            searchPlaceButton = FindViewById<Button>(Resource.Id.btn_search_place);
            searchNearbyPlaceButton = FindViewById<Button>(Resource.Id.btn_search_nearby_place);
            searchPlaceButton.SetOnClickListener(this);
            searchNearbyPlaceButton.SetOnClickListener(this);
        }

        public void OnClick(View view)
        {
            string queryText = queryInput.Text;
            string language = languageInput.Text;
            string countryCode = countryCodeInput.Text;
            double lat = double.Parse(latitudeInput.Text, CultureInfo.InvariantCulture);
            double lon = double.Parse(longitudeInput.Text, CultureInfo.InvariantCulture);
            Coordinate location = new Coordinate(lat, lon);
            Java.Lang.Integer radius = Java.Lang.Integer.ValueOf(radiusInput.Text);
            Java.Lang.Integer pageIndex = Java.Lang.Integer.ValueOf(pageIndexInput.Text);
            Java.Lang.Integer pageSize = Java.Lang.Integer.ValueOf(pageSizeInput.Text);

            switch (view.Id)
            {
                case Resource.Id.btn_search_place:
                    TextSearchRequest textSearchRequest = new TextSearchRequest();
                    textSearchRequest.Query = queryText;
                    textSearchRequest.Language = language;
                    textSearchRequest.CountryCode = countryCode;
                    textSearchRequest.Location = location;
                    textSearchRequest.Radius = radius;
                    textSearchRequest.PageIndex = pageIndex;
                    textSearchRequest.PageSize = pageSize;

                    TextSearchResultListener textSearchResultListener = new TextSearchResultListener();
                    searchService.TextSearch(textSearchRequest, textSearchResultListener);

                    break;

                case Resource.Id.btn_search_nearby_place:
                    NearbySearchRequest nearbySearchRequest = new NearbySearchRequest();
                    nearbySearchRequest.Query = queryText;
                    nearbySearchRequest.Language = language;
                    nearbySearchRequest.Location = location;
                    nearbySearchRequest.Radius = radius;
                    nearbySearchRequest.PageIndex = pageIndex;
                    nearbySearchRequest.PageSize = pageSize;

                    NearbySearchResultListener nearbySearchResultListener = new NearbySearchResultListener();
                    searchService.NearbySearch(nearbySearchRequest, nearbySearchResultListener);

                    break;

                default:
                    break;
            }
        }

        private static void logOnSearchError(SearchStatus searchStatus)
        {
            Log.Info(TAG, "Error Code:" +
                searchStatus.ErrorCode +
                "Error Message: " +
                searchStatus.ErrorMessage);
        }

        private static void addSitesToResultTextView(IList<Site> sites)
        {
            int count = 0;
            StringBuilder resultText = new StringBuilder();

            foreach (Site site in sites)
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

        private class TextSearchResultListener : Java.Lang.Object, ISearchResultListener
        {
            public void OnSearchError(SearchStatus searchStatus)
            {
                logOnSearchError(searchStatus);
            }

            public void OnSearchResult(Java.Lang.Object resultObject)
            {
                TextSearchResponse textSearchResponse = (TextSearchResponse)resultObject;
                addSitesToResultTextView(textSearchResponse.Sites);
            }
        }

        private class NearbySearchResultListener : Java.Lang.Object, ISearchResultListener
        {
            public void OnSearchError(SearchStatus status)
            {
                logOnSearchError(status);
            }

            public void OnSearchResult(Java.Lang.Object resultObject)
            {
                NearbySearchResponse nearbySearchResponse = (NearbySearchResponse)resultObject;
                addSitesToResultTextView(nearbySearchResponse.Sites);
            }
        }
    }
}