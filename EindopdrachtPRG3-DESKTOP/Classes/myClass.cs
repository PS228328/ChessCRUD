using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Eindopdracht_PRG3.Classes
{
    class myClass
    {
        MySqlConnection _myconnection = new MySqlConnection("Server=localhost;Database=schaken;Uid=root;Pwd=;");
        
        public DataTable HaalGeboorteDatumVanWitOp(int id)
        {
            _myconnection.Open();

            DataTable DateOfBirthWhite = new DataTable();
            MySqlCommand command = _myconnection.CreateCommand();
            command.CommandText = "SELECT Geboortedatum FROM deelnemers WHERE Deelneemnummer = @id";
            command.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = command.ExecuteReader();
            DateOfBirthWhite.Load(reader);

            _myconnection.Close();

            return DateOfBirthWhite;
        }

        public DataTable HaalGeboorteDatumVanZwartOp(int id)
        {
            _myconnection.Open();

            DataTable DateOfBirthBlack = new DataTable();
            MySqlCommand command = _myconnection.CreateCommand();
            command.CommandText = "SELECT Geboortedatum FROM deelnemers WHERE Deelneemnummer = @id";
            command.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = command.ExecuteReader();
            DateOfBirthBlack.Load(reader);

            _myconnection.Close();

            return DateOfBirthBlack;
        }

        public DataTable GetPartijen()
        {
            //Haal alle partijen op
            DataTable partijenTable = new DataTable();

            try
            {
                _myconnection.Open();

                MySqlCommand command = _myconnection.CreateCommand();
                command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart ORDER BY Partijnummer";
                MySqlDataReader reader = command.ExecuteReader();
                partijenTable.Load(reader);
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                _myconnection.Close();
            }

            return partijenTable;
        }

        public DataTable getPlayers()
        {
            //Haal alle spelers op
            DataTable deelnemers = new DataTable();

            try
            {
                _myconnection.Open();

                MySqlCommand command = _myconnection.CreateCommand();
                command.CommandText = "SELECT Deelneemnummer, Voornaam, Tussenvoegsel, Achternaam FROM deelnemers";
                MySqlDataReader reader = command.ExecuteReader();
                deelnemers.Load(reader);
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                _myconnection.Close();
            }
            return deelnemers;
        }

        public DataTable UpdatePlayer(int partijnummer, int wit, string witOud, int zwart, string zwartOud, string datum, string tijd, int speeltijd, string uitslag, string oudeuitslag)
        {
            //Update een speler
            DataTable updatedTable = new DataTable();
            int oldWhite = Convert.ToInt32(witOud);
            int oldBlack = Convert.ToInt32(zwartOud);

            try
            {
                bool checkallupdated = false;

                if (uitslag == "Winst wit")
                {
                    uitslag = "0";
                }
                else if (uitslag == "Remise")
                {
                    uitslag = "1";
                }
                else
                {
                    uitslag = "2";
                }
                _myconnection.Open();

                MySqlCommand command = _myconnection.CreateCommand();
                command.CommandText = "UPDATE `partijen` SET `Wit` = @wit, `Zwart` = @zwart, `Datum` = @datum, `Tijd` = @tijd, `Spelerstijd` = @spelerstijd, `Uitslag` = @uitslag WHERE `Partijnummer` = @id";
                command.Parameters.AddWithValue("@wit", wit);
                command.Parameters.AddWithValue("@zwart", zwart);
                command.Parameters.AddWithValue("@datum", datum);
                command.Parameters.AddWithValue("@tijd", tijd);
                command.Parameters.AddWithValue("@spelerstijd", speeltijd);
                command.Parameters.AddWithValue("@uitslag", uitslag);
                command.Parameters.AddWithValue("@id", partijnummer);
                command.ExecuteNonQuery();

                if(oudeuitslag != uitslag && (wit != oldWhite || zwart != oldBlack))
                {
                    MySqlCommand command2 = _myconnection.CreateCommand();
                    MySqlCommand command3 = _myconnection.CreateCommand();

                    if (oudeuitslag == "0")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @witoud";
                        command3.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @zwartoud";
                    }
                    else if(oudeuitslag == "1")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @witoud";
                        command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @zwartoud";
                    }
                    else
                    {
                        command2.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @witoud";
                        command3.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @zwartoud";
                    }

                    command2.Parameters.AddWithValue("@witoud", oldWhite);
                    command3.Parameters.AddWithValue("@zwartoud", oldBlack);

                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();

                    MySqlCommand command4 = _myconnection.CreateCommand();
                    MySqlCommand command5 = _myconnection.CreateCommand();

                    if (uitslag == "0")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @wit";
                        command5.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (uitslag == "1")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @wit";
                        command5.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @zwart";
                    }
                    else
                    {
                        command4.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @wit";
                        command5.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @zwart";
                    }

                    command4.Parameters.AddWithValue("@wit", wit);
                    command5.Parameters.AddWithValue("@zwart", zwart);

                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();

                    checkallupdated = true;
                }

                if (oudeuitslag != uitslag && checkallupdated == false)
                {
                    MySqlCommand command2 = _myconnection.CreateCommand();
                    MySqlCommand command3 = _myconnection.CreateCommand();
                    if (oudeuitslag == "0" && uitslag == "1")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @wit";
                        command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @wit";
                    }
                    else if (oudeuitslag == "0" && uitslag == "2")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @wit";
                        command3.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @wit";
                    }
                    else if (oudeuitslag == "1" && uitslag == "0")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @wit";
                        command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @wit";
                    }
                    else if (oudeuitslag == "1" && uitslag == "2")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @wit";
                        command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @wit";
                    }
                    else if (oudeuitslag == "2" && uitslag == "0")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @wit";
                        command3.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @wit";
                    }
                    else if (oudeuitslag == "2" && uitslag == "1")
                    {
                        command2.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @wit";
                        command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @wit";
                    }
                    command2.Parameters.AddWithValue("@wit", wit);
                    command3.Parameters.AddWithValue("@wit", wit);

                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();

                    MySqlCommand command4 = _myconnection.CreateCommand();
                    MySqlCommand command5 = _myconnection.CreateCommand();
                    if (oudeuitslag == "0" && uitslag == "1")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @zwart";
                        command5.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (oudeuitslag == "0" && uitslag == "2")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @zwart";
                        command5.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (oudeuitslag == "1" && uitslag == "0")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @zwart";
                        command5.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (oudeuitslag == "1" && uitslag == "2")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @zwart";
                        command5.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (oudeuitslag == "2" && uitslag == "0")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @zwart";
                        command5.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (oudeuitslag == "2" && uitslag == "1")
                    {
                        command4.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @zwart";
                        command5.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @zwart";
                    }
                    command4.Parameters.AddWithValue("@zwart", zwart);
                    command5.Parameters.AddWithValue("@zwart", zwart);

                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();
                }


                if (wit != oldWhite && checkallupdated == false)
                {
                    MySqlCommand command6 = _myconnection.CreateCommand();
                    MySqlCommand command7 = _myconnection.CreateCommand();

                    if (uitslag == "0")
                    {
                        command6.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @oudwit";
                        command7.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @wit";
                    }
                    else if (uitslag == "1")
                    {
                        command6.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @oudwit";
                        command7.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @wit";
                    }
                    else
                    {
                        command6.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @oudwit";
                        command7.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @wit";
                    }
                    command6.Parameters.AddWithValue("@wit", wit);
                    command6.Parameters.AddWithValue("@oudwit", oldWhite);

                    command7.Parameters.AddWithValue("@wit", wit);
                    command7.Parameters.AddWithValue("@oudwit", oldWhite);

                    command6.ExecuteNonQuery();
                    command7.ExecuteNonQuery();
                }
                
                if (zwart != oldBlack && checkallupdated == false)
                {
                    MySqlCommand command6 = _myconnection.CreateCommand();
                    MySqlCommand command7 = _myconnection.CreateCommand();

                    if (uitslag == "2")
                    {
                        command6.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @oudzwart";
                        command7.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @zwart";
                    }
                    else if (uitslag == "1")
                    {
                        command6.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @oudzwart";
                        command7.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @zwart";
                    }
                    else
                    {
                        command6.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @oudzwart";
                        command7.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @zwart";
                    }
                    command6.Parameters.AddWithValue("@zwart", zwart);
                    command6.Parameters.AddWithValue("@oudzwart", oldBlack);

                    command7.Parameters.AddWithValue("@zwart", zwart);
                    command7.Parameters.AddWithValue("@oudzwart", oldBlack);

                    command6.ExecuteNonQuery();
                    command7.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                //Any problems?
            }
            finally
            {
                _myconnection.Close();
            }

            return updatedTable;
        }

        public DataTable CreatePartij(int white, int black, string date, string time, int playtime, string result)
        {
            DataTable ELO = new DataTable();
            try
            {
                _myconnection.Open();

                MySqlCommand command = _myconnection.CreateCommand();
                command.CommandText = "INSERT INTO partijen (Wit, Zwart, Datum, Tijd, Spelerstijd, Uitslag) VALUES (@wit, @zwart, @datum, @tijd, @spelerstijd, @uitslag)";
                command.Parameters.AddWithValue("@wit", white);
                command.Parameters.AddWithValue("@zwart", black);
                command.Parameters.AddWithValue("@datum", date);
                command.Parameters.AddWithValue("@tijd", time);
                command.Parameters.AddWithValue("@spelerstijd", playtime);
                command.Parameters.AddWithValue("@uitslag", result);
                command.ExecuteNonQuery();

                MySqlCommand command2 = _myconnection.CreateCommand();

                if (result == "0")
                {
                    command2.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @wit";
                }
                else if (result == "1")
                {
                    command2.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @wit";
                }
                else
                {
                    command2.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @wit";
                }
                command2.Parameters.AddWithValue("@wit", white);
                command2.ExecuteNonQuery();

                MySqlCommand command3 = _myconnection.CreateCommand();

                if (result == "0")
                {
                    command3.CommandText = "UPDATE deelnemers SET Verlies = Verlies + 1 WHERE Deelneemnummer = @zwart";
                }
                else if (result == "1")
                {
                    command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk + 1 WHERE Deelneemnummer = @zwart";
                }
                else
                {
                    command3.CommandText = "UPDATE deelnemers SET Winst = Winst + 1 WHERE Deelneemnummer = @zwart";
                }
                command3.Parameters.AddWithValue("@zwart", black);
                command3.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong...");
            }
            finally
            {
                _myconnection.Close();
                MessageBox.Show("De partij is toegevoegd!");
            }
            return ELO;
        }

        public DataTable deletePartij(int partijNr, string uitslag, int wit, int zwart)
        {
            DataTable myTable = new DataTable();
            try
            {
                _myconnection.Open();

                MySqlCommand command2 = _myconnection.CreateCommand();
                MySqlCommand command3 = _myconnection.CreateCommand();

                if (uitslag == "1-0")
                {
                    command2.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @wit";
                    command3.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @zwart";
                }
                else if(uitslag == "1/2-1/2")
                {
                    command2.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @wit";
                    command3.CommandText = "UPDATE deelnemers SET Gelijk = Gelijk - 1 WHERE Deelneemnummer = @zwart";
                }
                else
                {
                    command3.CommandText = "UPDATE deelnemers SET Winst = Winst - 1 WHERE Deelneemnummer = @zwart";
                    command2.CommandText = "UPDATE deelnemers SET Verlies = Verlies - 1 WHERE Deelneemnummer = @wit";
                }
                command2.Parameters.AddWithValue("@wit", wit);
                command3.Parameters.AddWithValue("@zwart", zwart);

                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();

                MySqlCommand command = _myconnection.CreateCommand();
                command.CommandText = "DELETE FROM partijen WHERE Partijnummer = @partijnummer";
                command.Parameters.AddWithValue("@partijnummer", partijNr);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong...");
            }
            finally
            {
                _myconnection.Close();
                MessageBox.Show("De partij is verwijderd!");
            }
            return myTable;
        }

        public DataTable searchTextBlock2(string selected, string filledIn, string operater)
        {
            DataTable myTable = new DataTable();
            try
            {
                _myconnection.Open();

                MySqlCommand command = _myconnection.CreateCommand();
                if (selected == "Partijnummer")
                {
                    if (operater == "Kleiner dan/Minder dan/Voor deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Partijnummer < @filledIn";
                    }
                    else if(operater == "Groter dan/Meer dan/Na deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Partijnummer > @filledIn";
                    }
                    else
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Partijnummer = @filledIn";
                    }
                }
                else if(selected == "Spelerstijd")
                {
                    if (operater == "Kleiner dan/Minder dan/Voor deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Spelerstijd < @filledIn";
                    }
                    else if (operater == "Groter dan/Meer dan/Na deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Spelerstijd > @filledIn";
                    }
                    else
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Spelerstijd = @filledIn";
                    }
                }
                else if(selected == "Datum")
                {
                    if (operater == "Kleiner dan/Minder dan/Voor deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Datum < @filledIn";
                    }
                    else if (operater == "Groter dan/Meer dan/Na deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Datum > @filledIn";
                    }
                    else
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Datum = @filledIn";
                    }
                }
                else if (selected == "Tijd")
                {
                    if (operater == "Kleiner dan/Minder dan/Voor deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Tijd < @filledIn";
                    }
                    else if (operater == "Groter dan/Meer dan/Na deze datum/tijd")
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Tijd > @filledIn";
                    }
                    else
                    {
                        command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Tijd = @filledIn";
                    }
                }
                else
                {
                    command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE d.Achternaam = @filledIn OR de.Achternaam = @filledIn";
                }
                command.Parameters.AddWithValue("@filledIn", filledIn);
                command.ExecuteNonQuery();
                MySqlDataReader reader = command.ExecuteReader();
                myTable.Load(reader);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong...");
            }
            finally
            {
                _myconnection.Close();
            }
            return myTable;
        }

        public DataTable search(string uitslag)
        {
            DataTable myTable = new DataTable();
            try
            {
                _myconnection.Open();

                MySqlCommand command = _myconnection.CreateCommand();
                if (uitslag == "Winst wit")
                {
                    command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Uitslag = '0'";
                }
                else if (uitslag == "Winst zwart")
                {
                    command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Uitslag = '2'";
                }
                else
                {
                    command.CommandText = "SELECT Partijnummer, Datum, Tijd, Uitslag, CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd, Wit as WitNR, Zwart as ZwartNR FROM partijen p INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart WHERE Uitslag = '1'";
                }

                MySqlDataReader reader = command.ExecuteReader();
                myTable.Load(reader);

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong...");
            }
            finally
            {
                _myconnection.Close();
            }
            return myTable;
        }
    }
}
