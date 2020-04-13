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
using Android.Views.Animations;
using Android.Widget;

namespace XamarinAdsSample
{
    public class AdsManager
    {
        //static shared all instance object
        static InterstitialAd interstitial;
        Context context;
        public AdsManager(Context context)
        {
            this.context = context;
            MobileAds.Initialize(context, context.GetString(Resource.String.admob_app_id));
        }

        public void CreateAds(AdView adView, AdsListening listen = null)
        {
            var re = new AdRequest.Builder().Build();
            if (string.IsNullOrEmpty(adView.AdUnitId)) throw new ValidatingException("Ads unit id is required (AdsView)");
            var listener = new AdsListening();
            adView.AdListener = listen!=null? listen : listener;
            adView.LoadAd(re);
        }

        public void CreateInterstitialAds(AdsListening listen=null)
        {
            var adRequest = new AdRequest.Builder().Build();
            interstitial = new InterstitialAd(context);
            interstitial.AdUnitId = context.GetString(Resource.String.admobInterstitial);
            interstitial.AdListener = listen != null ? listen : new AdsListening();
            interstitial.LoadAd(adRequest);
        }
        public bool IsInterstitialAdsInit => interstitial != null;
        public InterstitialAd GetInterstitialAds()
        {
            if (!IsInterstitialAdsInit) throw new ValidatingException("Illegal interstitial ads is null");
            return interstitial;
        }


        public void CreateUnifiedAds(int numAds, AdsUnifiedLoadListening listen)
        {
            if (listen == null) throw new ValidatingException("Not Implemented listener Unified");
            var builder = new AdLoader.Builder(context, context.GetString(Resource.String.admobNative));
            builder.ForUnifiedNativeAd(listen);
            builder.WithAdListener(listen);
            builder.WithNativeAdOptions(new Android.Gms.Ads.Formats.NativeAdOptions.Builder().SetAdChoicesPlacement(5).Build());

            AdLoader adloader = builder.Build();
            adloader.LoadAds(new AdRequest.Builder().Build(), numAds);
            if (listen.Tag is null)
            {
                listen.Tag = adloader;
            }
        }
    }
}