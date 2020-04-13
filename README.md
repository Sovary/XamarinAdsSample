# XamarinAdsSample

Sample project for implementation admob on Android Native with C#(Xamarin).

Sample project includes:
- Admob banner
- Interstitial Ads
- Native Ads (Unified)
- Native Ads in Recycler view
- Custom Template Native Ads
<img src="https://github.com/Sovary/XamarinAdsSample/raw/master/banner.png" height="600">
<img src="https://github.com/Sovary/XamarinAdsSample/raw/master/recycler.png" height="600">


## Installation requirement

Use the package manager [nuget](https://www.nuget.org/) to install Google Play Service.

```
Install-Package Xamarin.GooglePlayServices.Ads -Version 71.1720.1
```

## Usage 

### Initialized Ads Manager

```C#
var adsMgr = new AdsManager(ApplicationContext);

```

### Callback listener
To get any events callback status.

```C#
//Implement Banner Ads
var listen = new AdsListening();
listen.Ads_LoadFailed += (sender, errCode) =>
{
    //Handle error
};
listen.Ads_LeftApp += (sender, evt) =>
{
    //Handle clicked and left application
};

//Not available for native ads. Use Ads_UnifiedNativeLoad instead
listen.Ads_Loaded += (sender, evt) =>
{
    //Handle ads loaded
};
```
### Create Banner and Interstitial Ads

```C#
//Banner Ads
var adview =  FindViewById<AdView>(Resource.Id.adView);
//Optional listener
adsMgr.CreateAds(adview,listen);

//Interstitial Ads

//For pre-load ads purpose
//Optional listener
adsMgr.CreateInterstitialAds(AdsListening);

//Ready to show interstitial
var inters = adsMgr.GetInterstitialAds();
if (inters.IsLoaded) inters.Show();
```

### Callback listener Unified Native Ads

```C#
var listen = new AdsUnifiedLoadListening();
listen.Ads_UnifiedNativeLoad += (obj, unifiedAds) =>
{
    var tag = ((AdsUnifiedLoadListening)obj).Tag as AdLoader;

    //Basically, request ads a batch maximum 5 requests.
    //so this handle after loaded finished that 5 requests
    if (!tag.IsLoading)
    {
      //Insert to recycler view or whatever...
    }
};
```

### Created Unified Ads

```C#
/*Number of 5 requests ads*/
adsMgr.CreateUnifiedAds(5, listen);
```



## License
[MIT](https://choosealicense.com/licenses/mit/) kong.sovaryhrdi@gmail.com
