-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-09-2023 a las 07:52:59
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `Id_Contrato` int(11) NOT NULL,
  `Fecha_Inicio` date NOT NULL,
  `Fecha_Fin` date NOT NULL,
  `Monto` decimal(10,0) NOT NULL,
  `Id_Inmueble` int(11) NOT NULL,
  `Id_Inquilino` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`Id_Contrato`, `Fecha_Inicio`, `Fecha_Fin`, `Monto`, `Id_Inmueble`, `Id_Inquilino`) VALUES
(1, '2023-06-01', '2024-01-01', 250000, 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `Id_Inmueble` int(11) NOT NULL,
  `Direccion` varchar(150) NOT NULL,
  `Id_Uso` int(11) NOT NULL,
  `Id_Tipo` int(11) NOT NULL,
  `Ambientes` int(11) NOT NULL,
  `Latitud` decimal(10,0) NOT NULL,
  `Longitud` decimal(10,0) NOT NULL,
  `Precio` decimal(10,0) NOT NULL,
  `Activo` tinyint(1) NOT NULL,
  `Id_Propietario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`Id_Inmueble`, `Direccion`, `Id_Uso`, `Id_Tipo`, `Ambientes`, `Latitud`, `Longitud`, `Precio`, `Activo`, `Id_Propietario`) VALUES
(1, 'Depto1', 2, 4, 4, 11, 12, 150000, 1, 1),
(2, 'Casa1', 2, 3, 6, 22, 21, 235000, 1, 2),
(3, 'Local1', 1, 1, 2, 33, 31, 478000, 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `Id_Inquilino` int(11) NOT NULL,
  `Apellido` varchar(150) NOT NULL,
  `Nombre` varchar(150) NOT NULL,
  `Dni` varchar(10) NOT NULL,
  `Telefono` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`Id_Inquilino`, `Apellido`, `Nombre`, `Dni`, `Telefono`) VALUES
(1, 'Perez', 'Susana', '25789321', '2664781435'),
(2, 'Carlos', 'Gomez', '48966123', '2664786954');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id_Propietario` int(11) NOT NULL,
  `Apellido` varchar(150) NOT NULL,
  `Nombre` varchar(150) NOT NULL,
  `Dni` varchar(10) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Mail` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id_Propietario`, `Apellido`, `Nombre`, `Dni`, `Telefono`, `Mail`) VALUES
(1, 'Miranda', 'Valeria', '35888888', '2664658749', 'v@mail.com'),
(2, 'Nimoy', 'Leonard', '10456789', '2664321654', 'ln@mail.com');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipos`
--

CREATE TABLE `tipos` (
  `Id_Tipo` int(11) NOT NULL,
  `Nombre` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipos`
--

INSERT INTO `tipos` (`Id_Tipo`, `Nombre`) VALUES
(1, 'Local'),
(2, 'Depósito'),
(3, 'Casa'),
(4, 'Departamento');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usos`
--

CREATE TABLE `usos` (
  `Id_Uso` int(11) NOT NULL,
  `Nombre` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usos`
--

INSERT INTO `usos` (`Id_Uso`, `Nombre`) VALUES
(1, 'Comercial'),
(2, 'Residencial');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`Id_Contrato`),
  ADD KEY `Id_Inmueble` (`Id_Inmueble`,`Id_Inquilino`),
  ADD KEY `Id_Inquilino` (`Id_Inquilino`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`Id_Inmueble`),
  ADD KEY `Id_Uso` (`Id_Uso`,`Id_Tipo`,`Id_Propietario`),
  ADD KEY `Id_Tipo` (`Id_Tipo`),
  ADD KEY `Id_Propietario` (`Id_Propietario`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`Id_Inquilino`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id_Propietario`);

--
-- Indices de la tabla `tipos`
--
ALTER TABLE `tipos`
  ADD PRIMARY KEY (`Id_Tipo`);

--
-- Indices de la tabla `usos`
--
ALTER TABLE `usos`
  ADD PRIMARY KEY (`Id_Uso`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id_Contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id_Inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id_Inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id_Propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `tipos`
--
ALTER TABLE `tipos`
  MODIFY `Id_Tipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usos`
--
ALTER TABLE `usos`
  MODIFY `Id_Uso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`Id_Inquilino`) REFERENCES `inquilinos` (`Id_Inquilino`) ON UPDATE CASCADE,
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`Id_Inmueble`) REFERENCES `inmuebles` (`Id_Inmueble`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`Id_Uso`) REFERENCES `usos` (`Id_Uso`) ON UPDATE CASCADE,
  ADD CONSTRAINT `inmuebles_ibfk_2` FOREIGN KEY (`Id_Tipo`) REFERENCES `tipos` (`Id_Tipo`) ON UPDATE CASCADE,
  ADD CONSTRAINT `inmuebles_ibfk_3` FOREIGN KEY (`Id_Propietario`) REFERENCES `propietarios` (`Id_Propietario`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
