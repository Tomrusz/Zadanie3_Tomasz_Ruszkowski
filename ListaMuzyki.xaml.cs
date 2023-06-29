using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Zadanie3_Tomasz_Ruszkowski
{
    /// <summary>
    /// Logika interakcji dla klasy ListaOsób.xaml
    /// </summary>
    public partial class ListaMuzyki : Window
    {
        private const string path = "Muzyka.xml";
        public ObservableCollection<Muzyka> MuzykaZbiór { get; set; } = new ObservableCollection<Muzyka>();

        ListBox lista;

        public ListaMuzyki()
        {
            DataContext = this;
            InitializeComponent();
            lista = (ListBox)this.FindName("Lista");
        }

        private void Edytuj(object sender, RoutedEventArgs e)
        {
            new WidokMuzyki(
                (Muzyka)lista.SelectedItem
                )
                .Show();
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Muzyka nowa = new Muzyka();
            MuzykaZbiór.Add(nowa);
            new WidokMuzyki(nowa)
                .Show();
        }

        private void Usuń(object sender, RoutedEventArgs e)
        {
            MuzykaZbiór.Remove(
                (Muzyka)lista.SelectedItem
                );
        }

        private void Importuj(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Muzyka>));
            FileStream strumieńOdczytu = new FileStream(path, FileMode.Open);
            ObservableCollection<Muzyka> MuzykaZbiór = (ObservableCollection<Muzyka>)serializer.Deserialize(strumieńOdczytu);
            strumieńOdczytu.Close();
            foreach (Muzyka muzyka in MuzykaZbiór)
            {
                MuzykaZbiór.Add(muzyka);
            }
        }

        private void Eksportuj(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Muzyka>));
            TextWriter strumieńZapisu = new StreamWriter(path);
            serializer.Serialize(strumieńZapisu, MuzykaZbiór);
            strumieńZapisu.Close();
        }
    }
}
