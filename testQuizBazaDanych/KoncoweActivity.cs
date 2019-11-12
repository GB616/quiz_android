using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using testQuizBazaDanych.Resources.DataHelper;
using Android.Database.Sqlite;
using Java.Lang;

namespace testQuizBazaDanych
{
    [Activity(Label = "testQuizBazaDanych")]
   
    class KoncoweActivity :Activity
    {
        private Button btnSave;
        private Button btnShow;
        private TextView txtPunkty;
        private AutoCompleteTextView txtNazwa;
        int punktyo;
        DataBaseHelper db;

        protected override void OnCreate(Bundle savedInstanceState)    
        {

             db = new DataBaseHelper(this);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ending);
            //base.OnCreate(Intent.GetIntExtra(name: "punkty1", defaultValue: 0);

            // Set our view from the "main" layout resource

            Find_views();
            
            var punktyK = Intent.Extras.GetInt("punkty1");
            Int32 punktyo = punktyK;
             txtPunkty.Text = punktyo.ToString();
            
            lstData = FindViewById<ListView>(Resource.Id.listViewWyniki);
            Load_data();
            btnSave.Click += save_Click;
            btnShow.Click += show_Click;
        }

        private void show_Click(object sender, EventArgs e)
        {
            pokazwyniki();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string nazwa = txtNazwa.Text;
           
            bool czyUdane;
            string punktys = punktyo.ToString();
            czyUdane = db.WstawianieDOBazy(nazwa, txtPunkty.Text );
            if(czyUdane)
            {
                Toast.MakeText(this, "Udało się", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Nie udało się", ToastLength.Long).Show();
            }
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public void Find_views()
        {
           btnSave = FindViewById<Button>(Resource.Id.btnSave);
           txtPunkty = FindViewById<TextView>(Resource.Id.textPunkty);
            txtNazwa = FindViewById<AutoCompleteTextView>(Resource.Id.ACtextViewNazwa);
            btnShow = FindViewById<Button>(Resource.Id.btnShow);
        }

        ListView lstData;
        List<string> listWyniki = new List<string>();
        public void pokazwyniki()
        {
            SQLiteCursor kursor = db.pobierz();
            if(kursor.Count > 0)
            {
                Toast.MakeText(this, "coś jest", ToastLength.Long).Show();
                StringBuffer buff = new StringBuffer();
                int i = 1;
                while (kursor.MoveToNext())
                    
                {
                    buff.Append(i +"  " + kursor.GetString(1) + "       " + kursor.GetString(2)+ "\n");
                    ++i;
                }
                listWyniki.Add(buff.ToString());
                PokazWiadomosc("Wynik", buff.ToString());
                
            }
            else
            {
                PokazWiadomosc("Brak", "NIe udało sie");
            }
        }

        public void Load_data()
        {
            var adapter = new ListViewWynikiAdapter(this, listWyniki);
            lstData.Adapter = adapter;
        }

        public void PokazWiadomosc(string tytul, string wiadomosc)
        {
            AlertDialog.Builder budowniczy = new AlertDialog.Builder(this);
            budowniczy.SetCancelable( true);
            budowniczy.SetMessage(wiadomosc);
            budowniczy.SetTitle(tytul);
            budowniczy.Show();

        }

    }
}