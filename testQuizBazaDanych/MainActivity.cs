using Android.App;
using Android.Widget;
using Android.OS;
using testQuizBazaDanych.Resources.DataHelper;
using Android.Util;
using testQuizBazaDanych.Resources.Tabele;
using System;
using Android.Content;
using System.Collections.Generic;

namespace testQuizBazaDanych
{
    [Activity(Label = "testQuizBazaDanych", MainLauncher = true)]
    public class MainActivity : Activity
    {
       
        private Button play;
        private Button options;
        private Button leaderboard;
        private Button exit;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
           
            Find_Views();
            HandleEvents();
        }

        public void Find_Views()
        {
            exit = FindViewById<Button>(Resource.Id.buttonExit);
            leaderboard = FindViewById<Button>(Resource.Id.buttonLeaderboard);
            play = FindViewById<Button>(Resource.Id.buttonPlay);
            options = FindViewById<Button>(Resource.Id.buttonOptions);

        }

        public void HandleEvents()
        {
            exit.Click += exit_CLick;
            leaderboard.Click += leaderboard_Click;
            play.Click += play_Click;
            options.Click += options_Click;

        }

        private void options_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(OptionsActivity));
            StartActivity(intent);
        }

        private void play_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(wyborKAtegoriiActivity));
            //intent.PutExtra();
            StartActivity(intent);
        }

        private void leaderboard_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LeaderBoardActivity));
            StartActivity(intent);
        }

        private void exit_CLick(object sender, EventArgs e)
        {
            Finish();
            //System.Exit(0);
        }
       
    }
}

