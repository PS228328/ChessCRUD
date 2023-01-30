<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Nieuwe deelnemer aanmaken</title>
    <link rel="stylesheet" href="css/style">
</head>

<body>
    <h1>Maak deelnemer aan</h1>
    <?php
    include_once "./classes/schaakDB.php";

    $db = new schaakDB();

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
        else if($jaar > 2014)
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
            $db->maakDeelnemerAan($_POST['Voornaam'], $_POST['Tussenvoegsel'], $_POST['Achternaam'], $sqlstring, $_POST['Land']);
            echo "<p>Het versturen is gelukt!</p>";
        }
    }

    ?>
    <form action="" method="post">
    <br>
        <table>
            <tr>
                <td>Voornaam</td>
                <td><input type="text" name="Voornaam" required></td>
            </tr>
            <tr>
                <td>Tussenvoegsel</td>
                <td><input type="text" name="Tussenvoegsel"></td>
            </tr>
            <tr>
                <td>Achternaam</td>
                <td><input type="text" name="Achternaam" required></td>
            </tr>
            <tr>
                <td>Geboortedag</td>
                <td><input class="dateinput" type="number" name="Geboortedag" min="1" max="31" required></td>
            </tr>
            <tr>
                <td>Geboortemaand</td>
                <td>
                    <select name="Geboortemaand">
                        <option>Januari</option>
                        <option>Februari</option>
                        <option>Maart</option>
                        <option>April</option>
                        <option>Mei</option>
                        <option>Juni</option>
                        <option>Juli</option>
                        <option>Augustus</option>
                        <option>September</option>
                        <option>Oktober</option>
                        <option>November</option>
                        <option>December</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>Geboortejaar</td>
                <td><input class="dateinput" type="number" min="1000" max="9999" name="Geboortejaar" required pattern="[0-9]{4}"></td>
            </tr>
            <tr>
                <td>Land</td>
                <td><input type="text" name="Land" required></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="submit" value="Deelnemer aanmaken" href="maakdeelnemer.php"></td>
            </tr>
        </table>
        <h4>Voorwaarden:</h4>
        <ul>
            <li>De deelnemer moet geboren zijn voor 1 januari 2015 om mee te mogen doen</li>
            <li>De deelnemer moet toegelaten zijn voor het toernooi door zijn/haar schaakclub</li>
            <br>
        </ul>
    </form>
    <a href="index.php">Ga terug naar het overzicht</a>
</body>

</html>