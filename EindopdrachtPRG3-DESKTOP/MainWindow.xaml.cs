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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Eindopdracht_PRG3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _myTimer = new DispatcherTimer();

        myClass _myClass = new myClass();
        bool message = true;
        public MainWindow()
        {
            InitializeComponent();
            GenerateTitles();
            GenerateTable();

            _myTimer.Start();
            _myTimer.Interval = TimeSpan.FromMilliseconds(200);
            _myTimer.Tick += MyTimer_Tick;
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            GenerateTitles();
            GenerateTable();
        }

        private void GenerateTitles()
        {
            myTable.Children.Clear();

            StackPanel titel = new StackPanel();
            titel.Orientation = Orientation.Horizontal;
            titel.Name = "title";
            myTable.Children.Add(titel);

            TextBlock block1 = new TextBlock();
            block1.Background = Brushes.Gray;
            block1.Foreground = Brushes.Black;
            block1.FontSize = 20;
            block1.FontWeight = FontWeights.Bold;
            block1.Width = 100;
            block1.TextAlignment = TextAlignment.Center;
            block1.Text = "Partij nr.";

            titel.Children.Add(block1);

            TextBlock block2 = new TextBlock();
            block2.Background = Brushes.WhiteSmoke;
            block2.Foreground = Brushes.Black;
            block2.FontSize = 20;
            block2.FontWeight = FontWeights.Bold;
            block2.Width = 200;
            block2.TextAlignment = TextAlignment.Center;
            block2.Text = "Wit";

            titel.Children.Add(block2);

            TextBlock block3 = new TextBlock();
            block3.Background = Brushes.Black;
            block3.Foreground = Brushes.White;
            block3.FontSize = 20;
            block3.FontWeight = FontWeights.Bold;
            block3.Width = 200;
            block3.TextAlignment = TextAlignment.Center;
            block3.Text = "Zwart";

            titel.Children.Add(block3);

            TextBlock block4 = new TextBlock();
            block4.Background = Brushes.Yellow;
            block4.Foreground = Brushes.Purple;
            block4.FontSize = 20;
            block4.FontWeight = FontWeights.Bold;
            block4.Width = 180;
            block4.TextAlignment = TextAlignment.Center;
            block4.Text = "Datum";

            titel.Children.Add(block4);

            TextBlock block5 = new TextBlock();
            block5.Background = Brushes.Red;
            block5.Foreground = Brushes.Yellow;
            block5.FontSize = 20;
            block5.FontWeight = FontWeights.Bold;
            block5.Width = 80;
            block5.TextAlignment = TextAlignment.Center;
            block5.Text = "Tijd";

            titel.Children.Add(block5);

            TextBlock block6 = new TextBlock();
            block6.Background = Brushes.Brown;
            block6.Foreground = Brushes.Yellow;
            block6.FontSize = 20;
            block6.FontWeight = FontWeights.Bold;
            block6.Width = 150;
            block6.TextAlignment = TextAlignment.Center;
            block6.Text = "Tijd per speler";

            titel.Children.Add(block6);

            TextBlock block7 = new TextBlock();
            block7.Background = Brushes.Blue;
            block7.Foreground = Brushes.LightSkyBlue;
            block7.FontSize = 20;
            block7.FontWeight = FontWeights.Bold;
            block7.Width = 90;
            block7.TextAlignment = TextAlignment.Center;
            block7.Text = "Uitslag";

            titel.Children.Add(block7);
        }

        private void GenerateTable()
        {
            DataTable myDataTable = new DataTable();
            myDataTable = _myClass.GetPartijen();

            int counter = 0;

            if (myDataTable != null)
            {
                while (counter < myDataTable.Rows.Count)
                {
                    StackPanel myPanel = new StackPanel();
                    myPanel.Name = "newStackPanel";
                    myPanel.Orientation = Orientation.Horizontal;
                    myTable.Children.Add(myPanel);

                    TextBlock partijNummer = new TextBlock();
                    partijNummer.Text = myDataTable.Rows[counter]["Partijnummer"].ToString();
                    partijNummer.FontSize = 20;
                    partijNummer.Background = Brushes.Beige;
                    partijNummer.FontWeight = FontWeights.Bold;
                    partijNummer.Width = 100;
                    partijNummer.TextAlignment = TextAlignment.Center;
                    partijNummer.Name = "partijNummer";
                    myPanel.Children.Add(partijNummer);

                    TextBlock wit = new TextBlock();
                    wit.Text = myDataTable.Rows[counter]["Wit"].ToString();
                    wit.FontSize = 15;
                    wit.Background = Brushes.Beige;
                    wit.FontWeight = FontWeights.Bold;
                    wit.Width = 200;
                    wit.TextAlignment = TextAlignment.Center;
                    wit.Name = "wit";
                    myPanel.Children.Add(wit);

                    TextBlock zwart = new TextBlock();
                    zwart.Text = myDataTable.Rows[counter]["Zwart"].ToString();
                    zwart.FontSize = 15;
                    zwart.Background = Brushes.Beige;
                    zwart.FontWeight = FontWeights.Bold;
                    zwart.Width = 200;
                    zwart.TextAlignment = TextAlignment.Center;
                    zwart.Name = "zwart";
                    myPanel.Children.Add(zwart);

                    TextBlock datum = new TextBlock();

                    string date = myDataTable.Rows[counter]["Datum"].ToString();
                    string[] split1 = date.Split(' ');
                    string[] dmy = split1[0].Split('-');

                    if (dmy[1] == "1")
                    {
                        datum.Text = dmy[0] + " " + "januari" + " " + dmy[2];
                    }
                    else if (dmy[1] == "2")
                    {
                        datum.Text = dmy[0] + " " + "februari" + " " + dmy[2];
                    }
                    else if (dmy[1] == "3")
                    {
                        datum.Text = dmy[0] + " " + "maart" + " " + dmy[2];
                    }
                    else if (dmy[1] == "4")
                    {
                        datum.Text = dmy[0] + " " + "april" + " " + dmy[2];
                    }
                    else if (dmy[1] == "5")
                    {
                        datum.Text = dmy[0] + " " + "mei" + " " + dmy[2];
                    }
                    else if (dmy[1] == "6")
                    {
                        datum.Text = dmy[0] + " " + "juni" + " " + dmy[2];
                    }
                    else if (dmy[1] == "7")
                    {
                        datum.Text = dmy[0] + " " + "juli" + " " + dmy[2];
                    }
                    else if (dmy[1] == "8")
                    {
                        datum.Text = dmy[0] + " " + "augustus" + " " + dmy[2];
                    }
                    else if (dmy[1] == "9")
                    {
                        datum.Text = dmy[0] + " " + "september" + " " + dmy[2];
                    }
                    else if (dmy[1] == "10")
                    {
                        datum.Text = dmy[0] + " " + "oktober" + " " + dmy[2];
                    }
                    else if (dmy[1] == "11")
                    {
                        datum.Text = dmy[0] + " " + "november" + " " + dmy[2];
                    }
                    else if (dmy[1] == "12")
                    {
                        datum.Text = dmy[0] + " " + "december" + " " + dmy[2];
                    }

                    datum.FontSize = 18;
                    datum.Background = Brushes.Beige;
                    datum.FontWeight = FontWeights.Bold;
                    datum.Width = 180;
                    datum.TextAlignment = TextAlignment.Center;
                    datum.Name = "datum";
                    myPanel.Children.Add(datum);

                    TextBlock tijd = new TextBlock();

                    string time = myDataTable.Rows[counter]["Tijd"].ToString();
                    string[] hms = time.Split(':');
                    tijd.Text = hms[0] + ":" + hms[1];

                    tijd.FontSize = 18;
                    tijd.Background = Brushes.Beige;
                    tijd.FontWeight = FontWeights.Bold;
                    tijd.Width = 80;
                    tijd.TextAlignment = TextAlignment.Center;
                    tijd.Name = "tijd";
                    myPanel.Children.Add(tijd);

                    TextBlock spelerstijd = new TextBlock();
                    spelerstijd.Text = myDataTable.Rows[counter]["Spelerstijd"].ToString() + " minuten";
                    spelerstijd.FontSize = 17;
                    spelerstijd.Background = Brushes.Beige;
                    spelerstijd.FontWeight = FontWeights.Bold;
                    spelerstijd.Width = 150;
                    spelerstijd.TextAlignment = TextAlignment.Center;
                    spelerstijd.Name = "spelerstijd";
                    myPanel.Children.Add(spelerstijd);

                    TextBlock uitslag = new TextBlock();
                    
                    if(myDataTable.Rows[counter]["Uitslag"].ToString() == "0")
                    {
                        uitslag.Text = "1-0";
                    }
                    else if(myDataTable.Rows[counter]["Uitslag"].ToString() == "1")
                    {
                        uitslag.Text = "1/2-1/2";
                    }
                    else
                    {
                        uitslag.Text = "0-1";
                    }
                    uitslag.FontSize = 19;
                    uitslag.Background = Brushes.Beige;
                    uitslag.FontWeight = FontWeights.Bold;
                    uitslag.Width = 90;
                    uitslag.TextAlignment = TextAlignment.Center;
                    uitslag.Name = "uitslag";
                    myPanel.Children.Add(uitslag);

                    Button updateBT = new Button();
                    updateBT.Content = "Update";
                    updateBT.Click += UpdateBT_Click;
                    myPanel.Children.Add(updateBT);

                    Button deleteBT = new Button();
                    deleteBT.Content = "Delete";
                    deleteBT.Click += DeleteBT_Click;
                    myPanel.Children.Add(deleteBT);

                    TextBlock verborgenWit = new TextBlock();
                    verborgenWit.Visibility = Visibility.Hidden;
                    verborgenWit.Text = myDataTable.Rows[counter]["WitNR"].ToString();
                    verborgenWit.FontSize = 17;
                    verborgenWit.Background = Brushes.Beige;
                    verborgenWit.FontWeight = FontWeights.Bold;
                    verborgenWit.Width = 50;
                    verborgenWit.TextAlignment = TextAlignment.Center;
                    verborgenWit.Name = "verborgenWit";
                    myPanel.Children.Add(verborgenWit);

                    TextBlock verborgenZwart = new TextBlock();
                    verborgenZwart.Visibility = Visibility.Hidden;
                    verborgenZwart.Text = myDataTable.Rows[counter]["ZwartNR"].ToString();
                    verborgenZwart.FontSize = 17;
                    verborgenZwart.Background = Brushes.Beige;
                    verborgenZwart.FontWeight = FontWeights.Bold;
                    verborgenZwart.Width = 50;
                    verborgenZwart.TextAlignment = TextAlignment.Center;
                    verborgenZwart.Name = "verborgenZwart";
                    myPanel.Children.Add(verborgenZwart);

                    counter++;
                }
            }
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            _myTimer.Start();
            MessageBoxResult myResult = MessageBox.Show("Zeker weten? De partij is dan weg!", "Partij verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (myResult == MessageBoxResult.No)
            {
                return;
            }

            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;

            int partijNr = 0;
            string oudeuitslag = "";
            int wit = 0;
            int zwart = 0;

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                if (item.Name == "partijNummer")
                {
                    partijNr = Int32.Parse(item.Text);
                }

                if (item.Name == "uitslag")
                {
                    oudeuitslag = item.Text;
                }

                if(item.Name == "verborgenWit")
                {
                    wit = Int32.Parse(item.Text);
                }

                if (item.Name == "verborgenZwart")
                {
                    zwart = Int32.Parse(item.Text);
                }
            }
            DataTable myDataTable = new DataTable();
            myDataTable = _myClass.deletePartij(partijNr, oudeuitslag, wit, zwart);

            _myClass.GetPartijen();
            GenerateTitles();
            GenerateTable();
        }

        private void UpdateBT_Click(object sender, RoutedEventArgs e)
        {
            _myTimer.Start();
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;

            string partijnummer = "";
            string wit = "";
            string zwart = "";
            string datum = "";
            string tijd = "";
            string tijdperspeler = "";
            string uitslag = "";

            foreach (TextBlock item in parent.Children.OfType<TextBlock>())
            {
                if (item.Name == "partijNummer")
                {
                    partijnummer = item.Text;
                }
                else if(item.Name == "wit")
                {
                    wit = item.Text;
                }
                else if (item.Name == "zwart")
                {
                    zwart = item.Text;
                }
                else if (item.Name == "datum")
                {
                    datum = item.Text;
                }
                else if (item.Name == "tijd")
                {
                    tijd = item.Text;
                }
                else if (item.Name == "spelerstijd")
                {
                    tijdperspeler = item.Text;
                }
                else if (item.Name == "uitslag")
                {
                    uitslag = item.Text;
                }
            }
            Update xaml = new Update(partijnummer, wit, zwart, datum, tijd, tijdperspeler, uitslag);
            this.Close();
            xaml.Show();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            _myTimer.Start();
            createNew create = new createNew();
            this.Close();
            create.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)zoekdingen.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }
            string selected = selectedItem.Content.ToString();

            if (selected == "Partijnummer" || selected == "Spelerstijd" || selected == "Datum" || selected == "Tijd")
            {
                kleinergelijkgroter.Visibility = Visibility.Visible;
                if (message == true)
                {
                    MessageBoxResult myResult = MessageBox.Show("Let op! Als u zoekt op een speler, vul dan zijn of haar achternaam in! \n Datums invoeren als JJJJ-MM-DD en tijden als HH:SS.", "Klik op OK om deze melding niet meer te krijgen", MessageBoxButton.OKCancel);
                    if (myResult == MessageBoxResult.OK)
                    {
                        message = false;
                    }
                }
            }
            else
            {
                kleinergelijkgroter.Visibility = Visibility.Hidden;
            }

            if (selected == "Naam")
            {
                beschrijving.Visibility = Visibility.Visible;
                beschrijving.Text = "Typ achternaam speler";
                if (message == true)
                {
                    MessageBoxResult myResult = MessageBox.Show("Let op! Als u zoekt op een speler, vul dan zijn of haar achternaam in! \n Datums invoeren als JJJJ-MM-DD en tijden als HH:SS.", "Klik op OK om deze melding niet meer te krijgen", MessageBoxButton.OKCancel);
                    if (myResult == MessageBoxResult.OK)
                    {
                        message = false;
                    }
                }
            }
            else
            {
                beschrijving.Visibility = Visibility.Hidden;
            }

            if(selected == "Uitslag")
            {
                uitslagSelect.Visibility = Visibility.Visible;
            }
            else
            {
                uitslagSelect.Visibility = Visibility.Hidden;
            }
        }

        private void Zoek_Click(object sender, RoutedEventArgs e)
        {
            _myTimer.Stop();
            ComboBoxItem selectedItem = (ComboBoxItem)zoekdingen.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Selecteer aub waar je naar wilt zoeken!");
                return;
            }
            string selected = selectedItem.Content.ToString();

            string typedWord = "";
            string helpingWord = "";
            string gegevenUitslag = "";

            int counter = 0;
            
            if(selected == "Partijnummer" || selected == "Spelerstijd" || selected == "Datum" || selected == "Tijd" || selected == "Naam")
            {
                ComboBoxItem selectedTref = (ComboBoxItem)kleinergelijkgroter.SelectedItem;
                if (selectedTref == null && selected != "Naam")
                {
                    MessageBox.Show("Selecteer aub waar je op wilt filteren!");
                    return;
                }
                if (selectedTref != null)
                {
                    helpingWord = selectedTref.Content.ToString();
                }
                if (searchWord.Text == "")
                {
                    MessageBox.Show("Vul iets in aub!");
                    return;
                }
                typedWord = searchWord.Text;
                DataTable myDataTable = new DataTable();
                myDataTable = _myClass.searchTextBlock2(selected, typedWord, helpingWord);

                GenerateTitles();
                while (counter < myDataTable.Rows.Count)
                {
                    StackPanel myPanel = new StackPanel();
                    myPanel.Name = "newStackPanel";
                    myPanel.Orientation = Orientation.Horizontal;
                    myTable.Children.Add(myPanel);

                    TextBlock partijNummer = new TextBlock();
                    partijNummer.Text = myDataTable.Rows[counter]["Partijnummer"].ToString();
                    partijNummer.FontSize = 20;
                    partijNummer.Background = Brushes.Beige;
                    partijNummer.FontWeight = FontWeights.Bold;
                    partijNummer.Width = 100;
                    partijNummer.TextAlignment = TextAlignment.Center;
                    partijNummer.Name = "partijNummer";
                    myPanel.Children.Add(partijNummer);

                    TextBlock wit = new TextBlock();
                    wit.Text = myDataTable.Rows[counter]["Wit"].ToString();
                    wit.FontSize = 15;
                    wit.Background = Brushes.Beige;
                    wit.FontWeight = FontWeights.Bold;
                    wit.Width = 200;
                    wit.TextAlignment = TextAlignment.Center;
                    wit.Name = "wit";
                    myPanel.Children.Add(wit);

                    TextBlock zwart = new TextBlock();
                    zwart.Text = myDataTable.Rows[counter]["Zwart"].ToString();
                    zwart.FontSize = 15;
                    zwart.Background = Brushes.Beige;
                    zwart.FontWeight = FontWeights.Bold;
                    zwart.Width = 200;
                    zwart.TextAlignment = TextAlignment.Center;
                    zwart.Name = "zwart";
                    myPanel.Children.Add(zwart);

                    TextBlock datum = new TextBlock();

                    string date = myDataTable.Rows[counter]["Datum"].ToString();
                    string[] split1 = date.Split(' ');
                    string[] dmy = split1[0].Split('-');

                    if (dmy[1] == "1")
                    {
                        datum.Text = dmy[0] + " " + "januari" + " " + dmy[2];
                    }
                    else if (dmy[1] == "2")
                    {
                        datum.Text = dmy[0] + " " + "februari" + " " + dmy[2];
                    }
                    else if (dmy[1] == "3")
                    {
                        datum.Text = dmy[0] + " " + "maart" + " " + dmy[2];
                    }
                    else if (dmy[1] == "4")
                    {
                        datum.Text = dmy[0] + " " + "april" + " " + dmy[2];
                    }
                    else if (dmy[1] == "5")
                    {
                        datum.Text = dmy[0] + " " + "mei" + " " + dmy[2];
                    }
                    else if (dmy[1] == "6")
                    {
                        datum.Text = dmy[0] + " " + "juni" + " " + dmy[2];
                    }
                    else if (dmy[1] == "7")
                    {
                        datum.Text = dmy[0] + " " + "juli" + " " + dmy[2];
                    }
                    else if (dmy[1] == "8")
                    {
                        datum.Text = dmy[0] + " " + "augustus" + " " + dmy[2];
                    }
                    else if (dmy[1] == "9")
                    {
                        datum.Text = dmy[0] + " " + "september" + " " + dmy[2];
                    }
                    else if (dmy[1] == "10")
                    {
                        datum.Text = dmy[0] + " " + "oktober" + " " + dmy[2];
                    }
                    else if (dmy[1] == "11")
                    {
                        datum.Text = dmy[0] + " " + "november" + " " + dmy[2];
                    }
                    else if (dmy[1] == "12")
                    {
                        datum.Text = dmy[0] + " " + "december" + " " + dmy[2];
                    }

                    datum.FontSize = 18;
                    datum.Background = Brushes.Beige;
                    datum.FontWeight = FontWeights.Bold;
                    datum.Width = 180;
                    datum.TextAlignment = TextAlignment.Center;
                    datum.Name = "datum";
                    myPanel.Children.Add(datum);

                    TextBlock tijd = new TextBlock();

                    string time = myDataTable.Rows[counter]["Tijd"].ToString();
                    string[] hms = time.Split(':');
                    tijd.Text = hms[0] + ":" + hms[1];

                    tijd.FontSize = 18;
                    tijd.Background = Brushes.Beige;
                    tijd.FontWeight = FontWeights.Bold;
                    tijd.Width = 80;
                    tijd.TextAlignment = TextAlignment.Center;
                    tijd.Name = "tijd";
                    myPanel.Children.Add(tijd);

                    TextBlock spelerstijd = new TextBlock();
                    spelerstijd.Text = myDataTable.Rows[counter]["Spelerstijd"].ToString() + " minuten";
                    spelerstijd.FontSize = 17;
                    spelerstijd.Background = Brushes.Beige;
                    spelerstijd.FontWeight = FontWeights.Bold;
                    spelerstijd.Width = 150;
                    spelerstijd.TextAlignment = TextAlignment.Center;
                    spelerstijd.Name = "spelerstijd";
                    myPanel.Children.Add(spelerstijd);

                    TextBlock uitslag = new TextBlock();

                    if (myDataTable.Rows[counter]["Uitslag"].ToString() == "0")
                    {
                        uitslag.Text = "1-0";
                    }
                    else if (myDataTable.Rows[counter]["Uitslag"].ToString() == "1")
                    {
                        uitslag.Text = "1/2-1/2";
                    }
                    else
                    {
                        uitslag.Text = "0-1";
                    }
                    uitslag.FontSize = 19;
                    uitslag.Background = Brushes.Beige;
                    uitslag.FontWeight = FontWeights.Bold;
                    uitslag.Width = 90;
                    uitslag.TextAlignment = TextAlignment.Center;
                    uitslag.Name = "uitslag";
                    myPanel.Children.Add(uitslag);

                    Button updateBT = new Button();
                    updateBT.Content = "Update";
                    updateBT.Click += UpdateBT_Click;
                    myPanel.Children.Add(updateBT);

                    Button deleteBT = new Button();
                    deleteBT.Content = "Delete";
                    deleteBT.Click += DeleteBT_Click;
                    myPanel.Children.Add(deleteBT);
                    counter++;
                }
            }
            else
            {
                ComboBoxItem selectedUitslag = (ComboBoxItem)uitslagSelect.SelectedItem;
                if (selectedUitslag == null)
                {
                    MessageBox.Show("Selecteer aub de uitslag!");
                    return;
                }
                gegevenUitslag = selectedUitslag.Content.ToString();
                DataTable myDataTable = new DataTable();
                myDataTable = _myClass.search(gegevenUitslag);

                GenerateTitles();

                while (counter < myDataTable.Rows.Count)
                {
                    StackPanel myPanel = new StackPanel();
                    myPanel.Name = "newStackPanel";
                    myPanel.Orientation = Orientation.Horizontal;
                    myTable.Children.Add(myPanel);

                    TextBlock partijNummer = new TextBlock();
                    partijNummer.Text = myDataTable.Rows[counter]["Partijnummer"].ToString();
                    partijNummer.FontSize = 20;
                    partijNummer.Background = Brushes.Beige;
                    partijNummer.FontWeight = FontWeights.Bold;
                    partijNummer.Width = 100;
                    partijNummer.TextAlignment = TextAlignment.Center;
                    partijNummer.Name = "partijNummer";
                    myPanel.Children.Add(partijNummer);

                    TextBlock wit = new TextBlock();
                    wit.Text = myDataTable.Rows[counter]["Wit"].ToString();
                    wit.FontSize = 15;
                    wit.Background = Brushes.Beige;
                    wit.FontWeight = FontWeights.Bold;
                    wit.Width = 200;
                    wit.TextAlignment = TextAlignment.Center;
                    wit.Name = "wit";
                    myPanel.Children.Add(wit);

                    TextBlock zwart = new TextBlock();
                    zwart.Text = myDataTable.Rows[counter]["Zwart"].ToString();
                    zwart.FontSize = 15;
                    zwart.Background = Brushes.Beige;
                    zwart.FontWeight = FontWeights.Bold;
                    zwart.Width = 200;
                    zwart.TextAlignment = TextAlignment.Center;
                    zwart.Name = "zwart";
                    myPanel.Children.Add(zwart);

                    TextBlock datum = new TextBlock();

                    string date = myDataTable.Rows[counter]["Datum"].ToString();
                    string[] split1 = date.Split(' ');
                    string[] dmy = split1[0].Split('-');

                    if (dmy[1] == "1")
                    {
                        datum.Text = dmy[0] + " " + "januari" + " " + dmy[2];
                    }
                    else if (dmy[1] == "2")
                    {
                        datum.Text = dmy[0] + " " + "februari" + " " + dmy[2];
                    }
                    else if (dmy[1] == "3")
                    {
                        datum.Text = dmy[0] + " " + "maart" + " " + dmy[2];
                    }
                    else if (dmy[1] == "4")
                    {
                        datum.Text = dmy[0] + " " + "april" + " " + dmy[2];
                    }
                    else if (dmy[1] == "5")
                    {
                        datum.Text = dmy[0] + " " + "mei" + " " + dmy[2];
                    }
                    else if (dmy[1] == "6")
                    {
                        datum.Text = dmy[0] + " " + "juni" + " " + dmy[2];
                    }
                    else if (dmy[1] == "7")
                    {
                        datum.Text = dmy[0] + " " + "juli" + " " + dmy[2];
                    }
                    else if (dmy[1] == "8")
                    {
                        datum.Text = dmy[0] + " " + "augustus" + " " + dmy[2];
                    }
                    else if (dmy[1] == "9")
                    {
                        datum.Text = dmy[0] + " " + "september" + " " + dmy[2];
                    }
                    else if (dmy[1] == "10")
                    {
                        datum.Text = dmy[0] + " " + "oktober" + " " + dmy[2];
                    }
                    else if (dmy[1] == "11")
                    {
                        datum.Text = dmy[0] + " " + "november" + " " + dmy[2];
                    }
                    else if (dmy[1] == "12")
                    {
                        datum.Text = dmy[0] + " " + "december" + " " + dmy[2];
                    }

                    datum.FontSize = 18;
                    datum.Background = Brushes.Beige;
                    datum.FontWeight = FontWeights.Bold;
                    datum.Width = 180;
                    datum.TextAlignment = TextAlignment.Center;
                    datum.Name = "datum";
                    myPanel.Children.Add(datum);

                    TextBlock tijd = new TextBlock();

                    string time = myDataTable.Rows[counter]["Tijd"].ToString();
                    string[] hms = time.Split(':');
                    tijd.Text = hms[0] + ":" + hms[1];

                    tijd.FontSize = 18;
                    tijd.Background = Brushes.Beige;
                    tijd.FontWeight = FontWeights.Bold;
                    tijd.Width = 80;
                    tijd.TextAlignment = TextAlignment.Center;
                    tijd.Name = "tijd";
                    myPanel.Children.Add(tijd);

                    TextBlock spelerstijd = new TextBlock();
                    spelerstijd.Text = myDataTable.Rows[counter]["Spelerstijd"].ToString() + " minuten";
                    spelerstijd.FontSize = 17;
                    spelerstijd.Background = Brushes.Beige;
                    spelerstijd.FontWeight = FontWeights.Bold;
                    spelerstijd.Width = 150;
                    spelerstijd.TextAlignment = TextAlignment.Center;
                    spelerstijd.Name = "spelerstijd";
                    myPanel.Children.Add(spelerstijd);

                    TextBlock uitslag = new TextBlock();

                    if (myDataTable.Rows[counter]["Uitslag"].ToString() == "0")
                    {
                        uitslag.Text = "1-0";
                    }
                    else if (myDataTable.Rows[counter]["Uitslag"].ToString() == "1")
                    {
                        uitslag.Text = "1/2-1/2";
                    }
                    else
                    {
                        uitslag.Text = "0-1";
                    }
                    uitslag.FontSize = 19;
                    uitslag.Background = Brushes.Beige;
                    uitslag.FontWeight = FontWeights.Bold;
                    uitslag.Width = 90;
                    uitslag.TextAlignment = TextAlignment.Center;
                    uitslag.Name = "uitslag";
                    myPanel.Children.Add(uitslag);

                    Button updateBT = new Button();
                    updateBT.Content = "Update";
                    updateBT.Click += UpdateBT_Click;
                    myPanel.Children.Add(updateBT);

                    Button deleteBT = new Button();
                    deleteBT.Content = "Delete";
                    deleteBT.Click += DeleteBT_Click;
                    myPanel.Children.Add(deleteBT);
                    counter++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _myTimer.Start();
            GenerateTitles();
            GenerateTable();
        }
    }
}
