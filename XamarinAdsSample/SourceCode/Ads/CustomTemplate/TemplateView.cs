using System;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Ads.Formats;
using Android.Graphics;
using Android.Graphics.Drawables;
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
            
            InitView(context, attrs);
        }

        public TemplateView(Context context, IAttributeSet attrs, int defStyleAttr):base(context, attrs, defStyleAttr)
        {
            InitView(context, attrs);
        }

        public TemplateView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes):base(context, attrs, defStyleAttr, defStyleRes)
        {
            InitView(context, attrs);
        }
        public void SetStyles(NativeTemplateStyle styles)
        {
            this.styles = styles;
            this.ApplyStyles();
        }

        public UnifiedNativeAdView NativeAdView => nativeAdView;

        private void ApplyStyles()
        {

            Drawable mainBackground = styles.MainBackgroundColor;
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

            Typeface primary = styles.PrimaryTextTypeface;
            if (primary != null && primaryView != null)
            {
                primaryView.Typeface= primary;
            }

            Typeface secondary = styles.SecondaryTextTypeface;
            if (secondary != null && secondaryView != null)
            {
                secondaryView.Typeface=secondary;
            }

            Typeface tertiary = styles.TertiaryTextTypeface;
            if (tertiary != null && tertiaryView != null)
            {
                tertiaryView.Typeface=tertiary;
            }

            Typeface ctaTypeface = styles.CallToActionTextTypeface;
            if (ctaTypeface != null && callToActionView != null)
            {
                callToActionView.Typeface=ctaTypeface;
            }

            int primaryTypefaceColor = styles.PrimaryTextTypefaceColor;
            if (primaryTypefaceColor > 0 && primaryView != null)
            {

                primaryView.SetTextColor(Android.Graphics.Color.ParseColor($"{primaryTypefaceColor}"));
            }

            int secondaryTypefaceColor = styles.SecondaryTextTypefaceColor;
            if (secondaryTypefaceColor > 0 && secondaryView != null)
            {
                secondaryView.SetTextColor(Android.Graphics.Color.ParseColor($"{secondaryTypefaceColor}"));
            }

            int tertiaryTypefaceColor = styles.TertiaryTextTypefaceColor;
            if (tertiaryTypefaceColor > 0 && tertiaryView != null)
            {
                tertiaryView.SetTextColor(Android.Graphics.Color.ParseColor($"{tertiaryTypefaceColor}"));
            }

            int ctaTypefaceColor = styles.CallToActionTypefaceColor;
            if (ctaTypefaceColor > 0 && callToActionView != null)
            {
                callToActionView.SetTextColor(Android.Graphics.Color.ParseColor($"{ctaTypefaceColor}"));
            }

            float ctaTextSize = styles.CallToActionTextSize;
            if (ctaTextSize > 0 && callToActionView != null)
            {
                callToActionView.SetTextSize(ComplexUnitType.Sp, ctaTextSize);
            }

            float primaryTextSize = styles.PrimaryTextSize;
            if (primaryTextSize > 0 && primaryView != null)
            {
                primaryView.SetTextSize(ComplexUnitType.Sp, primaryTextSize);
            }

            float secondaryTextSize = styles.SecondaryTextSize;
            if (secondaryTextSize > 0 && secondaryView != null)
            {
                secondaryView.SetTextSize(ComplexUnitType.Sp, secondaryTextSize);
            }

            float tertiaryTextSize = styles.TertiaryTextSize;
            if (tertiaryTextSize > 0 && tertiaryView != null)
            {
                tertiaryView.SetTextSize(ComplexUnitType.Sp, tertiaryTextSize);
            }

            Drawable ctaBackground = styles.CallToActionBackgroundColor;
            if (ctaBackground != null && callToActionView != null)
            {
                callToActionView.Background = ctaBackground;
            }

            Drawable primaryBackground = styles.PrimaryTextBackgroundColor;
            if (primaryBackground != null && primaryView != null)
            {
                primaryView.Background= primaryBackground;
            }

            Drawable secondaryBackground = styles.SecondaryTextBackgroundColor;
            if (secondaryBackground != null && secondaryView != null)
            {
                secondaryView.Background = secondaryBackground;
            }

            Drawable tertiaryBackground = styles.TertiaryTextBackgroundColor;
            if (tertiaryBackground != null && tertiaryView != null)
            {
                tertiaryView.Background = tertiaryBackground;
            }
        }

        private bool AdHasOnlyStore(UnifiedNativeAd nativeAd)
        {
            string store = nativeAd.Store;
            string advertiser = nativeAd.Advertiser;
            return !string.IsNullOrEmpty(store) && !string.IsNullOrEmpty(advertiser);
        }

        public void SetNativeAd(UnifiedNativeAd nativeAd)
        {
            this.nativeAd = nativeAd;

            string store = nativeAd.Store;
            string advertiser = nativeAd.Advertiser;
            string headline = nativeAd.Headline;
            string body = nativeAd.Body;
            string cta = nativeAd.CallToAction;
            int starRating = Convert.ToInt32(nativeAd.StarRating);
            NativeAd.Image icon = nativeAd.Icon;
            string secondaryText = string.Empty;

            nativeAdView.CallToActionView = callToActionView;
            nativeAdView.HeadlineView = primaryView;
            nativeAdView.MediaView = mediaView;
            secondaryView.Visibility = ViewStates.Visible;
            if (AdHasOnlyStore(nativeAd))
            {
                nativeAdView.StoreView = secondaryView;
                secondaryText = store;
            }
            else if (!string.IsNullOrEmpty(advertiser))
            {
                nativeAdView.AdvertiserView = secondaryView;
                secondaryText = advertiser;
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
        public void DestroyNativeAd()
        {
            nativeAd.Destroy();
        }

        public string GetTemplateTypeName()
        {
            if (templateType == Resource.Layout.gnt_medium_template_view)
            {
                return MEDIUM_TEMPLATE;
            }
            else if (templateType == Resource.Layout.gnt_small_template_view)
            {
                return SMALL_TEMPLATE;
            }
            return string.Empty;
        }

        private void InitView(Context context, IAttributeSet attributeSet)
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