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
using System.Text;
using Android.Net;

namespace Xamarin_Hms_Site_Demo
{
    [Activity(Label = "PlaceDetailSearchActivity")]
    public class PlaceDetailSearchActivity : AppCompatActivity, View.IOnClickListener
    {
        private static readonly string TAG = "PlaceDetailSearchActivity";

        private string apiKey;
        private static TextView resultTextView;
        private ISearchService searchService;
        private EditText siteIdInput;
        private EditText languageInput;
        private Button getPlaceDetailsButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_place_detail_search);

            apiKey = Intent.Extras.GetString("apiKey");
            searchService = SearchServiceFactory.Create(this, Uri.Encode(apiKey));

            resultTextView = FindViewById<TextView>(Resource.Id.textview_place_detail_results);
            siteIdInput = FindViewById<EditText>(Resource.Id.edittext_site_id);
            languageInput = FindViewById<EditText>(Resource.Id.edittext_language_pds);
            getPlaceDetailsButton = FindViewById<Button>(Resource.Id.btn_get_place_details);

            getPlaceDetailsButton.SetOnClickListener(this);
        }

        public void OnClick(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.btn_get_place_details:
                    DetailSearchRequest detailSearchRequest = new DetailSearchRequest();

                    detailSearchRequest.SiteId = siteIdInput.Text;
                    detailSearchRequest.Language = languageInput.Text;

                    DetailSearchResultListener detailSearchResultListener = new DetailSearchResultListener();
                    searchService.DetailSearch(detailSearchRequest, detailSearchResultListener);

                    break;

                default:
                    break;
            }
        }

        private class DetailSearchResultListener : Java.Lang.Object, ISearchResultListener
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
                DetailSearchResponse detailSearchResponse = (DetailSearchResponse)resultObject;
                StringBuilder resultText = new StringBuilder();
                Site site = detailSearchResponse.Site;
                resultText.AppendLine("Name: " + site.Name);
                resultText.AppendLine("Address: " + site.FormatAddress);
                resultText.AppendLine("Location: " + site.Location.Lat + " " + site.Location.Lng);
                resultTextView.Text = resultText.ToString();
            }
        }
    }
}