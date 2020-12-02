using System;
using System.Collections.Generic;
using System.Text;

namespace eParking
{
    public class Parking
    {
        #region Atributi

        List<Korisnik> korisnici;
        List<Lokacija> lokacije;

        #endregion

        #region Properties

        public List<Korisnik> Korisnici
        {
            get => korisnici;
        }

        public List<Lokacija> Lokacije
        {
            get => lokacije;
        }

        #endregion

        #region Konstruktor

        public Parking()
        {
            korisnici = new List<Korisnik>();
            lokacije = new List<Lokacija>();
        }

        #endregion

        #region Metode

        public void RadSaLokacijom(Lokacija l, int opcija, List<string> podaci = null)
        {
            if (opcija == 0)
            {
                if (Lokacije.Find(lokacija => lokacija.Naziv == l.Naziv) != null)
                    throw new InvalidOperationException("Lokacija već postoji!");

                Lokacije.Add(l);
            }
            else if (opcija == 1)
            {
                Lokacija lokacija = Lokacije.Find(loc => loc.Naziv == l.Naziv);
                if (lokacija == null)
                    throw new InvalidOperationException("Lokacija ne postoji!");

                Lokacije.Remove(lokacija);
            }
            else if (opcija == 2)
            {
                Lokacija lokacija = Lokacije.Find(loc => loc.Naziv == l.Naziv);
                if (lokacija == null)
                    throw new InvalidOperationException("Lokacija ne postoji!");

                foreach (string ulica in podaci)
                    if (!lokacija.Ulice.Contains(ulica))
                        lokacija.Ulice.Add(ulica);
            }
        }

        public void DodajKorisnika(Korisnik k, bool clan)
        {
            Korisnik korisnik = Korisnici.Find(kor => kor.Username == k.Username);
            if (korisnik != null && !clan)
                throw new ArgumentException("Korisnik već postoji!");
            else if (korisnik != null)
            {
                Korisnici.Remove(korisnik);
                Korisnici.Add(k);
            }
            else
                Korisnici.Add(k);
        }

        /// <summary>
        /// Metoda u kojoj se vrši rezervisanje parking mjesta za željenu lokaciju korisnika.
        /// Ukoliko korisnik nije član, ne smije mu se omogućiti rezervacija, kao ni u
        /// slučaju da na lokaciji nema slobodnih parking mjesta ili da je korisnik
        /// već rezervisao neko drugo parking mjesto. U suprotnom,
        /// vrši se rezervacija parking mjesta za korisnika.
        /// </summary>
        /// <param name="k"></param>
        /// <param name="l"></param>
        public void RezervišiParking(Korisnik k, Lokacija l)
        {
            if (Object.ReferenceEquals(k.GetType(), Clan.GetType()))
            {
                if (l.brojac != l.kapacitet)
                {
                    k.RezervišiMjesto(l);
                }
                else
                    throw new RankException("Sva mjesta su zauzeta");
            }
            else
                throw new RankException("Korisnik nije clan");
        }

        public double IzračunajTrenutnuZaradu()
        {
            throw new NotImplementedException();
        }

        public void OslobodiParkingMjesto(ITransakcija transakcija, Clan c)
        {
            if (transakcija.DajVrijemeDolaska(c.Vozilo).AddHours(24) < DateTime.Now)
            {
                c.RezervisanoParkingMjesto.Item2.OslobodiMjesto();
                c.OtkažiRezervaciju();
            }
            else
                throw new InvalidOperationException("Još uvijek nisu prošla 24 sata!");
        }

        #endregion
    }
}
