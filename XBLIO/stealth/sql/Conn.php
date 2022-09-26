<?php
//MySQLI
$conn = mysqli_connect("root.silent.hosted.nfoservers.com", "silentwebhost", "", "silentwebhost_OpenXBL");
if (!$conn) {
	die("Connection failed: " . mysqli_connect_error());
}
?>