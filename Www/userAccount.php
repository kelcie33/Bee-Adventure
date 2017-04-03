<?php
// CONNECTIONS ========
$host = "localhost"; //put your host here
$user = "root"; //in general is root
$password = ""; //use your password here
$dbname = "bee_adventure"; //your database
// MYSQL ========
mysqli_connect($host, $user, $password) or die("Cant connect into database");
mysqli_select_db($dbname)or die("Cant connect into database");
if($_REQUEST['action']=="show_useraccount") {
	$username = $_REQUEST['username'];
	$query = "SELECT * FROM `useraccounts` WHERE `username` = $username";
	$result = mysqli_query($query);
	while($array = mysqli_fetch_array($result)) {
		echo $array['username']."</next>";
		echo $array['password']."</next>";
		echo $array['email']."</next>";
	}
}
if($_REQUEST['action']=="submit_useraccount") {
	$username = $_REQUEST['username'];
	$password = $_REQUEST['password'];
	$email = $_REQUEST['email'];
	$query = "INSERT INTO `useraccounts` (`username`,`password`,`email`) VALUES ('$username','$password','$email')";
	mysqli_query($query);
}
?>
