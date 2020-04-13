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
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    public class AdsUnifiedLoadListening : AdsListening , UnifiedNativeAd.IOnUnifiedNativeAdLoadedListener
    {
        public EventHandler<UnifiedNativeAd> Ads_UnifiedNativeLoad { get; set; }

        public void OnUnifiedNativeAdLoaded(UnifiedNativeAd ad)
        {
            Ads_UnifiedNativeLoad?.Invoke(this, ad);
        }
    }
}