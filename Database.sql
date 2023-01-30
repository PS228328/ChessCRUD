-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Gegenereerd op: 30 jan 2023 om 09:38
-- Serverversie: 5.7.36
-- PHP-versie: 8.1.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `schaken`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `deelnemers`
--

CREATE TABLE `deelnemers` (
  `Deelneemnummer` int(11) NOT NULL,
  `Voornaam` varchar(40) NOT NULL,
  `Tussenvoegsel` varchar(20) DEFAULT '',
  `Achternaam` varchar(40) NOT NULL,
  `Geboortedatum` date NOT NULL,
  `Land` varchar(40) NOT NULL,
  `Winst` int(11) NOT NULL DEFAULT '0',
  `Gelijk` int(11) NOT NULL DEFAULT '0',
  `Verlies` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `deelnemers`
--

INSERT INTO `deelnemers` (`Deelneemnummer`, `Voornaam`, `Tussenvoegsel`, `Achternaam`, `Geboortedatum`, `Land`, `Winst`, `Gelijk`, `Verlies`) VALUES
(1, 'Magnus', '', 'Carlsen', '1990-11-30', 'Noorwegen', 1, 2, 0),
(2, 'Hikaru', '', 'Nakamura', '1987-12-09', 'VS', 2, 0, 1),
(3, 'Wesley', '', 'So', '1993-10-09', 'Filipijnen', 0, 1, 2),
(4, 'Vishy', '', 'Anand', '1969-12-11', 'India', 1, 1, 1);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `partijen`
--

CREATE TABLE `partijen` (
  `Partijnummer` int(11) NOT NULL,
  `Wit` int(11) NOT NULL,
  `Zwart` int(11) NOT NULL,
  `Datum` date NOT NULL,
  `Tijd` time NOT NULL,
  `Spelerstijd` int(11) NOT NULL,
  `Uitslag` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `partijen`
--

INSERT INTO `partijen` (`Partijnummer`, `Wit`, `Zwart`, `Datum`, `Tijd`, `Spelerstijd`, `Uitslag`) VALUES
(1, 1, 2, '2023-01-30', '15:50:00', 20, 0),
(2, 3, 4, '2023-01-31', '17:00:00', 20, 2),
(3, 1, 3, '2023-02-01', '10:30:00', 20, 1),
(4, 4, 2, '2023-02-04', '12:30:00', 20, 2),
(5, 4, 1, '2023-02-06', '13:30:00', 20, 1),
(6, 2, 3, '2023-02-09', '20:00:00', 20, 0);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `deelnemers`
--
ALTER TABLE `deelnemers`
  ADD PRIMARY KEY (`Deelneemnummer`);

--
-- Indexen voor tabel `partijen`
--
ALTER TABLE `partijen`
  ADD PRIMARY KEY (`Partijnummer`),
  ADD KEY `Wit` (`Wit`),
  ADD KEY `Zwart` (`Zwart`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `deelnemers`
--
ALTER TABLE `deelnemers`
  MODIFY `Deelneemnummer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT voor een tabel `partijen`
--
ALTER TABLE `partijen`
  MODIFY `Partijnummer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `partijen`
--
ALTER TABLE `partijen`
  ADD CONSTRAINT `partijen_ibfk_1` FOREIGN KEY (`Wit`) REFERENCES `deelnemers` (`Deelneemnummer`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `partijen_ibfk_2` FOREIGN KEY (`Zwart`) REFERENCES `deelnemers` (`Deelneemnummer`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
