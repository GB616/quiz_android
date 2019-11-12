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
using testQuizBazaDanych.Resources.Tabele;
using testQuizBazaDanych.Resources.DataHelper;
using Android.Util;

namespace testQuizBazaDanych
{
    [Activity(Label = "testQuizBazaDanych")]
    class wyborKAtegoriiActivity : Activity
    {
        DataBase db;
        ListView lstData;
        List<Kategorie> lstSource = new List<Kategorie>();


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.wyborKategorii);

            db = new DataBase();
            db.createDatabase();
            List<Kategorie> lstSource1 = db.selectTableKategorie();
            
            foreach ( Kategorie k in lstSource1)
            {
                db.deleteTableKategorie(k);
            }



            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            lstSource = db.selectTableKategorie();
            if(lstSource.Count == 0)
            {
                Dodaj_Kategorie();
           

            }
            lstData = FindViewById<ListView>(Resource.Id.listViewWyborKategorii);
            LoadData1();

            lstData.ItemClick += (s,e) =>
             {
                for(int i =0;i<lstData.Count;++i)
                {
                    if (e.Position == i)
                     {
                        
                         int nazwa;
                         nazwa = i;
                         var  intent = new Intent(this, typeof(PytaniaActivity));
                         intent.PutExtra("nazwa", nazwa);
                         StartActivity(intent);

                     }
                     
                 
                  }

             

                

               
            };

        }

        private void Dodaj_Kategorie()
        {
            Kategorie wszystkie = new Kategorie("Wszystkie");
            db.InsertIntoTableKategorie(wszystkie);

            Kategorie geografia = new Kategorie("Geografia");
            db.InsertIntoTableKategorie(geografia);

            Kategorie samochody = new Kategorie("Samochody");
            db.InsertIntoTableKategorie(samochody);

            Kategorie matematyka = new Kategorie("Matematyka");
            db.InsertIntoTableKategorie(matematyka);

            Kategorie fizyka = new Kategorie("Fizyka");
            db.InsertIntoTableKategorie(fizyka);

            Kategorie informatyka = new Kategorie("Informatyka");
            db.InsertIntoTableKategorie(informatyka);

            Kategorie chemia = new Kategorie("Chemia");
            db.InsertIntoTableKategorie(chemia);

            Kategorie biologia = new Kategorie("Biologia");
            db.InsertIntoTableKategorie(biologia);

            Kategorie polski = new Kategorie("Język Polski");
            db.InsertIntoTableKategorie(polski);

            Kategorie angielski = new Kategorie("Język Angielski");
            db.InsertIntoTableKategorie(angielski);

            Kategorie historia = new Kategorie("Historia");
            db.InsertIntoTableKategorie(historia);

            //Kategorie samochody = new Kategorie("samochody");
            //db.InsertIntoTableKategorie(samochody);


        }

        private void LoadData1()
        {
            lstSource = db.selectTableKategorie();
            var adapter = new lvWyborKategoriiAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }





}
}
