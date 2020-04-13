using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Gms.Ads.Formats;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XamarinAdsSample
{
    class NativeRecyclerAdapter : RecyclerView.Adapter
    {
        static int VIEW_ADS = 0;
        static int VIEW_NORMAL = 1;
        List<Object> data = new List<Object>();

         public void SetData(List<Object> list)
        {
            var ads = list.Where(p=>p is UnifiedNativeAd).Select(p=>p).ToList();
            var items = new Queue<Object>(list.Except(ads));
            int offset = (items.Count / ads.Count) + 1;
            
            foreach(var ad in ads)
            {
                this.data.Add(ad);
                int i = 0;
                while (items.Any() && i <= offset)
                {
                    this.data.Add(items.Dequeue());
                    i++;
                }
            }
            NotifyDataSetChanged();
        }

        public NativeRecyclerAdapter() { }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == VIEW_ADS)
            {
                var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ads_unified, parent, false);
                return new AdsViewHolder(view);
            }
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.menu_item_container, parent, false);
            return new MenuViewHolder(v);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            if (viewHolder is AdsViewHolder adH)
            {
                var ads = data[position] as UnifiedNativeAd;
                adH.AdsUnified.PopulateNativeAds(ads);
            }
            else if(viewHolder is MenuViewHolder mh)
            {
                var item = data[position] as MenuItem;
                mh.ItemName.Text = item.Name;
                mh.ItemDescription.Text = item.Description;
                mh.ItemPrice.Text = item.Price;
                mh.ItemCategory.Text = item.Category;
            }
        }

        public override int ItemCount => data.Count;


        public override int GetItemViewType(int position)
        {
            if (data[position] is UnifiedNativeAd) return VIEW_ADS;
            return VIEW_NORMAL;
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


        /// <summary>
        /// Hold Item
        /// </summary>
        public class MenuViewHolder : RecyclerView.ViewHolder
        {
            public TextView ItemName { get; set; }
            public TextView ItemDescription { get; set; }
            public TextView ItemPrice { get; set; }
            public TextView ItemCategory { get; set; }
            public ImageView ItemImage { get; set; }
            public MenuViewHolder(View view) : base(view)
            {
                ItemImage = view.FindViewById< ImageView>(Resource.Id.menu_item_image);
                ItemName = view.FindViewById<TextView>(Resource.Id.menu_item_name);
                ItemPrice = view.FindViewById<TextView>(Resource.Id.menu_item_price);
                ItemCategory = view.FindViewById<TextView>(Resource.Id.menu_item_category);
                ItemDescription = view.FindViewById<TextView>(Resource.Id.menu_item_description);
            }
        }
    }

    




}