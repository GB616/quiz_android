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
    public class Kategorie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

    public Kategorie( string n)
    {
        
        Name = n;
    }

    public Kategorie()
    {

    }

    public override string ToString()
    {
        return  Name ;
    }


    }

}