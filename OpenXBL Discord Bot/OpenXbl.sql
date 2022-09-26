
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";



CREATE TABLE `OpenXbl` (
  `id` int(11) NOT NULL,
  `CPUKEY` varchar(255) NOT NULL,
  `APIKEY` varchar(255) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;



CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `cpu` varchar(50) DEFAULT NULL,
  `Discord` varchar(32) NOT NULL DEFAULT 'Unknown',
  `Discordid` varchar(32) NOT NULL DEFAULT '0',
  `Username` varchar(32) NOT NULL DEFAULT 'SilentIsYourHero',
  `Email` varchar(32) NOT NULL DEFAULT 'silentismydaddy@hookah.com'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);


ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;


ALTER TABLE `OpenXbl`
  ADD PRIMARY KEY (`id`);


ALTER TABLE `OpenXbl`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;


