<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Wijzig gegevens deelnemer</title>
    <link rel="stylesheet" href="css/style">
</head>

<body>
    <h1>Wijzig gegevens deelnemer</h1>
    <?php
    include_once "./classes/schaakDB.php";

    $db = new schaakDB();
    $rows = $db->haalEenDeelnemerOp($_GET['id']);

    foreach($rows as $row)
    {
        $stringdate = $row['Geboortedatum'];
        $string = explode("-", $stringdate);
    }
    $counter = 0;
    $looper = 0;
    $months = array(1 => "Januari", 2 => "Februari", 3 => "Maart", 4 => "April", 5 => "Mei", 6 => "Juni", 7 => "Juli", 8 => "Augustus", 9 => "September", 10 => "Oktober", 11 => "November", 12 => "December");
    
    if($string[1] == "01")
    {
        $counter = 1;
    }
    else if($string[1] == "02")
    {
        $counter = 2;
    }
    else if($string[1] == "03")
    {
        $counter = 3;
    }
    else if($string[1] == "04")
    {
        $counter = 4;
    }
    else if($string[1] == "05")
    {
        $counter = 5;
    }
    else if($string[1] == "06")
    {
        $counter = 6;
    }
    else if($string[1] == "07")
    {
        $counter = 7;
    }
    else if($string[1] == "08")
    {
        $counter = 8;
    }
    else if($string[1] == "09")
    {
        $counter = 9;
    }
    else if($string[1] == "10")
    {
        $counter = 10;
    }
    else if($string[1] == "11")
    {
        $counter = 11;
    }
    else if($string[1] == "12")
    {
        $counter = 12;
    }

    if($_POST)
    {
        $maand = $_POST['Geboortemaand'];
        $dag = $_POST["Geboortedag"];
        $jaar = $_POST["Geboortejaar"]; 
        $x = $jaar;

        while($x >= 4)
        {
            $x = $x - 4;
        }

        if(($maand == "Februari" || $maand == "April" || $maand == "Juni" || $maand == "September" || $maand == "November") && $dag == 31)
        {
            echo "<p>ERROR! De geselecteerde maand heeft maar 30 dagen</p>";
        }
        else if($maand == "Februari" && $dag > 29)
        {
            echo "<p>ERROR! Februari heeft maar 28 of 29 dagen</p>";
        }
        else if($maand == "Februari" && $dag == 29 && $x != 0)
        {
            echo "<p>ERROR! Februari heeft maar 28 dagen in dit jaar</p>";
        }
        else if($jaar > 2013)
        {
            echo "<p>ERROR! Sorry, deelnemer is te jong...</p>";
        }
        else
        {
            $sqldag = "";
            $sqlmaand = "";
            if($maand == "Januari")
            {
                $sqlmaand = "01";
            }
            else if($maand == "Februari")
            {
                $sqlmaand = "02";
            }
            else if($maand == "Maart")
            {
                $sqlmaand = "03";
            }
            else if($maand == "April")
            {
                $sqlmaand = "04";
            }
            else if($maand == "Mei")
            {
                $sqlmaand = "05";
            }
            else if($maand == "Juni")
            {
                $sqlmaand = "06";
            }
            else if($maand == "Juli")
            {
                $sqlmaand = "07";
            }
            else if($maand == "Augustus")
            {
                $sqlmaand = "08";
            }
            else if($maand == "September")
            {
                $sqlmaand = "09";
            }
            else if($maand == "Oktober")
            {
                $sqlmaand = "10";
            }
            else if($maand == "November")
            {
                $sqlmaand = "11";
            }
            else if($maand == "December")
            {
                $sqlmaand = "12";
            }

            if($dag < 10)
            {
                $sqldag = "0$dag";
            }
            else
            {
                $sqldag = $dag;
            }

            $sqlstring = "$jaar-$sqlmaand-$sqldag";
            $db->updateDeelnemer($_GET['id'],$_POST['Voornaam'], $_POST['Tussenvoegsel'], $_POST['Achternaam'], $sqlstring, $_POST['Land']);
            echo "<p>Het versturen is gelukt!</p>";
        }
    }

    ?>
    <form action="" method="post">
    <br>
    <?php

    foreach($rows as $row)
    {
        echo "
        <table>
            <tr>
                <td>Voornaam</td>
                <td><input type='text' value=$row[Voornaam] name='Voornaam' required></td>
            </tr>
            <tr>
                <td>Tussenvoegsel</td>
                <td><input type='text' name='Tussenvoegsel' value=$row[Tussenvoegsel]></td>
            </tr>
            <tr>
                <td>Achternaam</td>
                <td><input type='text' value=$row[Achternaam] name='Achternaam' required></td>
            </tr>
            <tr>
                <td>Geboortedag</td>
                <td><input class='dateinput' type='number' name='Geboortedag' value=$string[2] min='1' max='31' required></td>
            </tr>
            <tr>
                <td>Geboortemaand</td>
                <td>
                    <select id='Maand' name='Geboortemaand'>";
                    while($looper < 12)
                    {
                        echo "<option>$months[$counter]</option>";
                        $looper++;
                        $counter++;
                        if($counter > 12)
                        {
                            $counter = 1;
                        }
                    }   
                echo"</select>
                </td>
            </tr>
            <tr>
                <td>Geboortejaar</td>
                <td><input class='dateinput' type='number' min='1000' max='9999' value=$string[0] name='Geboortejaar' required pattern='[0-9]{4}'></td>
            </tr>
            <tr>
                <td>Land</td>
                <td><input type='text' value=$row[Land] name='Land' required></td>
            </tr>
            <tr>
                <td></td>
                <td><input type='submit' value='Wijzig gegevens' href='maakdeelnemer.php'></td>
            </tr>
        </table>
        <h4>Voorwaarden:</h4>
        <ul>
            <li>De deelnemer moet geboren zijn voor 1 januari 2014 om mee te mogen doen</li>
            <li>De deelnemer moet toegelaten zijn voor het toernooi door zijn/haar schaakclub</li>
            <br>
        </ul>";
    }
        ?>
    </form>
    <a href="index.php">Ga terug naar het overzicht</a>
</body>

</html>