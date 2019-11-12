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
using Android.Util;
//using System.Timers;
using testQuizBazaDanych.Resources.Tabele;
using System.Threading.Tasks;


using static Java.Util.Jar.Attributes;

//using Java.Util;

using System.Threading;
using testQuizBazaDanych.Utility;

namespace testQuizBazaDanych
{
    [Activity(Label = "testQuizBazaDanych")]
    class PytaniaActivity : Activity
    {
        DataBase db;
        private Button btnOdp1;
        private Button btnOdp2;
        private Button btnOdp3;
        private Button btnOdp4;
        private TextView txtpytanie;
        Pytania pytanie;
        Int32 czas = 30;
        Int32 zycia = 3;
        Int32 punkty = 0;
        Int32 idPytania;
        string kategoria;
       
        List<Pytania> lstSource = new List<Pytania>();

        private System.Timers.Timer _timer;
        private int _countSeconds;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.questions);

            var idPytaniaV = Intent.Extras.GetInt("nazwa");
            idPytania = idPytaniaV;
            if(idPytania != 0)
            {
                idPytania += 1;
            }
           

            db = new DataBase();
            db.createDatabasePytania();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            if (lstSource.Count < 10)
            {
                
                Dodaj_Pytania();
            }

            lstSource = db.selectTablePytania();
            find_views();
            punktyView.Text = "punkty" + punkty.ToString();
            czasView.Text = "czas" + czas.ToString();
            zyciaView.Text = "zycia" + zycia.ToString();
            losuj_pytanie(kategoria);
            load_data();
            handle_events();
            //StartTimer(1000,);
            _timer = new System.Timers.Timer();
            //Trigger event every second
            _timer.Interval = 1000;
            _timer.Elapsed += OnTimedEvent;
            //count down 5 seconds
            _countSeconds = czas;

            _timer.Enabled = true;
        }


        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            _countSeconds--;

            //Update visual representation here
            //Remember to do it on UI thread
            RunOnUiThread(() => {

            string casek = _countSeconds.ToString();
            czasView.Text = "czas" + casek;

           

            if (_countSeconds <= 0)
            {
               
                _timer.Enabled = false;
                Odpowiedz_Zla();
                _timer.Enabled = true;

            }
            });

        }
           



        private Pytania losuj_pytanie(string nazwa)
        {
            Pytania p = new Pytania();
            Random generator = new Random(System.DateTime.Now.Millisecond);
            int numerPytania;
            int lstLength = lstSource.Count;
            while(true )
            {
                numerPytania = generator.Next(1,lstLength);
                p = lstSource[numerPytania];
                if (idPytania != 0)
                {
                    
                    if (p.Id_kategorii == idPytania)
                    {
                        return p;
                    }

                }
                else
                    return p;
            }

        }

        private int Timer_Tick()
        {
            throw new NotImplementedException();
        }

        private void Ustaw_idPytania()
        {
            switch(kategoria)
            {
                
            }
        }

    
        ImageView imageView;
        TextView zyciaView;
        TextView czasView;
        TextView punktyView;
        public void find_views()
        {
             btnOdp1 = FindViewById<Button>(Resource.Id.answear1);
             btnOdp2 = FindViewById<Button>(Resource.Id.answear2);
             btnOdp3 = FindViewById<Button>(Resource.Id.answear3);
             btnOdp4 = FindViewById<Button>(Resource.Id.answear4);
             txtpytanie = FindViewById<TextView>(Resource.Id.textviewPytanie);
             imageView = FindViewById<ImageView>(Resource.Id.imageViewQuestion);
            zyciaView = FindViewById<TextView>(Resource.Id.textviewZycia);
            czasView = FindViewById<TextView>(Resource.Id.textviewCzas);
            punktyView = FindViewById<TextView>(Resource.Id.textviewPunkty);
        }
        private void load_data()
        {
            pytanie = losuj_pytanie(kategoria);
            btnOdp1.Text = pytanie.Odp1;
            btnOdp2.Text = pytanie.Odp2;
            btnOdp3.Text = pytanie.Odp3;
            btnOdp4.Text = pytanie.Odp4;
            txtpytanie.Text = pytanie.Pytanie;
            
            int nazwa = pytanie.Obrazek;
            imageView.SetImageResource(nazwa);
            LinearLayout widok =(LinearLayout) FindViewById(Resource.Id.questionsLayout);
            LinearLayout widok2 = (LinearLayout)FindViewById(Resource.Id.questionsLayout2);
           
            if (pytanie.Poziom == 1)
            {
                
                widok.SetBackgroundColor(Android.Graphics.Color.Green);
                widok2.SetBackgroundColor(Android.Graphics.Color.Green);
            }
            else if(pytanie.Poziom == 2)
            {
                

                widok2.SetBackgroundColor(Android.Graphics.Color.Orange);
            }
            else if(pytanie.Poziom == 3)
            {
               
                widok.SetBackgroundColor(Android.Graphics.Color.Red);
                widok.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            
        }

        public void handle_events()
        {
            btnOdp1.Click += odp1_Click;
            btnOdp2.Click += odp2_Click;
            btnOdp3.Click += odp3_Click;
            btnOdp4.Click += odp4_Click;
        }

      
     

        private void Dodaj_Pytania()
        {
            Pytania samochody = new Pytania(Resource.Drawable.nissan, "Jakie to marka", "nissan", "mazda", "ford", "chevrolet", "nissan", 3, 1);
            db.InsertIntoTablePytania(samochody);

            Pytania samochody1 = new Pytania(Resource.Drawable.nissaniaudi, "Jakie to auta", "nissan", "nissan i audi", "ford", "chevrolet", "nissan i audi", 3, 1);
            db.InsertIntoTablePytania(samochody1);

            Pytania samochody2 = new Pytania(Resource.Drawable.s13, "Jaki kod ma to auto?", "B13", "S13", "S14", "SR20", "S13", 3, 1);
            db.InsertIntoTablePytania(samochody2);

            Pytania samochody3 = new Pytania(Resource.Drawable.rb26, "Jaki silnik posiada to auto?", "2JZ GTE", "SR20DET", "VR38DETT","RB26DETT", "RB26DETT",3,2);
            db.InsertIntoTablePytania(samochody3);

            Pytania samochody4 = new Pytania(Resource.Drawable.m13B, "Jaki typ silnika ma to auto?", "Wankla", "Bokser", "V6", "Gwiazdowy", "Wankla", 3, 2);
            db.InsertIntoTablePytania(samochody4);

            Pytania geografia11 = new Pytania(Resource.Drawable.chiny, "Jakie miasto jest stolicą Chin? ", "Pekin", "Singapur", "Szanghaj", "Kanton", "Pekin", 2, 1);
            db.InsertIntoTablePytania(geografia11);

            Pytania geografia21 = new Pytania(Resource.Drawable.sekwana, "Jaka rzeka płynie przez Paryż", "Dunaj", "Sekwana", "Dunajec", "Tamiza", "Sekwana", 2, 1);
            db.InsertIntoTablePytania(geografia21);

            Pytania geografia32 = new Pytania(Resource.Drawable.everest, "Na jakiej wysokości znajduje się najwyższy szczyt świata", "8888", "8148", "8848", "8611", "8848", 2, 2);
            db.InsertIntoTablePytania(geografia32);

            Pytania geografia43 = new Pytania(Resource.Drawable.stalagnat, "Co przedstawia zdjęcie ?", "Stalagmit ", "Stalaktyt", "Polje", "Stalagnat", "Stalagnat", 2, 3);
            db.InsertIntoTablePytania(geografia43);

            Pytania matematyka11 = new Pytania(Resource.Drawable.trojkat90, "Jaka miarę ma ostatni kąt?", "60", "30", "90", "45", "90", 4, 1);
            db.InsertIntoTablePytania(matematyka11);

            Pytania matematyka21 = new Pytania(Resource.Drawable.p1235, "Jaka liczbe nalezy podniesc do kwadratu, żeby otrzymać ten wynik? ", "1222", "1235", "1243", "1234", "1235", 4, 1);
            db.InsertIntoTablePytania(matematyka21);

            Pytania matematyka32 = new Pytania(Resource.Drawable.szesciokat, "Jaka jest miara katow wewnetrzych danej figury?", "360", "1080", "720", "480", "720", 4, 2);
            db.InsertIntoTablePytania(matematyka32);

            Pytania matematyka42 = new Pytania(Resource.Drawable.deltoid, "Jaki jest wzór na pole deltoidu?", "(a*b)/2", "1/2*a * b ", "1/2 * b * a", " a * a * b * b", "(a*b)/2", 4, 2);
            db.InsertIntoTablePytania(matematyka42);

            Pytania fizyka11 = new Pytania(Resource.Drawable.uklad, "Ile jest planek w ukłądzie słonecznym", "10", "5", "8", "9", "8", 5, 1);
            db.InsertIntoTablePytania(fizyka11);

            Pytania fizyka21 = new Pytania(Resource.Drawable.wzglednosc, "Kto jest autorem teorii względności?","Stefan Banach","Michael Faraday ","Nicola Tesla", "Albert Einstein", "Albert Einstein", 5, 1);
            db.InsertIntoTablePytania(fizyka21);

            Pytania fizyka31 = new Pytania(Resource.Drawable.stany, "Jak nazywa się przejście z gazowego w stały?", "resublimacja", "krzepnięcie", "topnienie", "sublimacja", "resublimacja", 5, 1);
            db.InsertIntoTablePytania(fizyka31);

            Pytania fizyka42 = new Pytania(Resource.Drawable.amper, "Jaką jednostką miary jest amper?", "Pojemności opornika ", "Przyspieszenia prądu", "Natężenia prądu", "Napięcia", "Natężenia prądu", 5, 2);
            db.InsertIntoTablePytania(fizyka42);

            Pytania informatyka11 = new Pytania(Resource.Drawable.dwojkowy, "Jak zapisać widoczną liczbę w systemie dwójkowym?", "1000", "1010", "1110", "1100", "1100", 6, 1);
            db.InsertIntoTablePytania(informatyka11);

            Pytania informatyka21 = new Pytania(Resource.Drawable.ipv4, "Na ilu bitach zapisany jest adres ipV4?", "64", "8", "16", "32", "32", 6, 1);
            db.InsertIntoTablePytania(informatyka21);

            Pytania informatyka32 = new Pytania(Resource.Drawable.cpu, "CPU to skrót od?", "Central Progressing Unit", " Central Processing Unit", "Center Processing Unit","Center Progressing Utility", " Central Processing Unit", 6, 3);
            db.InsertIntoTablePytania(informatyka32);

        }

        private void odp4_Click(object sender, EventArgs e)
        {
            if(btnOdp4.Text == pytanie.Prawidlowa)
            {
                if (pytanie.Poziom == 1)
                    punkty += 5;
                else if (pytanie.Poziom == 2)
                    punkty += 10;
                else
                    punkty += 15;
                punktyView.Text = "punkty" + punkty.ToString();
                losuj_pytanie(kategoria);
                load_data();
                czas = 30;
                _countSeconds = czas;
                
            }
            else
            {

                zycia -= 1;
                zyciaView.Text = "zycia" + zycia.ToString();
                if (zycia == 0)
                {
                    var intent = new Intent(this, typeof(KoncoweActivity));
                    intent.PutExtra("punkty1",punkty);
                    StartActivity(intent);
                }
                else
                {
                    losuj_pytanie(kategoria);
                    load_data();
                    czas = 30;
                    _countSeconds = czas;
                }
            }
        }

        private void odp3_Click(object sender, EventArgs e)
        {
            if (btnOdp3.Text == pytanie.Prawidlowa)
            {
                if (pytanie.Poziom == 1)
                    punkty += 5;
                else if (pytanie.Poziom == 2)
                    punkty += 10;
                else
                    punkty += 15;
                punktyView.Text = "punkty" + punkty.ToString();
                losuj_pytanie(kategoria);
                load_data();
                czas = 30;
                _countSeconds = czas;
            }
            else
            {

                zycia -= 1;
                zyciaView.Text = "zycia" + zycia.ToString();
                if (zycia == 0)
                {
                    var intent = new Intent(this, typeof(KoncoweActivity));
                    intent.PutExtra("punkty1", punkty);
                    StartActivity(intent);
                }
                else
                {
                    losuj_pytanie(kategoria);
                    load_data();
                    czas = 30;
                    _countSeconds = czas;
                }
            }
        }

        private void odp2_Click(object sender, EventArgs e)
        {
            if (btnOdp2.Text == pytanie.Prawidlowa)
            {
                Odpowiedz_Prawidlowa();
            }
            else
            {

                Odpowiedz_Zla();
            }
        }

        private void odp1_Click(object sender, EventArgs e)
        {
            if (btnOdp1.Text == pytanie.Prawidlowa)
            {
                Odpowiedz_Prawidlowa();
            }
            else
            {

                Odpowiedz_Zla();
            }
        }

        public void Odpowiedz_Prawidlowa()
        {
            if (pytanie.Poziom == 1)
                punkty += 5;
            else if (pytanie.Poziom == 2)
                punkty += 10;
            else
                punkty += 15;
            punktyView.Text = "punkty" + punkty.ToString();
            Kolejne_Pytanie();
        }

        public void Odpowiedz_Zla()
        {
            zycia -= 1;
            zyciaView.Text = "zycia" + zycia.ToString();
            if (zycia == 0)
            {
                Koniec_Gry();
            }
            else
            {
                Kolejne_Pytanie();
                _timer.Enabled = true;
            }
        }

        public void Koniec_Gry()
        {
            var intent = new Intent(this, typeof(KoncoweActivity));
            intent.PutExtra("punkty1", punkty);
            StartActivity(intent);
        }

        public void Kolejne_Pytanie()
        {
            losuj_pytanie(kategoria);
            load_data();
            czas = 30;
            _countSeconds = czas;
        }
    }
}