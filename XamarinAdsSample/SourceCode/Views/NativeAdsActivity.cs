using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.Gms.Ads.Formats;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    [Activity(Label = "NativeAdsActivity", Name = "com.hellokh.sovary.NativeAdsActivity")]
    public class NativeAdsActivity : AppCompatActivity
    {
        AdsManager adsMgr;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_native);
            adsMgr = new AdsManager(ApplicationContext);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            LinearLayoutManager layoutManager = new LinearLayoutManager(this);
            layoutManager.Orientation = RecyclerView.Vertical;
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(layoutManager);
            var adapter = new NativeRecyclerAdapter();
            recyclerView.SetAdapter(adapter);

            //Stored batch native ads
            var nativeAds = new List<UnifiedNativeAd>();
            var listen = new AdsUnifiedLoadListening();
            listen.Ads_UnifiedNativeLoad += (obj, unifiedAds) =>
            {
                nativeAds.Add(unifiedAds);
                var tag = ((AdsUnifiedLoadListening)obj).Tag as AdLoader;
                if (!tag.IsLoading)
                {
                    adapter.SetAds(nativeAds);
                }
            };

            listen.Ads_LoadFailed += (obj, errCode) =>
            {
                //Handle Ads Load Failed
            };
            listen.Ads_LeftApp += (s, e) =>
            {
                //Handle Ads clicked left app
            };
            //Max pre request 5
            adsMgr.CreateUnifiedAds(3, listen);
        }

    }
}