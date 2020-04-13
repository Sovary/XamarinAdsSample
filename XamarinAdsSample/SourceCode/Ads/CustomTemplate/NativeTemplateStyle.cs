using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    public sealed class NativeTemplateStyle : Java.Lang.Object
    {
        public Typeface CallToActionTextTypeface { get; set; }

        // Size of call to action text.
        public float CallToActionTextSize { get; set; }

        // Call to action typeface color in the form 0xAARRGGBB.
        public int CallToActionTypefaceColor { get; set; }

        // Call to action background color.
        public ColorDrawable CallToActionBackgroundColor { get; set; }

        // All templates have a primary text area which is populated by the native ad's headline.

        // Primary text typeface.
        public Typeface PrimaryTextTypeface { get; set; }

        // Size of primary text.
        public float PrimaryTextSize { get; set; }

        // Primary text typeface color in the form 0xAARRGGBB.
        public int PrimaryTextTypefaceColor { get; set; }

        // Primary text background color.
        public ColorDrawable PrimaryTextBackgroundColor { get; set; }

        // The typeface, typeface color, and background color for the second row of text in the template.
        // All templates have a secondary text area which is populated either by the body of the ad or
        // by the rating of the app.

        // Secondary text typeface.
        public Typeface SecondaryTextTypeface { get; set; }

        // Size of secondary text.
        public float SecondaryTextSize { get; set; }

        // Secondary text typeface color in the form 0xAARRGGBB.
        public int SecondaryTextTypefaceColor { get; set; }

        // Secondary text background color.
        public ColorDrawable SecondaryTextBackgroundColor { get; set; }

        // The typeface, typeface color, and background color for the third row of text in the template.
        // The third row is used to display store name or the default tertiary text.

        // Tertiary text typeface.
        public Typeface TertiaryTextTypeface { get; set; }

        // Size of tertiary text.
        public float TertiaryTextSize { get; set; }

        // Tertiary text typeface color in the form 0xAARRGGBB.
        public int TertiaryTextTypefaceColor { get; set; }

        // Tertiary text background color.
        public ColorDrawable TertiaryTextBackgroundColor { get; set; }

        // The background color for the bulk of the ad.
        public ColorDrawable MainBackgroundColor { get; set; }

        /** A class that provides helper methods to build a style object. * */
        public sealed class Builder : Java.Lang.Object
        {

            private NativeTemplateStyle styles;

            public Builder()
            {
                this.styles = new NativeTemplateStyle();
            }

            public Builder withCallToActionTextTypeface(Typeface callToActionTextTypeface)
            {
                this.styles.CallToActionTextTypeface = callToActionTextTypeface;
                return this;
            }

            public Builder withCallToActionTextSize(float callToActionTextSize)
            {
                this.styles.CallToActionTextSize = callToActionTextSize;
                return this;
            }

            public Builder withCallToActionTypefaceColor(int callToActionTypefaceColor)
            {
                this.styles.CallToActionTypefaceColor = callToActionTypefaceColor;
                return this;
            }

            public Builder withCallToActionBackgroundColor(ColorDrawable callToActionBackgroundColor)
            {
                this.styles.CallToActionBackgroundColor = callToActionBackgroundColor;
                return this;
            }

            public Builder withPrimaryTextTypeface(Typeface primaryTextTypeface)
            {
                this.styles.PrimaryTextTypeface = primaryTextTypeface;
                return this;
            }

            public Builder withPrimaryTextSize(float primaryTextSize)
            {
                this.styles.PrimaryTextSize = primaryTextSize;
                return this;
            }

            public Builder withPrimaryTextTypefaceColor(int primaryTextTypefaceColor)
            {
                this.styles.PrimaryTextTypefaceColor = primaryTextTypefaceColor;
                return this;
            }

            public Builder withPrimaryTextBackgroundColor(ColorDrawable primaryTextBackgroundColor)
            {
                this.styles.PrimaryTextBackgroundColor = primaryTextBackgroundColor;
                return this;
            }

            public Builder withSecondaryTextTypeface(Typeface secondaryTextTypeface)
            {
                this.styles.SecondaryTextTypeface = secondaryTextTypeface;
                return this;
            }

            public Builder withSecondaryTextSize(float secondaryTextSize)
            {
                this.styles.SecondaryTextSize = secondaryTextSize;
                return this;
            }

            public Builder withSecondaryTextTypefaceColor(int secondaryTextTypefaceColor)
            {
                this.styles.SecondaryTextTypefaceColor = secondaryTextTypefaceColor;
                return this;
            }

            public Builder withSecondaryTextBackgroundColor(ColorDrawable secondaryTextBackgroundColor)
            {
                this.styles.SecondaryTextBackgroundColor = secondaryTextBackgroundColor;
                return this;
            }

            public Builder withTertiaryTextTypeface(Typeface tertiaryTextTypeface)
            {
                this.styles.TertiaryTextTypeface = tertiaryTextTypeface;
                return this;
            }

            public Builder withTertiaryTextSize(float tertiaryTextSize)
            {
                this.styles.TertiaryTextSize = tertiaryTextSize;
                return this;
            }

            public Builder withTertiaryTextTypefaceColor(int tertiaryTextTypefaceColor)
            {
                this.styles.TertiaryTextTypefaceColor = tertiaryTextTypefaceColor;
                return this;
            }

            public Builder withTertiaryTextBackgroundColor(ColorDrawable tertiaryTextBackgroundColor)
            {
                this.styles.TertiaryTextBackgroundColor = tertiaryTextBackgroundColor;
                return this;
            }

            public Builder withMainBackgroundColor(ColorDrawable mainBackgroundColor)
            {
                this.styles.MainBackgroundColor = mainBackgroundColor;
                return this;
            }

            public NativeTemplateStyle build()
            {
                return styles;
            }
        }
    }
}
    