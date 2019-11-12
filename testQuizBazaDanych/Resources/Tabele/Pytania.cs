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
using SQLite;

namespace testQuizBazaDanych.Resources.Tabele
{
    public class Pytania
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public ImageView Obrazek{get;set;}
        public int Obrazek { get; set; }
        public string Pytanie { get; set; }
        public string Odp1 { get; set; }
        public string Odp2 { get; set; }
        public string Odp3 { get; set; }
        public string Odp4 { get; set; }
        public string Prawidlowa { get; set; }
        public int Id_kategorii { get; set; }
        public int Poziom { get; set; }

       

        public Pytania(int obrazek, string pytanie, string odp1, string odp2, string odp3, string odp4, string prawidlowa, int idk, int poziom)
        {
            Obrazek = obrazek;
            Pytanie = pytanie;
            Odp1 = odp1;
            Odp2 = odp2;
            Odp3 = odp3;
            Odp4 = odp4;
            Prawidlowa = prawidlowa;
            Id_kategorii = idk;
            Poziom = poziom;
        }

        public Pytania()
        {

        }

    }
}