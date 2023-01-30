using Eindopdracht_PRG3.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eindopdracht_PRG3
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        MainWindow myWindow = new MainWindow();
        myClass _myClass = new myClass();
        string _oudeuitslag = "";
        string _oudespelerwit = "";
        string _oudespelerzwart = "";

        public Update(string nr, string wit, string zwart, string datum, string tijd, string tijdperspeler, string uitslag)
        {
            InitializeComponent();

            titleUpdate.Text = "Update Partij " + nr;

            DataTable myDatatable = new DataTable();
            myDatatable = _myClass.getPlayers();

            int counter = 0;

            while(counter < myDatatable.Rows.Count)
            {
                string id = myDatatable.Rows[counter]["Deelneemnummer"].ToString();
                string voornaam = myDatatable.Rows[counter]["Voornaam"].ToString();
                string tussenvoegsel = myDatatable.Rows[counter]["Tussenvoegsel"].ToString();
                string achternaam = myDatatable.Rows[counter]["Achternaam"].ToString();

                ComboBoxItem item = new ComboBoxItem();
                ComboBoxItem item2 = new ComboBoxItem();
                item.Content = id + ". " + voornaam + " " + tussenvoegsel + " " + achternaam;
                item2.Content = id + ". " + voornaam + " " + tussenvoegsel + " " + achternaam;

                Wittespeler.Items.Add(item);
                Zwartespeler.Items.Add(item2);

                if ((voornaam + " " + tussenvoegsel + " " + achternaam) == wit)
                {
                    Wittespeler.SelectedItem = item;
                    _oudespelerwit = id;
                }

                if ((voornaam + " " + tussenvoegsel + " " + achternaam) == zwart)
                {
                    Zwartespeler.SelectedItem = item2;
                    _oudespelerzwart = id;
                }
                counter++;
            }

            string[] dmy = datum.Split(' ');
            dag.Text = dmy[0];

            ComboBoxItem maand1 = new ComboBoxItem();
            maand1.Content = "januari";
            maanden.Items.Add(maand1);

            ComboBoxItem maand2 = new ComboBoxItem();
            maand2.Content = "februari";
            maanden.Items.Add(maand2);

            ComboBoxItem maand3 = new ComboBoxItem();
            maand3.Content = "maart";
            maanden.Items.Add(maand3);

            ComboBoxItem maand4 = new ComboBoxItem();
            maand4.Content = "april";
            maanden.Items.Add(maand4);

            ComboBoxItem maand5 = new ComboBoxItem();
            maand5.Content = "mei";
            maanden.Items.Add(maand5);

            ComboBoxItem maand6 = new ComboBoxItem();
            maand6.Content = "juni";
            maanden.Items.Add(maand6);

            ComboBoxItem maand7 = new ComboBoxItem();
            maand7.Content = "juli";
            maanden.Items.Add(maand7);

            ComboBoxItem maand8 = new ComboBoxItem();
            maand8.Content = "augustus";
            maanden.Items.Add(maand8);

            ComboBoxItem maand9 = new ComboBoxItem();
            maand9.Content = "september";
            maanden.Items.Add(maand9);

            ComboBoxItem maand10 = new ComboBoxItem();
            maand10.Content = "oktober";
            maanden.Items.Add(maand10);

            ComboBoxItem maand11 = new ComboBoxItem();
            maand11.Content = "november";
            maanden.Items.Add(maand11);

            ComboBoxItem maand12 = new ComboBoxItem();
            maand12.Content = "december";
            maanden.Items.Add(maand12);

            if(dmy[1] == "januari")
            {
                maanden.SelectedItem = maand1;
            }
            else if(dmy[1] == "februari")
            {
                maanden.SelectedItem = maand2;
            }
            else if (dmy[1] == "maart")
            {
                maanden.SelectedItem = maand3;
            }
            else if (dmy[1] == "april")
            {
                maanden.SelectedItem = maand4;
            }
            else if (dmy[1] == "mei")
            {
                maanden.SelectedItem = maand5;
            }
            else if (dmy[1] == "juni")
            {
                maanden.SelectedItem = maand6;
            }
            else if (dmy[1] == "juli")
            {
                maanden.SelectedItem = maand7;
            }
            else if (dmy[1] == "augustus")
            {
                maanden.SelectedItem = maand8;
            }
            else if (dmy[1] == "september")
            {
                maanden.SelectedItem = maand9;
            }
            else if (dmy[1] == "oktober")
            {
                maanden.SelectedItem = maand10;
            }
            else if (dmy[1] == "november")
            {
                maanden.SelectedItem = maand11;
            }
            else if (dmy[1] == "december")
            {
                maanden.SelectedItem = maand12;
            }

            jaar.Text = dmy[2];

            string[] hm = tijd.Split(':');
            hours.Text = hm[0];
            minutes.Text = hm[1];

            string[] split = tijdperspeler.Split(' ');
            Spelertijd.Text = split[0];

            ComboBoxItem winstwit = new ComboBoxItem();
            winstwit.Content = "Winst wit";
            uitslagen.Items.Add(winstwit);

            ComboBoxItem remise = new ComboBoxItem();
            remise.Content = "Remise";
            uitslagen.Items.Add(remise);

            ComboBoxItem winstzwart = new ComboBoxItem();
            winstzwart.Content = "Winst zwart";
            uitslagen.Items.Add(winstzwart);

            if (uitslag == "1-0")
            {
                uitslagen.SelectedItem = winstwit;
                _oudeuitslag = "0";
            }
            else if(uitslag == "1/2-1/2")
            {
                uitslagen.SelectedItem = remise;
                _oudeuitslag = "1";
            }
            else
            {
                uitslagen.SelectedItem = winstzwart;
                _oudeuitslag = "2";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] splittitle = titleUpdate.Text.Split(' ');
            int partijNummer = Int32.Parse(splittitle[2]);

            ComboBoxItem SelectedWhitePlayer = null;
            ComboBoxItem SelectedBlackPlayer = null;
            if (Wittespeler != null)
            {
                SelectedWhitePlayer = (ComboBoxItem)Wittespeler.SelectedItem;
            }
            if (Zwartespeler != null)
            {
                SelectedBlackPlayer = (ComboBoxItem)Zwartespeler.SelectedItem;
            }
            
            if (SelectedWhitePlayer == null)
            {
                MessageBox.Show("Je hebt geen witte speler geselecteerd!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedBlackPlayer == null)
            {
                MessageBox.Show("Je hebt geen zwarte speler geselecteerd!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string whitePlayer = SelectedWhitePlayer.Content.ToString();
            string blackPlayer = SelectedBlackPlayer.Content.ToString();

            string[] splitWit = whitePlayer.Split('.');
            string[] splitZwart = blackPlayer.Split('.');

            int idWit = Int32.Parse(splitWit[0]);
            int idZwart = Int32.Parse(splitZwart[0]);

            if (whitePlayer == blackPlayer)
            {
                MessageBox.Show("Je kunt niet twee dezelfde spelers hebben!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ComboBoxItem SelectedMonth = (ComboBoxItem)maanden.SelectedItem;
            if (SelectedMonth == null)
            {
                MessageBox.Show("Je hebt geen maand geselecteerd!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string month = SelectedMonth.Content.ToString();
            int day = 0;
            int year = 0;
            
            try
            {
                day = Int32.Parse(dag.Text);
                year = Int32.Parse(jaar.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Vul aub een getal in bij dag en maand!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(day > 31 || day < 1)
            {
                MessageBox.Show("Vul bij dagen aub een getal in tussen de 1 en de 31!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(year < 1000 || year > 9999)
            {
                MessageBox.Show("Je moet 4 getallen invullen bij het jaar!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(month == "januari")
            {
                month = "01";
            }
            else if(month == "februari")
            {
                month = "02";
            }
            else if (month == "maart")
            {
                month = "03";
            }
            else if (month == "april")
            {
                month = "04";
            }
            else if (month == "mei")
            {
                month = "05";
            }
            else if (month == "juni")
            {
                month = "06";
            }
            else if (month == "juli")
            {
                month = "07";
            }
            else if (month == "augustus")
            {
                month = "08";
            }
            else if (month == "september")
            {
                month = "09";
            }
            else if (month == "oktober")
            {
                month = "10";
            }
            else if (month == "november")
            {
                month = "11";
            }
            else if (month == "december")
            {
                month = "12";
            }

            if(day == 31 && (month == "04" || month == "06" || month == "09" || month == "11"))
            {
                MessageBox.Show("De geselecteerde maand heeft maar 30 dagen!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(day > 29 && month == "02")
            {
                MessageBox.Show("Februari heeft maar 28 dagen en in schrikkeljaren 29!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int originalyear = year;

            myClass checkDateOfBirth = new myClass();
            DataTable myDataTable1 = new DataTable();
            myDataTable1 = checkDateOfBirth.HaalGeboorteDatumVanWitOp(idWit);

            string dateOfBirth = myDataTable1.Rows[0]["Geboortedatum"].ToString();
            string[] split = dateOfBirth.Split('-');
            string[] splitdaytime = split[2].Split(' ');

            int year1 = Int32.Parse(splitdaytime[0]);
            int month1 = Int32.Parse(split[1]);
            int day1 = Int32.Parse(split[0]);

            if (year - 7 < year1)
            {
                MessageBox.Show("De witte speler is te jong. De deelnemer moet minimaal 7 jaar zijn op de dag van de gespeelde partij");
                return;
            }
            else if (year - 7 == year1)
            {
                int counter = 0;
                while (maanden.SelectedIndex > counter)
                {
                    counter++;
                }
                counter++;

                if (month1 > counter)
                {
                    MessageBox.Show("De witte speler is te jong. De deelnemer moet minimaal 7 jaar zijn op de dag van de gespeelde partij");
                    return;
                }
                else
                {
                    if (day < day1)
                    {
                        MessageBox.Show("De witte speler is te jong. De deelnemer moet minimaal 7 jaar zijn op de dag van de gespeelde partij");
                        return;
                    }
                }
            }

            myClass checkDateOfBirth2 = new myClass();
            DataTable myDataTable2 = new DataTable();
            myDataTable2 = checkDateOfBirth.HaalGeboorteDatumVanZwartOp(idZwart);

            string dateOfBirth2 = myDataTable2.Rows[0]["Geboortedatum"].ToString();
            string[] split2 = dateOfBirth2.Split('-');
            string[] splitdaytime2 = split2[2].Split(' ');

            int year2 = Int32.Parse(splitdaytime2[0]);
            int month2 = Int32.Parse(split2[1]);
            int day2 = Int32.Parse(split2[0]);

            if (year - 7 < year2)
            {
                MessageBox.Show("De zwarte speler is te jong. De deelnemer moet minimaal 7 jaar zijn op de dag van de gespeelde partij");
                return;
            }
            else if (year - 7 == year2)
            {
                int counter = 0;
                while (maanden.SelectedIndex > counter)
                {
                    counter++;
                }
                counter++;

                if (month2 > counter)
                {
                    MessageBox.Show("De zwarte speler is te jong. De deelnemer moet minimaal 7 jaar zijn op de dag van de gespeelde partij");
                    return;
                }
                else
                {
                    if (day < day2)
                    {
                        MessageBox.Show("De zwarte speler is te jong. De deelnemer moet minimaal 7 jaar zijn op de dag van de gespeelde partij");
                        return;
                    }
                }
            }

            if (day == 29 && month == "02")
            {
                while(year >= 4)
                {
                    year -= 4;
                }
                if(year != 0)
                {
                    MessageBox.Show(originalyear + " is geen schrikkeljaar. Februari heeft maar 28 dagen!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            string sqldate = year.ToString() + "-" + month + "-" + day.ToString();

            int hour = 0;
            int minute = 0;

            try
            {
                hour = Int32.Parse(hours.Text);
                minute = Int32.Parse(minutes.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Vul aub geldige getallen in bij de tijd!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(hour > 23 || hour < 0 || minute < 0 || minute > 59)
            {
                MessageBox.Show("Vul bij minuten een getal in tussen 0 en 59, en bij uren een getal tussen 0 en 23!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string time = "";

            if (hour < 10 && minute < 10)
            {
                time = "0" + hour.ToString() + ":0" + minute.ToString();
            }
            else if(hour < 10)
            {
                time = "0" + hour.ToString() + ":" + minute.ToString();
            }
            else if(minute < 10)
            {
                time =  hour.ToString() + ":0" + minute.ToString();
            }
            else
            {
                time = hour.ToString() + ":" + minute.ToString();
            }

            int speeltijd = 0;

            try
            {
                speeltijd = Int32.Parse(Spelertijd.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Vul aub een geldig getal in bij de spelerstijd", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ComboBoxItem Result = (ComboBoxItem)uitslagen.SelectedItem;
            if (Result == null)
            {
                MessageBox.Show("Er is geen uitslag geselecteerd!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string uitslag = Result.Content.ToString();

            myClass update = new myClass();
            DataTable myDataTable = new DataTable();
            myDataTable = update.UpdatePlayer(partijNummer, idWit, _oudespelerwit, idZwart, _oudespelerzwart, sqldate, time, speeltijd, uitslag, _oudeuitslag);

            this.Close();
            myWindow.ShowDialog();
        }
    }
}
