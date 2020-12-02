using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace eParking
{
    public class Korisnik
    {
        #region Atributi

        int brojLogovanja = 0;
        string username, adresa;
        Vozilo vozilo;

        #endregion

        #region Properties

        public int BrojLogovanja
        {
            get => brojLogovanja;
        }

        public string Username
        {
            get => username;
            set
            {
                if (String.IsNullOrWhiteSpace(value)
                    || value.Length < 5 || value.Length > 10
                    || !value.All(char.IsLetterOrDigit))
                    throw new FormatException("Neispravno korisničko ime!");

                username = value;
            }
        }

        public string Adresa
        {
            get => adresa;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new FormatException("Neispravna adresa!");

                adresa = value;
            }
        }

        public Vozilo Vozilo
        {
            get => vozilo;
            set => vozilo = value;
        }

        #endregion

        #region Konstruktor

        public Korisnik(string user, string address, Vozilo vehicle)
        {
            Username = user;
            Adresa = address;
            Vozilo = vehicle;
        }

        public Korisnik()
        {

        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se korisnik pokušava ulogovati na sistem.
        /// Ukoliko se ulogovao više od 99 puta, ne dozvoljava mu se logovanje na sistem.
        /// U suprotnom, povećava se broj logovanja i dozvoljava se login.
        /// </summary>
        /// <returns></returns>
        public bool UlogujSe()
        {
            if (brojLogovanja <= 99) {
                brojLogovanja++;
                return true;
            }
            else
                return false;
        }

        #endregion
    }
}
