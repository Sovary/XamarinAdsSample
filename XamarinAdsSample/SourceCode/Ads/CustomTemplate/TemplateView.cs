using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Ads.Formats;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;

namespace XamarinAdsSample
{
    public class TemplateView : FrameLayout
    {
        private int templateType;
        private NativeTemplateStyle styles;
        private UnifiedNativeAd nativeAd;
        private UnifiedNativeAdView nativeAdView;

        private TextView primaryView;
        private TextView secondaryView;
        private RatingBar ratingBar;
        private TextView tertiaryView;
        private ImageView iconView;
        private MediaView mediaView;
        private Button callToActionView;
        private ConstraintLayout background;
        private static string MEDIUM_TEMPLATE = "medium_template";
        private static string SMALL_TEMPLATE = "small_template";
        public TemplateView(Context context) : base(context) { }
        public TemplateView(Context context, IAttributeSet attrs):base(context, attrs)
        {
            
            initView(context, attrs);
        }

        public TemplateView(Context context, IAttributeSet attrs, int defStyleAttr):base(context, attrs, defStyleAttr)
        {
            initView(context, attrs);
        }

        public TemplateView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes):base(context, attrs, defStyleAttr, defStyleRes)
        {
            initView(context, attrs);
        }
        public void setStyles(NativeTemplateStyle styles)
        {
            this.styles = styles;
            this.applyStyles();
        }

        public UnifiedNativeAdView getNativeAdView()
        {
            return nativeAdView;
        }

        private void applyStyles()
        {

            Drawable mainBackground = styles.getMainBackgroundColor();
            if (mainBackground != null)
            {
                background.Background= mainBackground;
                if (primaryView != null)
                {
                    primaryView.Background = mainBackground;
                }
                if (secondaryView != null)
                {
                    secondaryView.Background = mainBackground;
                }
                if (tertiaryView != null)
                {
                    tertiaryView.Background = mainBackground;
                }
            }

            Typeface primary = styles.getPrimaryTextTypeface();
            if (primary != null && primaryView != null)
            {
                primaryView.Typeface= primary;
            }

            Typeface secondary = styles.getSecondaryTextTypeface();
            if (secondary != null && secondaryView != null)
            {
                secondaryView.Typeface=secondary;
            }

            Typeface tertiary = styles.getTertiaryTextTypeface();
            if (tertiary != null && tertiaryView != null)
            {
                tertiaryView.Typeface=tertiary;
            }

            Typeface ctaTypeface = styles.getCallToActionTextTypeface();
            if (ctaTypeface != null && callToActionView != null)
            {
                callToActionView.Typeface=ctaTypeface;
            }

            int primaryTypefaceColor = styles.getPrimaryTextTypefaceColor();
            if (primaryTypefaceColor > 0 && primaryView != null)
            {

                primaryView.SetTextColor(Android.Graphics.Color.ParseColor($"{primaryTypefaceColor}"));
            }

            int secondaryTypefaceColor = styles.getSecondaryTextTypefaceColor();
            if (secondaryTypefaceColor > 0 && secondaryView != null)
            {
                secondaryView.SetTextColor(Android.Graphics.Color.ParseColor($"{secondaryTypefaceColor}"));
            }

            int tertiaryTypefaceColor = styles.getTertiaryTextTypefaceColor();
            if (tertiaryTypefaceColor > 0 && tertiaryView != null)
            {
                tertiaryView.SetTextColor(Android.Graphics.Color.ParseColor($"{tertiaryTypefaceColor}"));
            }

            int ctaTypefaceColor = styles.getCallToActionTypefaceColor();
            if (ctaTypefaceColor > 0 && callToActionView != null)
            {
                callToActionView.SetTextColor(Android.Graphics.Color.ParseColor($"{ctaTypefaceColor}"));
            }

            float ctaTextSize = styles.getCallToActionTextSize();
            if (ctaTextSize > 0 && callToActionView != null)
            {
                callToActionView.SetTextSize(ComplexUnitType.Sp, ctaTextSize);
            }

            float primaryTextSize = styles.getPrimaryTextSize();
            if (primaryTextSize > 0 && primaryView != null)
            {
                primaryView.SetTextSize(ComplexUnitType.Sp, primaryTextSize);
            }

            float secondaryTextSize = styles.getSecondaryTextSize();
            if (secondaryTextSize > 0 && secondaryView != null)
            {
                secondaryView.SetTextSize(ComplexUnitType.Sp, secondaryTextSize);
            }

            float tertiaryTextSize = styles.getTertiaryTextSize();
            if (tertiaryTextSize > 0 && tertiaryView != null)
            {
                tertiaryView.SetTextSize(ComplexUnitType.Sp, tertiaryTextSize);
            }

            Drawable ctaBackground = styles.getCallToActionBackgroundColor();
            if (ctaBackground != null && callToActionView != null)
            {
                callToActionView.Background = ctaBackground;
            }

            Drawable primaryBackground = styles.getPrimaryTextBackgroundColor();
            if (primaryBackground != null && primaryView != null)
            {
                primaryView.Background= primaryBackground;
            }

            Drawable secondaryBackground = styles.getSecondaryTextBackgroundColor();
            if (secondaryBackground != null && secondaryView != null)
            {
                secondaryView.Background = secondaryBackground;
            }

            Drawable tertiaryBackground = styles.getTertiaryTextBackgroundColor();
            if (tertiaryBackground != null && tertiaryView != null)
            {
                tertiaryView.Background = tertiaryBackground;
            }

            /*invalidate();
            requestLayout();*/
        }

        private bool adHasOnlyStore(UnifiedNativeAd nativeAd)
        {
            string store = nativeAd.Store;
            string advertiser = nativeAd.Advertiser;
            return !string.IsNullOrEmpty(store) && !string.IsNullOrEmpty(advertiser);
        }

        public void setNativeAd(UnifiedNativeAd nativeAd)
        {
            this.nativeAd = nativeAd;

            string store = nativeAd.Store;
            string advertiser = nativeAd.Advertiser;
            string headline = nativeAd.Headline;
            string body = nativeAd.Body;
            string cta = nativeAd.CallToAction;
            int starRating = Convert.ToInt32(nativeAd.StarRating);
            NativeAd.Image icon = nativeAd.Icon;
            string secondaryText;

            nativeAdView.CallToActionView = callToActionView;
            nativeAdView.HeadlineView = primaryView;
            nativeAdView.MediaView = mediaView;
            secondaryView.Visibility = ViewStates.Visible;
            if (adHasOnlyStore(nativeAd))
            {
                nativeAdView.StoreView = secondaryView;
                secondaryText = store;
            }
            else if (!string.IsNullOrEmpty(advertiser))
            {
                nativeAdView.AdvertiserView = secondaryView;
                secondaryText = advertiser;
            }
            else
            {
                secondaryText = "";
            }

            primaryView.Text = headline;
            callToActionView.Text = cta;

            //  Set the secondary view to be the star rating if available.
            if (starRating > 0)
            {
                secondaryView.Visibility = ViewStates.Gone;
                ratingBar.Visibility = ViewStates.Visible;
                ratingBar.Max = 5;
                nativeAdView.StarRatingView = ratingBar;
            }
            else
            {
                secondaryView.Text= secondaryText;
                secondaryView.Visibility = ViewStates.Visible;
                ratingBar.Visibility = ViewStates.Gone;
            }

            if (icon != null)
            {
                iconView.Visibility = ViewStates.Visible;
                iconView.SetImageDrawable(icon.Drawable);
            }
            else
            {
                iconView.Visibility = ViewStates.Gone;
            }

            if (tertiaryView != null)
            {
                tertiaryView.Text= body;
                nativeAdView.BodyView = tertiaryView;
            }

            nativeAdView.SetNativeAd(nativeAd);
        }

        /// <summary>
        /// To prevent memory leaks, make sure to destroy your ad when you don't need it anymore. This
        ///  method does not destroy the template view.
        ///  https://developers.google.com/admob/android/native-unified#destroy_ad
        /// </summary>
        public void destroyNativeAd()
        {
            nativeAd.Destroy();
        }

        public string getTemplateTypeName()
        {
            if (templateType == Resource.Layout.gnt_medium_template_view)
            {
                return MEDIUM_TEMPLATE;
            }
            else if (templateType == Resource.Layout.gnt_small_template_view)
            {
                return SMALL_TEMPLATE;
            }
            return "";
        }

        private void initView(Context context, IAttributeSet attributeSet)
        {

            TypedArray attributes =
                    context.Theme.ObtainStyledAttributes(attributeSet, Resource.Styleable.TemplateView, 0, 0);

            try
            {
                templateType =
                        attributes.GetResourceId(
                                Resource.Styleable.TemplateView_gnt_template_type, Resource.Layout.gnt_medium_template_view);
            }
            finally
            {
                attributes.Recycle();
            }
            LayoutInflater inflater =
                    (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            inflater.Inflate(templateType, this);
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();
            nativeAdView = FindViewById< UnifiedNativeAdView>(Resource.Id.native_ad_view);
            primaryView = FindViewById< TextView>(Resource.Id.primary);
            secondaryView = FindViewById< TextView>(Resource.Id.secondary);
            tertiaryView = FindViewById<TextView>(Resource.Id.body);

            ratingBar = FindViewById< RatingBar>(Resource.Id.rating_bar);
            ratingBar.Enabled = false;

            callToActionView = FindViewById< Button>(Resource.Id.cta);
            iconView = FindViewById< ImageView>(Resource.Id.icon);
            mediaView = FindViewById< MediaView>(Resource.Id.media_view);
            background = FindViewById< ConstraintLayout>(Resource.Id.background);
        }

    }
}