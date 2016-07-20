using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using Android.Graphics;
using System.Collections.Generic;
using MyCircle.Resources;

namespace MyCircle
{
    [Activity(Label = "Circle", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        RelativeLayout rl;
        public List<Circle> circles = new List<Circle>();
        View1 view1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            rl = FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
            view1 = new View1(this, null);
            rl.AddView(view1);
            button.Click += button_Click;
        }

        void button_Click(object sender, EventArgs e)
        {
            circles.Add(new Circle());
            view1.Invalidate();
            //rl.Invalidate();
            //WebClient wc = new WebClient();
            //Stream s = wc.OpenRead("http://images.bjjtgl.gov.cn/portals/0/newimages/lukuangmap.jpg");
        }
    }
}

