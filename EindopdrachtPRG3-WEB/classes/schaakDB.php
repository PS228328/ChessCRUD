<?php

class schaakDB
{
    const DSN = "mysql:host=localhost;dbname=schaken";
    const USER = "root";
    const PASSWD = "";

    function maakDeelnemerAan($voornaam, $tussenvoegsel, $achternaam, $geboortedatum, $land)
    {
        //Voeg de deelnemer toe met alle ingevulde informatie door de gebruiker
        $pdo = new PDO(self::DSN,self::USER,self::PASSWD);

        $statement = $pdo->prepare("INSERT INTO deelnemers(Voornaam,Tussenvoegsel,Achternaam,Geboortedatum,Land)VALUES(:Voornaam,:Tussenvoegsel,:Achternaam,:Geboortedatum,:Land)");

        $statement->bindValue(":Voornaam",$voornaam,PDO::PARAM_STR);
        $statement->bindValue("Tussenvoegsel",$tussenvoegsel,PDO::PARAM_STR);
        $statement->bindValue(":Achternaam",$achternaam,PDO::PARAM_STR);
        $statement->bindValue(":Geboortedatum",$geboortedatum,PDO::PARAM_STR);
        $statement->bindValue(":Land",$land,PDO::PARAM_STR);

        $statement->execute();
    }

    function haalAlleDeelnemersOp()
    {
        //Hier halen we alle deelnemers op
        $pdo = new PDO(self::DSN,self::USER,self::PASSWD);

        $statement = $pdo->prepare("SELECT * FROM deelnemers");

        $statement->execute();

        $rows = $statement->fetchAll(PDO::FETCH_ASSOC);

        return $rows;
    }

    function haalAlleDeelnemersOpMetZoekFunctie($zoekwoord, $waarop)
    {
        //Wanneer er een zoekwoord is ingetyped, dan word alles opgehaald waar de achter, voornaam of land overeenkomt met het zoekwoord
        $pdo = new PDO(self::DSN,self::USER,self::PASSWD);

        if($waarop == "Achternaam")
        {
            $statement = $pdo->prepare("SELECT * FROM deelnemers WHERE Achternaam like '%$zoekwoord%'");
        }
        else if($waarop == "Voornaam")
        {
            $statement = $pdo->prepare("SELECT * FROM deelnemers WHERE Voornaam like '%$zoekwoord%'");
        }
        else
        {
            $statement = $pdo->prepare("SELECT * FROM deelnemers WHERE Land like '%$zoekwoord%'");
        }

        $statement->execute();

        $rows = $statement->fetchAll(PDO::FETCH_ASSOC);

        return $rows;
    }

    function verwijderDeelnemer($id)
    {
        //Hier verwijderen we een deelnemer met het id. Het id staat in de button waardoor we deze makkelijk kunnen ophalen
        $pdo = new PDO(self::DSN,self::USER,self::PASSWD);

        $statement = $pdo->prepare("DELETE FROM deelnemers WHERE Deelneemnummer = :Deelneemnummer");

        $statement->bindValue(":Deelneemnummer",$id,PDO::PARAM_INT);

        $statement->execute();
    }

    function updateDeelnemer($id, $voornaam, $tussenvoegsel, $achternaam, $geboortedatum, $land)
    {
        //Hier updaten we een deelnemer, alle ingevulde gegevens worden gewijzigd
        $pdo = new PDO(self::DSN,self::USER,self::PASSWD);

        $statement = $pdo->prepare("UPDATE deelnemers SET Voornaam = :Voornaam, Tussenvoegsel = :Tussenvoegsel, Achternaam = :Achternaam, Geboortedatum = :Geboortedatum, Land = :Land WHERE Deelneemnummer = :Deelneemnummer");

        $statement->bindValue(":Deelneemnummer", $id, PDO::PARAM_INT);
        $statement->bindValue(":Voornaam", $voornaam, PDO::PARAM_STR);
        $statement->bindValue(":Tussenvoegsel", $tussenvoegsel, PDO::PARAM_STR);
        $statement->bindValue(":Achternaam", $achternaam, PDO::PARAM_STR);
        $statement->bindValue(":Geboortedatum", $geboortedatum, PDO::PARAM_STR);
        $statement->bindValue(":Land", $land, PDO::PARAM_STR);

        $statement->execute();
    }

    function haalEenDeelnemerOp($id)
    {
        //Hier halen we een deelnemer op met het meegegeven id
        $pdo = new PDO(self::DSN,self::USER,self::PASSWD);

        $statement = $pdo->prepare("SELECT * FROM deelnemers WHERE Deelneemnummer = :Deelneemnummer");

        $statement->bindValue("Deelneemnummer", $id, PDO::PARAM_INT);

        $statement->execute();

        $rows = $statement->fetchAll(PDO::FETCH_ASSOC);

        return $rows;
    }

    function haalAllePartijenOp()
    {
        //Hier worden alle partijen opgehaald, deze kunnen in PHP niet bewerkt worden
        $pdo2 = new PDO(self::DSN,self::USER,self::PASSWD);

        $statement2 = $pdo2->prepare("SELECT Partijnummer, Datum, Tijd, Uitslag, 
        CONCAT(d.Voornaam, ' ', d.Tussenvoegsel, ' ', d.Achternaam) as Wit, 
        CONCAT(de.Voornaam, ' ', de.Tussenvoegsel, ' ', de.Achternaam) as Zwart, Spelerstijd 
        FROM partijen p 
        INNER JOIN deelnemers d ON d.Deelneemnummer = p.Wit 
        INNER JOIN deelnemers de ON de.Deelneemnummer = p.Zwart");

        $statement2->execute();

        $rows2 = $statement2->fetchAll(PDO::FETCH_ASSOC);

        return $rows2;
    }
}
?>