using System;
using System.Linq;

namespace eParking
{
    public enum Status
    {
        Aktivna,
        Neaktivna
    }

    public class Clan : Korisnik
    {
        #region Atributi

        string password;
        Status status;
        DateTime aktivnaDo;
        Tuple<int, Lokacija> rezervisanoParkingMjesto;

        #endregion

        #region Properties

        public string Password
        {
            get => password;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length < 10
                    || !value.All(char.IsLetterOrDigit))
                    throw new FormatException("Neispravan format passworda!");

                password = value;
            }
        }

        public Status Status
        {
            get => status;
        }

        public DateTime AktivnaDo
        {
            get => aktivnaDo;
        }

        public Tuple<int, Lokacija> RezervisanoParkingMjesto
        {
            get => rezervisanoParkingMjesto;
        }

        #endregion

        #region Konstruktor

        public Clan(string user, string pass, string address, Vozilo vehicle, DateTime endDate)
            : base(user, address, vehicle)
        {
            Password = pass;
            aktivnaDo = endDate;
            status = Status.Aktivna;
        }

        public Clan(DateTime endDate)
        {
            aktivnaDo = endDate;
            status = Status.Aktivna;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se vrši rezervacija parking mjesta za korisnika.
        /// Ako je osobi istekla članarina, dolazi do pojave izuzetka, kao i u slučaju
        /// da već postoji rezervisano parking mjesto.
        /// </summary>
        /// <param name="l"></param>
        public void RezervišiMjesto(Lokacija l)
        {
            ProvjeriJeLiČlanarinaIstekla();
            if (status == Status.Aktivna)
            {
                if (rezervisanoParkingMjesto == null)
                {
                    rezervisanoParkingMjesto = (l.DajTrenutniBrojSlobodnogMjesta(), l);
                }
                else
                    throw new RankException("Greska, vec ste rezervisali parking mjesto!");
            }
            else
                throw new RankException("Greska, clanarina je istekla");
        }

        public void ProvjeriJeLiČlanarinaIstekla()
        {
            if (DateTime.Now > aktivnaDo)
                status = Status.Neaktivna;
        }

        public void OtkažiRezervaciju()
        {
            rezervisanoParkingMjesto = null;
        }

        #endregion
    }
}
