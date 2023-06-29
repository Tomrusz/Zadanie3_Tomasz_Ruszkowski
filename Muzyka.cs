using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3_Tomasz_Ruszkowski
{
    public class Muzyka : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private static Dictionary<string, ICollection<string>> powiązaneWłaściwości = new Dictionary<string, ICollection<string>>()
        {
            ["Tytuł"] = new string[] { "Tytuł" },
            ["Autor"] = new string[] { "Autor" },
            ["Wydawca"] = new string[] { "Wydawca" },
            ["DataWydania"] = new string[] { "Wiek" },
            ["Nośnik"] = new string[] { "Nośnik" },
            ["Wiek"] = new string[] { "SkrótSzczegółów" }
        };
        void NotyfikujZmianę(
            [CallerMemberName] string? nazwaWłaściwości = null,
            HashSet<string> jużZałatwione = null
            )
        {
            if (jużZałatwione == null)
                jużZałatwione = new();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWłaściwości));
            jużZałatwione.Add(nazwaWłaściwości);
            if (powiązaneWłaściwości.ContainsKey(nazwaWłaściwości))
                foreach (string powiązanaWłaściwość in powiązaneWłaściwości[nazwaWłaściwości])
                    if (jużZałatwione.Contains(powiązanaWłaściwość) == false)
                        NotyfikujZmianę(powiązanaWłaściwość, jużZałatwione);
                    }
                
            
        

        private string tytuł;
        private string autor;
        private string wydawca;
        private DateTime? dataWydania = null;
        public enum nośnik { Kaseta, Cd, Pendrive }
        private nośnik nosnik;

        public string Tytuł
        {
            get => tytuł;
            set
            {
                tytuł = value;
                NotyfikujZmianę();
            }
        }
        public string Autor
        {
            get => autor;
            set
            {
                autor = value;
                NotyfikujZmianę();
            }
        }
        public string Wydawca
        {
            get => wydawca;
            set
            {
                wydawca = value;
                NotyfikujZmianę();
            }
        }
        public DateTime? DataWydania
        {
            get => dataWydania;
            set
            {
                dataWydania = value;
                NotyfikujZmianę();
            }
        }
        public string SkrótSzczegółów
        {
            get
            {
                if (DataWydania == null)
                    return Tytuł;
                else
                    return $"{Tytuł}, {Wiek} lat";
            }
        }
        public ushort? Wiek
        {
            get
            {
                if (dataWydania == null)
                    return null;
                DateTime koniec = DateTime.Now;
                TimeSpan różnica = (TimeSpan)(koniec - dataWydania);
                return (ushort)Math.Floor(różnica.Days / 365.25);
            }
        }
    }
}
