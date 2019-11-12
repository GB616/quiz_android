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
using Android.Database.Sqlite;

namespace testQuizBazaDanych.Resources.DataHelper
{
    public class DataBaseHelper : SQLiteOpenHelper
    {
        public static string database_name = "Baza";
        public static string database_table = "Wyniki";
        public DataBaseHelper(Context context) : base(context, database_name, null, 1)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("create table " + database_table + " (ID INTEGER PRIMARY KEY AUTOINCREMENT, NICK TEXT , PUNKTY TEXT )");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("drop table " + database_table);
            OnCreate(db);
        }

        public Boolean WstawianieDOBazy(string nick , string punkty)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues cv = new ContentValues();
            cv.Put("NICK", nick);
            cv.Put("PUNKTY", punkty);
            if (db.Insert(database_table, null, cv) == -1)
                return false;
            else
                return true;
        }

        public SQLiteCursor pobierz()
        {
            SQLiteDatabase db = this.WritableDatabase;
            SQLiteCursor kursor = (SQLiteCursor)db.RawQuery("SELECT * FROM " + database_table + " ORDER BY PUNKTY DESC", null);// + "ORDER BY 3 DESC", null);
            return kursor;
        }
    }
}