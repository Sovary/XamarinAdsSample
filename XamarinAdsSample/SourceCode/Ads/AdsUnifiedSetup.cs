using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads.Formats;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    public class AdsUnifiedSetup
    {
        private UnifiedNativeAdView adView;
        public UnifiedNativeAdView AdView { get => adView; private set { adView = value; } }
        public void SetAdsViewHolder(View view)
        {
            AdView = (UnifiedNativeAdView)view.FindViewById(Resource.Id.adsUnifiedView);

            // The MediaView will display a video asset if one is present in the ad, and the
            // first image asset otherwise.
            adView.MediaView = adView.FindViewById(Resource.Id.ad_media) as MediaView;
            // Register the view used for each individual asset.
            adView.HeadlineView = adView.FindViewById(Resource.Id.ad_headline);
            adView.BodyView = adView.FindViewById(Resource.Id.ad_body);
            adView.CallToActionView = adView.FindViewById(Resource.Id.ad_call_to_action);
            adView.IconView = adView.FindViewById(Resource.Id.ad_icon);
            adView.PriceView = adView.FindViewById(Resource.Id.ad_price);
            adView.StarRatingView = adView.FindViewById(Resource.Id.ad_stars);
            adView.StoreView = adView.FindViewById(Resource.Id.ad_store);
            adView.AdvertiserView = adView.FindViewById(Resource.Id.ad_advertiser);
        }

        public void PopulateNativeAds(UnifiedNativeAd nativeAds)
        {
            UnifiedNativeAdView adview = this.AdView;
            ((TextView)adview.HeadlineView).Text = nativeAds.Headline;
            ((TextView)adview.BodyView).Text = nativeAds.Body;
            ((Button)adview.CallToActionView).Text = nativeAds.CallToAction;
            ViewStates getViewState(Object ob) => ob is null ? ViewStates.Gone : ViewStates.Visible;

            adview.IconView.Visibility = getViewState(nativeAds.Icon);
            if (nativeAds.Icon != null)
            {
                ((ImageView)adview.IconView).SetImageDrawable(nativeAds.Icon.Drawable);
            }

            if (nativeAds.Price != null)
            {
                ((TextView)adview.PriceView).Text = nativeAds.Price;
            }
            adview.PriceView.Visibility = getViewState(nativeAds.Price);

            if (nativeAds.Store != null)
            {
                ((TextView)adview.StoreView).Text = nativeAds.Store;
            }
            adview.StoreView.Visibility = getViewState(nativeAds.Store);

            if (nativeAds.StarRating != null)
            {
                ((RatingBar)adview.StarRatingView).Rating = nativeAds.StarRating.FloatValue();
            }
            adview.StarRatingView.Visibility = getViewState(nativeAds.StarRating);

            if (nativeAds.Advertiser != null)
            {
                ((TextView)adview.AdvertiserView).Text = nativeAds.Advertiser;
            }
            adview.AdvertiserView.Visibility = getViewState(nativeAds.Advertiser);
            adview.SetNativeAd(nativeAds);
        }
    }
}