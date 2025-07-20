-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ad
-- ------------------------------------------------------
-- Server version	8.0.42

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `container`
--

DROP TABLE IF EXISTS `container`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `container` (
  `ContainerID` int NOT NULL AUTO_INCREMENT,
  `ContainerNumber` varchar(50) NOT NULL,
  `ContainerType` varchar(50) NOT NULL,
  `Capacity` varchar(50) NOT NULL,
  `Status` varchar(45) DEFAULT 'Available',
  `Notes` varchar(255) NOT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `UpdatedAt` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ContainerID`),
  UNIQUE KEY `ContainerNumber` (`ContainerNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `container`
--

LOCK TABLES `container` WRITE;
/*!40000 ALTER TABLE `container` DISABLE KEYS */;
INSERT INTO `container` VALUES (1,'25546785','Top Off','50K','Available','','2025-07-17 17:52:48','2025-07-18 14:33:19'),(2,'5666871','Dry','50K','Available','','2025-07-19 01:21:42','2025-07-19 01:21:42'),(3,'125456578','Open Top','10K','Available','-','2025-07-20 03:55:54','2025-07-20 04:09:11');
/*!40000 ALTER TABLE `container` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer_registration`
--

DROP TABLE IF EXISTS `customer_registration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customer_registration` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Full_Name` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Address` varchar(45) NOT NULL,
  `DOB` date NOT NULL,
  `Gender` varchar(45) NOT NULL,
  `NIC` varchar(12) NOT NULL,
  `Martial_Status` varchar(45) NOT NULL,
  `Contact_Number` int NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Confirm_Password` varchar(45) NOT NULL,
  `RegisteredDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Confirm_Password_UNIQUE` (`Confirm_Password`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_registration`
--

LOCK TABLES `customer_registration` WRITE;
/*!40000 ALTER TABLE `customer_registration` DISABLE KEYS */;
INSERT INTO `customer_registration` VALUES (2,'Sithu','sithu@gmail.com','MeGalle','2000-07-16','Female','200545669776','UnMarried',756889636,'2002','2002','2025-07-18 15:50:40');
/*!40000 ALTER TABLE `customer_registration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emp_registration`
--

DROP TABLE IF EXISTS `emp_registration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emp_registration` (
  `EmpID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `DOB` date NOT NULL,
  `Gender` varchar(7) NOT NULL,
  `Address` varchar(200) NOT NULL,
  `NIC` varchar(12) NOT NULL,
  `Contact` int NOT NULL,
  `MartialStatus` varchar(45) NOT NULL,
  `JobRole` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `CPassword` varchar(45) NOT NULL,
  `HiredDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`EmpID`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emp_registration`
--

LOCK TABLES `emp_registration` WRITE;
/*!40000 ALTER TABLE `emp_registration` DISABLE KEYS */;
INSERT INTO `emp_registration` VALUES (1,'sithu','sithu@gmail.com','2000-07-09','Female','Galle','200278101227',764383920,'Married','Driver','sithu','sithu','2025-07-18 15:50:40'),(5,'Janaka D e Silva','janaka6@gmail.com','2000-07-20','Male','Megalle,Galle','5645213456',756487965,'Married','Driver','1969','1969','2025-07-20 03:58:51');
/*!40000 ALTER TABLE `emp_registration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job_status`
--

DROP TABLE IF EXISTS `job_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `job_status` (
  `SID` int NOT NULL AUTO_INCREMENT,
  `RID` int NOT NULL,
  `status` varchar(45) NOT NULL,
  `UpdatedAt` date NOT NULL,
  PRIMARY KEY (`SID`),
  KEY `fk_job_status_service_request` (`RID`),
  CONSTRAINT `fk_job_status_service_request` FOREIGN KEY (`RID`) REFERENCES `service_request` (`RID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job_status`
--

LOCK TABLES `job_status` WRITE;
/*!40000 ALTER TABLE `job_status` DISABLE KEYS */;
INSERT INTO `job_status` VALUES (1,2,'Completed','2025-07-17'),(2,2,'In Progress','2025-07-17'),(3,2,'Completed','2025-07-17'),(4,2,'Completed','2025-07-18'),(5,3,'In Progress','2025-07-18'),(6,4,'Completed','2025-07-18'),(7,3,'Completed','2025-07-18'),(8,5,'In Progress','2025-07-19'),(9,9,'Completed','2025-07-20'),(10,3,'--SELECT--','2025-07-20'),(11,3,'Completed','2025-07-20'),(12,8,'Completed','2025-07-20');
/*!40000 ALTER TABLE `job_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `load_assignment`
--

DROP TABLE IF EXISTS `load_assignment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `load_assignment` (
  `AssignmentID` int NOT NULL AUTO_INCREMENT,
  `RID` int NOT NULL,
  `VID` int NOT NULL,
  `DriverName` varchar(45) NOT NULL,
  `AssistantName` varchar(45) NOT NULL,
  `ContainerID` int NOT NULL,
  `AssignedDate` date NOT NULL,
  `Status` varchar(45) NOT NULL,
  PRIMARY KEY (`AssignmentID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `load_assignment`
--

LOCK TABLES `load_assignment` WRITE;
/*!40000 ALTER TABLE `load_assignment` DISABLE KEYS */;
INSERT INTO `load_assignment` VALUES (1,4,2,'sithu','Chamila',1,'2025-07-19','Assigned'),(2,5,2,'sithu','Chamila Subhashini',1,'2025-07-19','Assigned');
/*!40000 ALTER TABLE `load_assignment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `login` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Role` varchar(45) NOT NULL,
  `CPassword` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES (2,'Admin@gmail.com','1234','Admin','1234');
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `service_request`
--

DROP TABLE IF EXISTS `service_request`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `service_request` (
  `RID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int NOT NULL,
  `PickupLocation` varchar(45) NOT NULL,
  `DeliveryLocation` varchar(45) NOT NULL,
  `Date` date NOT NULL,
  `SpecialNote` varchar(200) NOT NULL,
  `Item_Type` varchar(45) NOT NULL,
  `Quantity` int NOT NULL,
  `Status` varchar(20) DEFAULT 'Pending',
  `ServiceDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`RID`),
  KEY `fk_service_request_customer` (`CustomerID`),
  CONSTRAINT `fk_service_request_customer` FOREIGN KEY (`CustomerID`) REFERENCES `customer_registration` (`ID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service_request`
--

LOCK TABLES `service_request` WRITE;
/*!40000 ALTER TABLE `service_request` DISABLE KEYS */;
INSERT INTO `service_request` VALUES (2,2,'Galle','Matara','2025-07-23','Be gentle','Chair',1,'Confirmed','2025-07-18 15:50:40'),(3,2,'Hambanthota','Galle','2025-08-20','Please be extra Care','Glasses',6,'Declined','2025-07-18 15:50:40'),(4,2,'Galle','Matara','2025-08-26','Gentle','Glasses',5,'Declined','2025-07-18 15:50:40'),(5,2,'Galle','Colombo','2025-07-24','Pls be gentle with the product','Glasses\r\nFurnitures',3,'Confirmed','2025-07-18 18:19:49'),(6,2,'no. 43/A, Ihalabomiriya, Kaduwela','No.251/7, J.E Perera Mawatha, Galle','2025-07-24','','Furnitures',5,'Confirmed','2025-07-19 00:45:18'),(7,2,'Megalle','Colombo','2025-07-24','-','Furnitures',7,'Confirmed','2025-07-19 11:03:14'),(8,2,'Galle','Hambanthota','2025-08-20','-','Furnitures',5,'Confirmed','2025-07-19 16:57:32'),(9,2,'No52/A, Matara','N0.23/1, Megalle, Galle','2025-07-20','-','Furnitures',10,'Confirmed','2025-07-20 03:42:06');
/*!40000 ALTER TABLE `service_request` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicle_registration`
--

DROP TABLE IF EXISTS `vehicle_registration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehicle_registration` (
  `VID` int NOT NULL AUTO_INCREMENT,
  `Vehicle_Number` varchar(45) NOT NULL,
  `OwnerContact` varchar(10) NOT NULL,
  `Vehicle_Type` varchar(45) NOT NULL,
  `Status` varchar(45) NOT NULL,
  `Note` varchar(45) DEFAULT NULL,
  `RegisteredDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`VID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicle_registration`
--

LOCK TABLES `vehicle_registration` WRITE;
/*!40000 ALTER TABLE `vehicle_registration` DISABLE KEYS */;
INSERT INTO `vehicle_registration` VALUES (2,'313131515','0745555555','Large Truck','Available','','2025-07-18 15:50:40'),(3,'48484899','0778888888','Mini Lorry','Pending','','2025-07-20 09:02:03');
/*!40000 ALTER TABLE `vehicle_registration` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-20 10:17:28
