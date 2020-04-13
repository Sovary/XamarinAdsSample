using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace XamarinAdsSample
{
    [Activity(Label = "NativeAdsActivity", Name = "com.hellokh.sovary.NativeAdsActivity")]
    public class NativeAdsActivity : AppCompatActivity
    {
        AdsManager adsMgr;
        //Stored ads and data
        List<Object> dataAll = new List<Object>();
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

            dataAll.AddRange(GetDummyData());
            var listen = new AdsUnifiedLoadListening();
            listen.Ads_UnifiedNativeLoad += (obj, unifiedAds) =>
            {
                dataAll.Add(unifiedAds);
                var tag = ((AdsUnifiedLoadListening)obj).Tag as AdLoader;
                if (!tag.IsLoading)
                {
                    adapter.SetData(dataAll);
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
            adsMgr.CreateUnifiedAds(5, listen);
        }



        public List<MenuItem> GetDummyData()
        {
            using (StreamReader r = new StreamReader(ApplicationContext.Resources.OpenRawResource(Resource.Raw.menu_items_json)))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<MenuItem>>(json);
            }
        }

    }
}