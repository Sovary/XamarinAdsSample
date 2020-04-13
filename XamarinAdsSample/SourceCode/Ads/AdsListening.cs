using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    public class AdsListening : AdListener
    {
        public Object Tag { get; set; }
        public EventHandler<int> Ads_LoadFailed { get; set; }
        public EventHandler Ads_Loaded { get; set; }
        public EventHandler Ads_Clicked { get; set; }
        public EventHandler Ads_Opened { get; set; }
        public EventHandler Ads_LeftApp { get; set; }
        
        public override void OnAdFailedToLoad(int p0)
        {
            base.OnAdFailedToLoad(p0);
            Ads_LoadFailed?.Invoke(this, p0);
        }
        public override void OnAdLoaded()
        {
            base.OnAdLoaded();
            Ads_Loaded?.Invoke(this, null);
        }
        public override void OnAdClicked()
        {
            base.OnAdClicked();
            Ads_Clicked?.Invoke(this, null);
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
            Ads_Opened?.Invoke(this, null);
        }

        public override void OnAdLeftApplication()
        {
            base.OnAdLeftApplication();
            Ads_LeftApp?.Invoke(this, null);
        }
    }
}