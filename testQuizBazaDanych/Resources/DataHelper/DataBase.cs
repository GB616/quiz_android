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
using SQLite;
using Android.Util;

namespace testQuizBazaDanych.Resources.DataHelper
{
    class DataBase
    {
        public string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDatabase()
        {
            try
            {
               
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "kategorie1.db3")))
                {
                    connection.CreateTable<Kategorie>();
                   
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool createDatabasePytania()
        {
            try
            {
                 using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Pytania.db3")))
                {

                  connection.CreateTable<Pytania>();
                 return true;
                }
               
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
        public bool InsertIntoTableKategorie(Kategorie kategorie)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "kategorie1.db3")))
                {
                    connection.Insert(kategorie);
                    
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool InsertIntoTablePytania(Pytania pytania)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Pytania.db3")))
                {
                    connection.Insert(pytania);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public List<Kategorie> selectTableKategorie()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "kategorie1.db3")))
                {
                    return connection.Table<Kategorie>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public List<Pytania> selectTablePytania()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Pytania.db3")))
                {
                    return connection.Table<Pytania>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        //public bool updateTableKategorie(Kategorie kategorie)
        //{
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "kategorie1.db3")))
        //        {
        //            connection.Query<Kategorie>("UPDATE Kategorie set Name -? Where Id - ?", kategorie.Name, kategorie.Id);
        //            return true;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Log.Info("SQLiteEX", ex.Message);
        //        return false;
        //    }
        //}

        //public bool updateTablePytania(Pytania  pytania)
        //{
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "bazaDanychTQ.db3")))
        //        {
        //            connection.Query<Kategorie>("UPDATE Pytania set Obrazek -?, Pytanie -?, Odp1 -?,Odp2 -?,Odp3 -?, Odp4 -?, Prawidlowa -?, Id_kategorii Where Id - ?", pytania.Obrazek,pytania.Pytanie,pytania.Odp1,pytania.Odp2,pytania.Odp3,pytania.Odp4,pytania.Prawidlowa,pytania.Id_kategorii, pytania.Id);
        //            return true;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Log.Info("SQLiteEX", ex.Message);
        //        return false;
        //    }
        //}

        public bool deleteTableKategorie(Kategorie kategorie)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "kategorie1.db3")))
                {
                    connection.Delete(kategorie);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool deleteTablePytania(Pytania pytanie)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "bazaDanychTQ.db3")))
                {
                    connection.Delete(pytanie);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }

        public bool selectQueryTableKategorie(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "kategorie1.db3")))
                {
                    connection.Query<Kategorie>("SELECT * Kategorie  Where Id - ?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return false;
            }
        }
    public bool selectQueryTablePytania(int Id)
    {
        try
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Pytania.db3")))
            {
                connection.Query<Kategorie>("SELECT * Pytania  Where Id - ?", Id);
                return true;
            }
        }
        catch (SQLiteException ex)
        {
            Log.Info("SQLiteEX", ex.Message);
            return false;
        }
    }
    }

}

