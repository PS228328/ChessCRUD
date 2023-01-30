<?php
include_once "./classes/schaakDB.php";

$db = new schaakDB;

if(!empty($_GET["id"]))
{
    $db->verwijderDeelnemer($_GET["id"]);
}
header("Location: index.php");

?>