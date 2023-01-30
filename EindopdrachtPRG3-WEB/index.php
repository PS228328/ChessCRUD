<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lijst van deelnemers</title>
    <link rel="stylesheet" href="css/style">
    <link rel="stylesheet" href="css/table">
</head>
<body>
    <h1>Deelnemers overzicht</h1>
    <?php
    include_once "./classes/schaakDB.php";

    $db = new schaakDB();

    //Hieronder kijken we of de zoekbalk is gepost, zo ja. Dan moeten we alleen de deelnemers ophalen met het zoekwoord,
    //Zo niet, dan moet alles worden opgehaald

    if($_POST)
    {
        $rows = $db->haalAlleDeelnemersOpMetZoekFunctie($_POST['zoekwoord'], $_POST['ZoekOpRij']);
    }
    else
    {
        $rows = $db->haalAlleDeelnemersOp();
    }

    ?>
    <form action="index.php" method="POST">
        <input type="text" name="zoekwoord" ><br>        
        <select name="ZoekOpRij">
            <option>Voornaam</option>                        
            <option>Achternaam</option>
            <option>Land</option>
        </select>
        <button style="color: black; width: 15%;" type="submit">Zoek...</button>
    </form><br>
    <table>
        <tr>
            <th>Deelnemersnummer</th>
            <th>Voornaam</th>
            <th>Tussenvoegsel</th>
            <th>Achternaam</th>
            <th>Geboortedatum (JJJJ-MM-DD)</th>
            <th>Land</th>
            <th>W</th>
            <th>G</th>
            <th>V</th>
        </tr>
        <?php
        //De variabele rows is gevuld. Voor elke rij moeten we nu een tabelrij "echo'en"
        foreach($rows as $row)
        {
            $id = $row['Deelneemnummer'];

            echo "
            <tr>
            <th>$row[Deelneemnummer]</th>
            <td>$row[Voornaam]</td>
            <td>$row[Tussenvoegsel]</td>
            <td>$row[Achternaam]</td>
            <td>$row[Geboortedatum]</td>
            <td>$row[Land]</td>
            <td style='background-color: lightgreen;'>$row[Winst]</td>
            <td>$row[Gelijk]</td>
            <td style='background-color: salmon;'>$row[Verlies]</td>
            <td style='background-color: blue;'><a href='updatedeelnemer.php?id=$id'>Update</a></td>
            <td style='background-color: red;'><a href='verwijderdeelnemer.php?id=$id'>Delete</a></td>
            </tr>";
        }
        ?>
    </table>
    <br>
    <a href="maakdeelnemer.php">Nieuwe deelnemer aanmaken</a>
    <h1 style="background-color: orange;">Partijen overzicht</h1>
    <p style="background-color: black;">De partijen kunt u niet bewerken. Neem contact op met de beheerder als u partijen wilt invoegen of bewerken.</p>
    <table>
        <tr>
            <th style='background-color: gray; color: black;'>Partijnummer</th>
            <th style='background-color: white; color: black;'>Wit</th>
            <th style='background-color: black; color: white;'>Zwart</th>
            <th style='background-color: yellow; color: purple;'>Datum</th>
            <th style='background-color: red; color: yellow;'>Tijd</th>
            <th style='background-color: brown; color: yellow;'>Tijd per speler</th>
            <th style='background-color: blue; color: lightskyblue;'>Uitslag</th>
        </tr>
    <?php
    include_once "./classes/schaakDB.php";
    
    $db2 = new schaakDB();

    $rows2 = $db2->haalAllePartijenOp();
    foreach($rows2 as $row2)
        {
            echo "
            <tr>
            <td>$row2[Partijnummer]</td>
            <td>$row2[Wit]</td>
            <td>$row2[Zwart]</td>
            <td>$row2[Datum]</td>
            <td>$row2[Tijd]</td>
            <td>$row2[Spelerstijd] minuten</td>";

            if($row2['Uitslag'] == 0)
            {
                echo "<td>1-0</td>";
            }
            else if($row2['Uitslag'] == 1)
            {
                echo "<td>1/2-1/2</td>";
            }
            else
            {
                echo "<td>0-1</td>";
            }

            echo "</tr>";
        }
    ?>
    </table>
</body>
</html>