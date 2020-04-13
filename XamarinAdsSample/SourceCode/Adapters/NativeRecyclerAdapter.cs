using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Gms.Ads.Formats;
using System.Collections.Generic;

namespace XamarinAdsSample
{
    class NativeRecyclerAdapter : RecyclerView.Adapter
    {
        List<UnifiedNativeAd> data = new List<UnifiedNativeAd>();

        

        public void SetAds(List<UnifiedNativeAd> ads)
        {
            data.AddRange(ads);
            NotifyDataSetChanged();
        }

        public NativeRecyclerAdapter() { }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ads_unified, parent, false);
            return new AdsViewHolder(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            if (viewHolder is AdsViewHolder adH)
            {
                var ads = data[position] as UnifiedNativeAd;
                adH.AdsUnified.PopulateNativeAds(ads);
            }
        }

        public override int ItemCount => data.Count;
    }

    /// <summary>
    /// Hold Ads
    /// </summary>
    public class AdsViewHolder : RecyclerView.ViewHolder
    {
        public AdsUnifiedSetup AdsUnified { get; set; }
        public AdsViewHolder(View view) : base(view)
        {
            var setup = new AdsUnifiedSetup();
            setup.SetAdsViewHolder(view);
            AdsUnified = setup;
        }
    }

}