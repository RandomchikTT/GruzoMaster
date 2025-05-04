-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.4.24-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              12.0.0.6468
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных gruzomaster
CREATE DATABASE IF NOT EXISTS `gruzomaster` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `gruzomaster`;

-- Дамп структуры для таблица gruzomaster.cargo
CREATE TABLE IF NOT EXISTS `cargo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ID_user_creator` int(11) DEFAULT NULL,
  `ID_company` int(11) DEFAULT NULL,
  `Name` text DEFAULT NULL,
  `Description` text DEFAULT NULL,
  `AddressFromCargo` text DEFAULT NULL,
  `AddressToCargo` text DEFAULT NULL,
  `Price` int(11) DEFAULT NULL,
  `DeliveryType` int(11) DEFAULT NULL,
  `CargoLogs` longtext DEFAULT NULL,
  `ForwarderID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.cargo: ~22 rows (приблизительно)
INSERT INTO `cargo` (`ID`, `ID_user_creator`, `ID_company`, `Name`, `Description`, `AddressFromCargo`, `AddressToCargo`, `Price`, `DeliveryType`, `CargoLogs`, `ForwarderID`) VALUES
	(1, 1, 6, 'Перевозка цинка', 'Описание здесь какое то', 'Алибегова 12', 'Алибегова 27', 1500, 1, '[]', 1),
	(2, 1, 8, 'assdasdas', 'asdasdasdasd', 'asdasd', 'asdasd', 5000, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2024-05-29T14:01:12.2434426+03:00"},{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Внес изменения в данные о грузе: Изменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8Изменил стоимость груза с 123132 на 123132132","TimeCreated":"2024-06-03T19:03:45.7055258+03:00"},{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Внес изменения в данные о грузе: Изменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8","TimeCreated":"2024-06-03T19:04:50.7659674+03:00"},{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Внес изменения в данные о грузе: Изменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8","TimeCreated":"2024-06-03T19:05:03.4321858+03:00"},{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Внес изменения в данные о грузе: Изменил стоимость груза с 123132132 на 1231321323","TimeCreated":"2024-06-03T19:08:26.232893+03:00"},{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Внес изменения в данные о грузе: Изменил стоимость груза с 1.231.321.323₽ на 5.000₽","TimeCreated":"2024-06-03T19:11:20.3312942+03:00"}]', 2),
	(3, 1, 9, 'Перевозка Сырья', 'Сырье для изготовки мед препаратов', 'улица Алибегова 12', 'Кропткина 32', 15000, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2024-06-21T12:22:46.7163758+03:00"}]', -1),
	(4, 1, 12, 'weasdasd', 'ewrwer', 'sdfasdasdasd', 'sdfdsdasd', 1123, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-01-27T17:58:44.3382279+03:00"},{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Внес изменения в данные о грузе: Изменил название груза с weasd на weasdasdИзменил адрес отправки груза с sdfasdasd на sdfasdasdasd","TimeCreated":"2025-01-27T17:59:32.1833907+03:00"}]', -1),
	(5, 1, 10, '123asd', 'asdasd', 'asdasd', 'asdasd', 1231, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-20T15:48:01.8570623+03:00"}]', -1),
	(6, 1, 10, 'asdasd', 'asdasd', 'asdasd', 'asdasdas', 121, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-20T15:49:08.3654156+03:00"}]', -1),
	(7, 1, 11, 'asdasd', 'asdasd', 'sdasdasd', 'asdasdasd', 11, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-20T15:59:45.9699809+03:00"}]', -1),
	(8, 1, 10, 'asdasd', 'asdasd', 'asdasd', 'asdasd', 121, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-20T16:02:40.3729127+03:00"}]', -1),
	(9, 1, 11, 'asdasd', 'asdaad', 'asdas', 'asdasd', 1231, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-20T16:03:33.4046282+03:00"}]', -1),
	(10, 1, 10, 'asdasd', 'asdasd', 'asddas', 'asdasd', 12, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-20T16:04:25.8143033+03:00"}]', -1),
	(11, 1, 10, 'фывфыв', 'фывфывфыв', 'фывфывфыв', 'фывфывфыв', 1212, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T15:03:04.3620374+03:00"}]', -1),
	(12, 1, 11, 'asdasd', 'asdasd', 'asdasd', 'asasddasd', 12, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T15:17:36.5305415+03:00"}]', -1),
	(13, 1, 11, 'asdasd', 'asdasd', 'asdasd', 'asasddasd', 12, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T15:17:37.1136895+03:00"}]', -1),
	(14, 1, 12, 'asdasd', 'asdasd', 'asdasd', 'asdasd', 12, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T15:20:25.8753069+03:00"}]', -1),
	(15, 1, 11, 'asdasd', 'asdasd', 'asdasd', 'asdasd', 112, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T15:21:21.4909936+03:00"}]', -1),
	(16, 1, 8, 'фывфыв', 'фывфывфыв', 'фывфывфыв', 'фывфывфы', 121, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:11:35.0746581+03:00"}]', -1),
	(17, 1, 10, 'asdasd', 'aSDASDA', 'ASDASD', 'asdasd', 12312, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:21:24.0067313+03:00"}]', -1),
	(18, 1, 8, 'asdasdasd', 'asdasda', 'asdasdasd', 'sdasdasd', 121212, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:21:48.6079722+03:00"}]', -1),
	(19, 1, 8, 'asdasdasd', 'asdasdasd', 'asdasdasd', 'asdasd', 12312, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:22:50.8002238+03:00"}]', -1),
	(20, 1, 8, 'asdasdads', 'qsdasdasd', 'asdasd', 'asdasda', 1111, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:23:57.639008+03:00"}]', -1),
	(21, 1, 10, 'asdasdas', 'asdasdasd', 'asdasdasd', 'asdasddas', 1231, 3, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:27:33.5648632+03:00"}]', -1),
	(22, 1, 8, 'фывфыв', 'фывфыв', 'фывфывфыв', 'фывфыв', 1111, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-21T16:47:24.8174179+03:00"}]', -1),
	(23, 1, 10, 'xdfghj', 'sdfghjk', 'srydfghkulj', 'derrtfyug', 1231, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-02-22T15:39:49.1819012+03:00"}]', -1),
	(24, 1, 8, 'asdasdasd', 'asdasdasdasd', 'улица Алибегова', 'улица Колесникова', 15000, 3, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-03-04T15:23:57.3675539+03:00"}]', -1),
	(25, 1, 11, 'ывфывфыв', 'фыывфывфыв', 'фывфывфыв', 'фывфывфыв', 1212, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-03-10T14:49:24.2323042+03:00"}]', -1),
	(28, 1, 8, 'asdasdsd', 'asdasdasda', 'asdasdasdasd', 'asdasdasd', 12321, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-03-11T15:16:14.5693153+03:00"}]', -1),
	(29, 1, 8, 'dsasasdasd', 'ASDSD', 'ASDASD', 'ASDASD', 12345, 0, '[{"UserCreated":{"Login":"randomchik","Name":"Савелий","UserType":1,"ID":1},"Description":"Создал заявку на выполнение груза","TimeCreated":"2025-04-10T14:26:29.304231+03:00"}]', -1);

-- Дамп структуры для таблица gruzomaster.cargo_log
CREATE TABLE IF NOT EXISTS `cargo_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cargo_id` int(11) NOT NULL,
  `action` varchar(255) NOT NULL,
  `action_date` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `cargo_id` (`cargo_id`),
  CONSTRAINT `cargo_log_ibfk_1` FOREIGN KEY (`cargo_id`) REFERENCES `cargo` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.cargo_log: ~0 rows (приблизительно)
INSERT INTO `cargo_log` (`id`, `cargo_id`, `action`, `action_date`) VALUES
	(1, 28, 'INSERT', '2025-03-11 12:16:15'),
	(2, 29, 'INSERT', '2025-04-10 11:26:29');

-- Дамп структуры для таблица gruzomaster.cargo_parts
CREATE TABLE IF NOT EXISTS `cargo_parts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CargoID` int(11) DEFAULT NULL,
  `TransportID` int(11) DEFAULT NULL,
  `DeliveryDate` text DEFAULT NULL,
  `Weight` int(11) DEFAULT NULL,
  `Volume` int(11) DEFAULT NULL,
  `DeliveryType` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `CargoID` (`CargoID`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.cargo_parts: ~48 rows (приблизительно)
INSERT INTO `cargo_parts` (`ID`, `CargoID`, `TransportID`, `DeliveryDate`, `Weight`, `Volume`, `DeliveryType`) VALUES
	(1, 21, 6, '2025-02-21', 1, 1, 3),
	(2, 21, 7, '2025-02-21', 1, 0, 3),
	(3, 21, 8, '2025-02-21', 1, 0, 3),
	(4, 21, 9, '2025-02-21', 1, 0, 3),
	(5, 21, 10, '2025-02-21', 1, 0, 3),
	(6, 21, 11, '2025-02-21', 7, 0, 3),
	(7, 22, 6, '2025-02-21', 1, 1, 0),
	(8, 22, 7, '2025-02-21', 1, 1, 0),
	(9, 22, 8, '2025-02-21', 1, 1, 0),
	(10, 22, 9, '2025-02-21', 1, 1, 0),
	(11, 22, 10, '2025-02-21', 1, 1, 0),
	(12, 22, 11, '2025-02-21', 12, 0, 0),
	(13, 22, 6, '2025-02-22', 1, 0, 0),
	(14, 22, 7, '2025-02-22', 1, 0, 0),
	(15, 22, 8, '2025-02-22', 1, 0, 0),
	(16, 22, 9, '2025-02-22', 1, 0, 0),
	(17, 22, 10, '2025-02-22', 1, 0, 0),
	(18, 22, 11, '2025-02-22', 12, 0, 0),
	(19, 22, 6, '2025-02-23', 1, 0, 0),
	(20, 22, 7, '2025-02-23', 1, 0, 0),
	(21, 22, 8, '2025-02-23', 1, 0, 0),
	(22, 22, 9, '2025-02-23', 1, 0, 0),
	(23, 22, 10, '2025-02-23', 1, 0, 0),
	(24, 22, 11, '2025-02-23', 11, 0, 0),
	(25, 23, 6, '2025-02-22', 1, 1, 0),
	(26, 23, 7, '2025-02-22', 1, 1, 0),
	(27, 23, 8, '2025-02-22', 1, 1, 0),
	(28, 23, 9, '2025-02-22', 1, 1, 0),
	(29, 23, 10, '2025-02-22', 1, 1, 0),
	(30, 23, 11, '2025-02-22', 7, 5, 0),
	(31, 24, 6, '2025-03-04', 1, 1, 3),
	(32, 24, 7, '2025-03-04', 1, 1, 3),
	(33, 24, 8, '2025-03-04', 1, 1, 3),
	(34, 24, 9, '2025-03-04', 1, 1, 3),
	(35, 24, 10, '2025-03-04', 1, 1, 3),
	(36, 24, 11, '2025-03-04', 10, 10, 3),
	(37, 25, 6, '2025-03-10', 1, 1, 0),
	(38, 25, 7, '2025-03-10', 1, 1, 0),
	(39, 25, 8, '2025-03-10', 1, 1, 0),
	(40, 25, 9, '2025-03-10', 1, 1, 0),
	(41, 25, 10, '2025-03-10', 1, 1, 0),
	(42, 25, 11, '2025-03-10', 7, 7, 0),
	(43, 28, 6, '2025-03-11', 1, 1, 0),
	(44, 28, 7, '2025-03-11', 1, 1, 0),
	(45, 28, 8, '2025-03-11', 1, 1, 0),
	(46, 28, 9, '2025-03-11', 1, 1, 0),
	(47, 28, 10, '2025-03-11', 1, 1, 0),
	(48, 28, 11, '2025-03-11', 7, 7, 0),
	(49, 29, 6, '2025-04-10', 2, 2, 0),
	(50, 29, 7, '2025-04-10', 3, 3, 0),
	(51, 29, 8, '2025-04-10', 4, 4, 0),
	(52, 29, 9, '2025-04-10', 5, 1, 0),
	(53, 29, 10, '2025-04-10', 1, 0, 0);

-- Дамп структуры для представление gruzomaster.cargo_view
-- Создание временной таблицы для обработки ошибок зависимостей представлений
CREATE TABLE `cargo_view` (
	`ID` INT(11) NOT NULL,
	`ID_user_creator` INT(11) NULL,
	`ID_company` INT(11) NULL,
	`Name` TEXT NULL COLLATE 'utf8mb4_general_ci',
	`Description` TEXT NULL COLLATE 'utf8mb4_general_ci',
	`AddressFromCargo` TEXT NULL COLLATE 'utf8mb4_general_ci',
	`AddressToCargo` TEXT NULL COLLATE 'utf8mb4_general_ci',
	`Price` INT(11) NULL,
	`DeliveryType` INT(11) NULL,
	`CargoLogs` LONGTEXT NULL COLLATE 'utf8mb4_general_ci',
	`ForwarderID` INT(11) NULL
) ENGINE=MyISAM;

-- Дамп структуры для таблица gruzomaster.companies
CREATE TABLE IF NOT EXISTS `companies` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `Country` int(11) NOT NULL DEFAULT 0,
  `Contacts` text NOT NULL,
  `City` text NOT NULL,
  `TimeAdded` text NOT NULL,
  `Email` text NOT NULL,
  `BankData` text DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.companies: ~4 rows (приблизительно)
INSERT INTO `companies` (`id`, `Name`, `Country`, `Contacts`, `City`, `TimeAdded`, `Email`, `BankData`) VALUES
	(6, 'Fadexs', 1, '{"Russian":"+79125309887","Bellarusian":"+375296436030","Litva":"+79125030889"}', 'улица Притыцкого, д.12, кв. 53', '11.04.2024 9:29:16', 'saveliytoo2@gmail.com', '{"INN":"12985112321","LTD":"098765551231","NameOfBank":"Альфа Банк","NumberBank":"19820085321","AddressBank":"улица Притыцкого 12"}'),
	(7, 'TechSolutions', 3, '{"Litva":"+12025550123"}', '123 Main St, Suite 200', '11.04.2024 9:33:08', 'info@techsolutions.com', '{"INN":"","LTD":"123456789","NameOfBank":"Chase Bank","NumberBank":"9876543210","AddressBank":"456 Elm St"}'),
	(8, 'GlobalTrade', 3, '{"Litva":"+39012345678"}', 'Hauptstraße 5, 10115 Berlin', '11.04.2024 9:34:16', 'info@globaltrade.com', '{"INN":"","LTD":"0987654321","NameOfBank":"Deutsche Bank","NumberBank":"1234567890","AddressBank":"Unter den Linden 17"}'),
	(9, 'GreenTech Innovations', 1, '{"Bellarusian":"+375298761231"}', 'улица Мирошниченко, 23', '11.04.2024 9:48:15', 'green-tech@gmail.com', '{"INN":"89112354312","LTD":"","NameOfBank":"ПриорБанк","NumberBank":"123632139114","AddressBank":"улица Колесникова 21"}'),
	(10, 'MediCare Solutions', 3, '{"Litva":"+89123414115"}', '45 High Street, Lithuania', '11.04.2024 9:50:19', 'contact@medicaresolutions.co.lt', '{"INN":"","LTD":"5678901234","NameOfBank":"Barclays","NumberBank":"3456789012","AddressBank":"1 Churchill Place"}'),
	(11, 'Brabus', 2, '{"Bellarusian":"375441234567"}', 'Brauberg 18', '21.06.2024 12:35:55', 'Savagaysdalna9@xyila.com', '{"INN":"","LTD":"","NameOfBank":"AlphaNaebalovo","NumberBank":"6553734","AddressBank":"PoezdPukan 3"}'),
	(12, 'eaesd', 1, '{"Bellarusian":"123123123123"}', 'asdasd', '27.01.2025 17:58:05', 'asdsd@gmail.com', '{"INN":"123123","LTD":"123123123123","NameOfBank":"123123","NumberBank":"23","AddressBank":"123"}');

-- Дамп структуры для таблица gruzomaster.drivers
CREATE TABLE IF NOT EXISTS `drivers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` text DEFAULT NULL,
  `DateBirthday` text DEFAULT NULL,
  `MedSpravka` text DEFAULT NULL,
  `ListLicenses` text DEFAULT NULL,
  `SerialPassport` text DEFAULT NULL,
  `NumberPassport` text DEFAULT NULL,
  `PhoneNumbers` text DEFAULT NULL,
  `Address` text DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.drivers: ~11 rows (приблизительно)
INSERT INTO `drivers` (`id`, `FullName`, `DateBirthday`, `MedSpravka`, `ListLicenses`, `SerialPassport`, `NumberPassport`, `PhoneNumbers`, `Address`) VALUES
	(3, 'Петров Петр Петрович', '01.01.1999 0:00:00', '01.01.2023 0:00:00', '[1,2]', '123123123', '12312312312312', '{"Bellarusian":"+375296436030"}', '------'),
	(4, 'Иванов Иван Иванович', '02.02.1995 0:00:00', '01.01.2023 0:00:00', '[1]', '456456456', '45645645645645', '{"Russian":"+79001237567"}', '------'),
	(5, 'Сидоров Сидор Сидорович', '03.03.1990 0:00:00', '01.01.2023 0:00:00', '[3]', '789789789', '78978978978978', '{"Russian":"+79001784567"}', '------'),
	(6, 'Кузнецова Анна Ивановна', '04.04.1985 0:00:00', '01.01.2023 0:00:00', '[2]', '101010101', '10101010101010', '{"Russian":"+79001564567"}', '------'),
	(7, 'Александров Александр Александрович', '05.05.1980 0:00:00', '01.01.2023 0:00:00', '[1,3]', '121212121', '12121212121212', '{"Russian":"+79011234567"}', '------'),
	(8, 'Дмитриева Елена Дмитриевна', '06.06.1975 0:00:00', '01.01.2023 0:00:00', '[2]', '141414141', '14141414141414', '{"Russian":"+79001234667"}', '------'),
	(9, 'Федоров Федор Федорович', '07.07.1970 0:00:00', '01.01.2023 0:00:00', '[3]', '161616161', '16161616161616', '{"Russian":"+79001234588"}', '------'),
	(10, 'Егорова Евгения Егоровна', '08.08.1965 0:00:00', '01.01.2023 0:00:00', '[1,2]', '181818181', '18181818181818', '{"Russian":"+79005736567"}', '------'),
	(11, 'Андреев Андрей Андреевич', '09.09.1960 0:00:00', '01.01.2023 0:00:00', '[1]', '202020202', '20202020202020', '{"Russian":"+79001284667"}', '------'),
	(12, 'Ольгова Ольга Ольговна', '10.10.1955 0:00:00', '01.01.2023 0:00:00', '[2]', '222222222', '22222222222222', '{"Russian":"+79001239067"}', '------'),
	(13, 'Михайлов Михаил Михайлович', '11.11.1950 0:00:00', '01.01.2023 0:00:00', '[3]', '242424242', '24242424242424', '{"Russian":"+790019746657"}', '------');

-- Дамп структуры для процедура gruzomaster.InsertCargo
DELIMITER //
CREATE PROCEDURE `InsertCargo`(
	IN `user_creator_id` INT,
	IN `company_id` INT,
	IN `name` VARCHAR(255),
	IN `description` TEXT,
	IN `address_from` VARCHAR(255),
	IN `address_to` VARCHAR(255),
	IN `price` DECIMAL(10, 2),
	IN `delivery_type` INT,
	IN `cargo_logs` JSON,
	IN `forwarder_id` INT,
	OUT `new_cargo_id` INT
)
BEGIN
    -- Вставка данных в таблицу cargo
    INSERT INTO cargo (ID_user_creator, ID_company, Name, Description, AddressFromCargo, AddressToCargo, Price, DeliveryType, CargoLogs, ForwarderID)
    VALUES (user_creator_id, company_id, name, description, address_from, address_to, price, delivery_type, cargo_logs, forwarder_id);
    
    -- Получение последнего вставленного ID
    SET new_cargo_id = LAST_INSERT_ID();
END//
DELIMITER ;

-- Дамп структуры для таблица gruzomaster.transport
CREATE TABLE IF NOT EXISTS `transport` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Brand` int(11) NOT NULL DEFAULT 0,
  `Model` text NOT NULL,
  `Type` int(11) NOT NULL DEFAULT 0,
  `GovNumber` text NOT NULL,
  `TechInspection` text NOT NULL,
  `Capacity` int(11) DEFAULT NULL,
  `Volume` int(11) DEFAULT NULL,
  `CurrentDriverId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.transport: ~5 rows (приблизительно)
INSERT INTO `transport` (`id`, `Brand`, `Model`, `Type`, `GovNumber`, `TechInspection`, `Capacity`, `Volume`, `CurrentDriverId`) VALUES
	(6, 1, 'Daily', 1, 'о125мр719', '14.06.2024', 2, 2, 3),
	(7, 2, 'XF95', 1, 'н195мр67', '12.07.2024', 3, 3, 4),
	(8, 3, 'G440', 1, 'х137ре799', '29.06.2024', 4, 4, 5),
	(9, 5, 'C7H', 1, 'р517нк719', '11.04.2024', 5, 5, 6),
	(10, 4, 'Actros', 1, 'м131рн67', '14.06.2024', 6, 6, 7),
	(11, 4, 'asdasd', 3, 'asd', '20.01.1900', 12, 12, 5);

-- Дамп структуры для таблица gruzomaster.userlogs
CREATE TABLE IF NOT EXISTS `userlogs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` text DEFAULT NULL,
  `time` text DEFAULT NULL,
  `action` longtext DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.userlogs: ~62 rows (приблизительно)
INSERT INTO `userlogs` (`id`, `login`, `time`, `action`) VALUES
	(1, 'randomchik', '20.09.2023 19:35:56', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(2, 'randomchik', '21.09.2023 0:04:32', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(3, 'randomchik', '21.09.2023 0:09:47', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(4, 'randomchik', '21.09.2023 0:12:17', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(5, 'randomchik', '21.09.2023 0:25:35', 'Добавил водителя в базу данных: Жидович Алексей Геннадьевич.'),
	(6, 'randomchik', '21.09.2023 0:31:32', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(7, 'randomchik', '21.09.2023 1:11:10', 'Добавил водителя в базу данных: Жидович Савелий Але.'),
	(8, 'randomchik', '21.09.2023 1:35:15', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(9, 'randomchik', '21.09.2023 1:39:40', 'Добавил водителя в базу данных: Жидович Савелий Алексеевич.'),
	(10, 'randomchik', '21.09.2023 15:35:04', 'Добавил водителя в базу данных: Жидович Савелий Геннадьевич #2.'),
	(11, 'randomchik', '21.09.2023 15:44:35', 'Обновил данные на водителя: Жидович Савелий Геннадьевич #2.'),
	(12, 'randomchik', '21.09.2023 15:48:14', 'Удалил водителя Жидович Савелий Алексеевич #1.'),
	(13, 'randomchik', '21.09.2023 17:09:29', 'Добавил водителя в базу данных: Петров Петр Петрович #3.'),
	(14, 'randomchik', '21.09.2023 18:48:31', 'Добавил транспорт в базу данных: Iveco #1.'),
	(15, 'randomchik', '21.09.2023 18:59:28', 'Добавил транспорт в базу данных: Iveco #2.'),
	(16, 'randomchik', '21.09.2023 19:00:44', 'Добавил транспорт в базу данных: Scania #3.'),
	(17, 'randomchik', '24.09.2023 17:38:56', 'Добавил транспорт в базу данных: Scania #4.'),
	(18, 'randomchik', '24.09.2023 18:01:57', 'Добавил транспорт в базу данных: Iveco #5.'),
	(19, 'randomchik', '24.09.2023 18:16:59', 'Изменил данные о транспорте: DAF #5.'),
	(20, 'randomchik', '24.09.2023 18:17:58', 'Изменил данные о транспорте: Scania #5.'),
	(21, 'randomchik', '27.09.2023 1:29:03', 'Добавил компанию в базу данных: ООО Малекс #1.'),
	(22, 'randomchik', '27.09.2023 1:48:06', 'Удалил компанию с базы данных ООО Малекс #1.'),
	(23, 'randomchik', '27.09.2023 13:59:05', 'Добавил компанию в базу данных: ooo malex #2.'),
	(24, 'randomchik', '27.09.2023 13:59:53', 'Удалил компанию с базы данных ooo malex #2.'),
	(25, 'randomchik', '27.09.2023 14:13:00', 'Добавил компанию в базу данных: ASDASD #3.'),
	(26, 'randomchik', '27.09.2023 14:25:46', 'Добавил компанию в базу данных: ranodm #4.'),
	(27, 'randomchik', '27.09.2023 14:33:39', 'Изменил данные компании: ranodmasdasd #-1.'),
	(28, 'randomchik', '27.09.2023 14:36:30', 'Изменил данные компании: ranodm #4.'),
	(29, 'randomchik', '13.02.2024 10:59:37', 'Изменил данные о транспорте: Iveco #5.'),
	(30, 'randomchik', '20.02.2024 12:18:48', 'Добавил компанию в базу данных: sdasd #5.'),
	(31, 'randomchik', '11.04.2024 8:51:53', 'Изменил данные компании: sdasd #5.'),
	(32, 'randomchik', '11.04.2024 9:03:38', 'Удалил транспорт с базы данных TruckTractor #5.'),
	(33, 'randomchik', '11.04.2024 9:05:04', 'Добавил транспорт в базу данных: Iveco #6.'),
	(34, 'randomchik', '11.04.2024 9:05:42', 'Добавил транспорт в базу данных: DAF #7.'),
	(35, 'randomchik', '11.04.2024 9:06:26', 'Добавил транспорт в базу данных: Scania #8.'),
	(36, 'randomchik', '11.04.2024 9:07:09', 'Добавил транспорт в базу данных: Sitrak #9.'),
	(37, 'randomchik', '11.04.2024 9:07:46', 'Добавил транспорт в базу данных: Mercedes #10.'),
	(38, 'randomchik', '11.04.2024 9:29:16', 'Добавил компанию в базу данных: Fadexs #6.'),
	(39, 'randomchik', '11.04.2024 9:33:08', 'Добавил компанию в базу данных: TechSolutions #7.'),
	(40, 'randomchik', '11.04.2024 9:34:16', 'Добавил компанию в базу данных: GlobalTrade #8.'),
	(41, 'randomchik', '11.04.2024 9:48:15', 'Добавил компанию в базу данных: GreenTech Innovations #9.'),
	(42, 'randomchik', '11.04.2024 9:50:19', 'Добавил компанию в базу данных: MediCare Solutions #10.'),
	(43, 'randomchik', '03.06.2024 19:00:27', 'Изменил данные о грузе #2: \nИзменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8Изменил описание груза с  на asdasdasdИзменил стоимость груза с 123132 на 1231321 !'),
	(44, 'randomchik', '03.06.2024 19:03:46', 'Изменил данные о грузе #2: \nИзменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8Изменил стоимость груза с 123132 на 123132132 !'),
	(45, 'randomchik', '03.06.2024 19:04:51', 'Изменил данные о грузе #2: \nИзменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8 !'),
	(46, 'randomchik', '03.06.2024 19:05:04', 'Изменил данные о грузе #2: \nИзменил транспорт с TruckTractor-Sitrak #9 на TruckTractor-Sitrak #9Изменил водителя с Иванов Иван Иванович #4 на Иванов Иван Иванович #4Изменил компанию заказчик с GlobalTrade #8 на GlobalTrade #8 !'),
	(47, 'randomchik', '03.06.2024 19:08:27', 'Изменил данные о грузе #2: \nИзменил стоимость груза с 123132132 на 1231321323 !'),
	(48, 'randomchik', '03.06.2024 19:11:21', 'Изменил данные о грузе #2: \nИзменил стоимость груза с 1.231.321.323₽ на 5.000₽ !'),
	(49, 'randomchik', '21.06.2024 12:22:48', 'Создал заявку на выполнение груза !'),
	(50, 'randomchik', '21.06.2024 12:35:55', 'Добавил компанию в базу данных: Brabus #11.'),
	(51, 'randomchik', '27.01.2025 17:58:05', 'Добавил компанию в базу данных: eaesd #12.'),
	(52, 'randomchik', '27.01.2025 17:58:45', 'Создал заявку на выполнение груза !'),
	(53, 'randomchik', '27.01.2025 17:59:33', 'Изменил данные о грузе #4: \nИзменил название груза с weasd на weasdasdИзменил адрес отправки груза с sdfasdasd на sdfasdasdasd !'),
	(54, 'randomchik', '20.02.2025 15:48:06', 'Создал заявку на выполнение груза !'),
	(55, 'randomchik', '20.02.2025 16:04:26', 'Создал заявку на выполнение груза !'),
	(56, 'randomchik', '21.02.2025 14:55:27', 'Добавил транспорт в базу данных: Mercedes #11.'),
	(57, 'randomchik', '21.02.2025 15:20:29', 'Создал заявку на выполнение груза !'),
	(58, 'randomchik', '21.02.2025 16:11:36', 'Создал заявку на выполнение груза !'),
	(59, 'randomchik', '21.02.2025 16:21:30', 'Создал заявку на выполнение груза !'),
	(60, 'randomchik', '21.02.2025 16:21:59', 'Создал заявку на выполнение груза !'),
	(61, 'randomchik', '21.02.2025 16:24:15', 'Создал заявку на выполнение груза !'),
	(62, 'randomchik', '21.02.2025 16:27:41', 'Создал заявку на выполнение груза !'),
	(63, 'randomchik', '21.02.2025 16:47:25', 'Создал заявку на выполнение груза !'),
	(64, 'randomchik', '22.02.2025 15:39:50', 'Создал заявку на выполнение груза !'),
	(65, 'randomchik', '04.03.2025 15:23:58', 'Создал заявку на выполнение груза !'),
	(66, 'randomchik', '10.03.2025 14:49:38', 'Создал заявку на выполнение груза !'),
	(67, 'randomchik', '11.03.2025 15:16:16', 'Создал заявку на выполнение груза !'),
	(68, 'randomchik', '10.04.2025 14:26:31', 'Создал заявку на выполнение груза !');

-- Дамп структуры для таблица gruzomaster.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Login` text DEFAULT NULL,
  `Password` text DEFAULT NULL,
  `UserType` int(11) DEFAULT NULL,
  `Name` text DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы gruzomaster.users: ~0 rows (приблизительно)
INSERT INTO `users` (`id`, `Login`, `Password`, `UserType`, `Name`) VALUES
	(1, 'randomchik', '96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e', 1, 'Савелий'),
	(2, 'random', '96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e', 2, 'Артем'),
	(3, 'test', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 0, 'Test');

-- Дамп структуры для триггер gruzomaster.after_cargo_insert
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ZERO_IN_DATE,NO_ZERO_DATE,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER after_cargo_insert
AFTER INSERT ON cargo
FOR EACH ROW
BEGIN
    -- Вставка записи в таблицу cargo_log после успешной вставки в cargo
    INSERT INTO cargo_log (cargo_id, action, action_date)
    VALUES (NEW.ID, 'INSERT', NOW());
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Дамп структуры для представление gruzomaster.cargo_view
-- Удаление временной таблицы и создание окончательной структуры представления
DROP TABLE IF EXISTS `cargo_view`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `cargo_view` AS SELECT * FROM `cargo` ;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
