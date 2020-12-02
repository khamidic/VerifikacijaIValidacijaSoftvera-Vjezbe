using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace eParking
{
    public class Lokacija
    {
        #region Atributi

        string naziv;
        List<string> ulice;
        double cijena;
        int kapacitet, brojač;

        #endregion

        #region Properties

        public string Naziv
        {
            get => naziv;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || !value.All(char.IsLetter))
                    throw new ArgumentException("Neispravan format naziva!");

                naziv = value;
            }
        }

        public List<string> Ulice
        {
            get => ulice;
        }

        public double Cijena
        {
            get => cijena;
            set
            {
                if (value < 0.0 || value > 50.00)
                    throw new ArgumentException("Neispravan format cijene!");

                cijena = value;
            }
        }

        public int Kapacitet
        {
            get => kapacitet;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Neispravan format kapaciteta!");

                kapacitet = value;
            }
        }

        #endregion

        #region Konstruktor

        public Lokacija(string name, List<string> streets, double price, int capacity)
        {
            Naziv = name;
            if (streets == null)
                throw new NullReferenceException("Morate specificirati barem jednu ulicu!");

            ulice = streets;
            Cijena = price;
            Kapacitet = capacity;
            brojač = 0;

        }

        #endregion

        #region Metode

        public int DajTrenutniBrojSlobodnogMjesta()
        {
            brojač++;
            if (brojač == kapacitet)
                throw new InvalidOperationException("Sva mjesta su zauzeta!");
            return brojač;
        }

        /// <summary>
        /// Metoda u kojoj se vrši rezervisanje parking mjesta za klijenta.
        /// Ukoliko su sva mjesta zauzeta ili je klijentu istekla članarina, baca se
        /// izuzetak. U suprotnom, zauzima se prvo slobodno mjesto.
        /// </summary>
        /// <param name="c"></param>
        public void ZauzmiMjesto(Clan c)
        {
            if (brojač != kapacitet)
            {
                if (c.Status == Status.Aktivan)
                {
                    c.RezervišiMjesto(this);
                }
                else
                    throw new RankException("Clanarina je istekla");
            }
            else
                throw new RankException("Nema slobodnih mjesta");
                
        }

        public void OslobodiMjesto()
        {
            brojač--;
        }

        #endregion
    }
}
