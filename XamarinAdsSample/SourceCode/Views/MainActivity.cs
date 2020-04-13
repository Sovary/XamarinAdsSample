using System;
using Android.App;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace XamarinAdsSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        AdsManager adsMgr;

        TextView txtStatusInter;
        Button btnLoadInter, btnShowInter, btnOpenActiveNative;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            ///Init ManagerAds
            adsMgr = new AdsManager(ApplicationContext);
            txtStatusInter = FindViewById<TextView>(Resource.Id.txtStateInterstitial);
            btnLoadInter = FindViewById<Button>(Resource.Id.btnLoad);
            btnShowInter = FindViewById<Button>(Resource.Id.btnShowInter);
            btnOpenActiveNative = FindViewById<Button>(Resource.Id.btnOpenNative);

            btnLoadInter.Click += BtnLoadInter_Click;
            btnShowInter.Click += BtnShowInter_Click;

            btnOpenActiveNative.Click += BtnOpenActiveNative_Click;

            //Implement Banner Ads
            var adview =  FindViewById<AdView>(Resource.Id.adView);
            adsMgr.CreateAds(adview);

            var builder = new AdLoader.Builder(ApplicationContext, GetString(Resource.String.admobNative));
            var listen = new AdsUnifiedLoadListening();
            listen.Ads_UnifiedNativeLoad += (obj, unifiedAds) =>
            {
                var style = new NativeTemplateStyle.Builder().build();
                TemplateView view = FindViewById<TemplateView>(Resource.Id.adviewTemplate);
                view.Visibility = ViewStates.Visible;
                view.setStyles(style);
                view.setNativeAd(unifiedAds);
            };
            builder.ForUnifiedNativeAd(listen);
            builder.WithAdListener(listen);
            builder.WithNativeAdOptions(new Android.Gms.Ads.Formats.NativeAdOptions.Builder().Build());

            AdLoader adloader = builder.Build();
            adloader.LoadAd(new AdRequest.Builder().Build());
            if (listen.Tag is null)
            {
                listen.Tag = adloader;
            }
        }

        private void BtnOpenActiveNative_Click(object sender, EventArgs e)
        {
            StartActivity(new Android.Content.Intent(ApplicationContext,typeof(NativeAdsActivity)));
        }

        AdsListening AdsListening
        {
            get
            {
                AdsListening listen = new AdsListening();
                listen.Ads_LoadFailed += (sen, err) =>
                {
                    //Handle error
                    txtStatusInter.Text = $"Interstitail load failed code: {err}";
                };
                listen.Ads_LeftApp += (sen, ev) =>
                {
                    //Handle clicked and left application
                    txtStatusInter.Text = $"Interstitail clicked and Left App";
                };
                listen.Ads_Loaded += (s, e) =>
                {
                    //Handle ads loaded
                    txtStatusInter.Text = $"Interstitail ready to show";
                };
                return listen;
            }
        }

        private void BtnShowInter_Click(object sender, EventArgs e)
        {
            if(!adsMgr.IsInterstitialAdsInit)
            {
                Toast.MakeText(this, "Not yet load interstitial", ToastLength.Short);
                return;
            }
            var inters = adsMgr.GetInterstitialAds();
            if (inters.IsLoaded)
            {
                inters.Show();
                txtStatusInter.Text = "Status";
            }
        }

        /// <summary>
        /// Preload ads purpose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadInter_Click(object sender, EventArgs e)
        {
            adsMgr.CreateInterstitialAds(AdsListening);
            txtStatusInter.Text = $"Created interstitial ads wait...";
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

