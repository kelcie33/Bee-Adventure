<?php
// CONNECTIONS ========
$host = "vergil.u.washington.edu"; //put your host here
$user = "guest"; //in general is root
$password = ""; //use your password here
$port = "33999";
$dbname = "bee_adventure"; //your database
// MYSQL ========
$conn = mysqli_connect($host, $user, $password, null, $port) or die("Cant connect into database");
mysqli_select_db($conn, $dbname)or die("Cant connect into database");
if($_REQUEST['action']=="show_user_account") {
	$username = $_REQUEST['username'];
	$query = "SELECT * FROM `user_accounts` WHERE `username` = $username";
	$result = mysqli_query($query);
	while($array = mysqli_fetch_array($result)) {
		echo $array['username']."</next>";
		echo $array['password']."</next>";
		echo $array['email']."</next>";
	}
}
if($_REQUEST['action']=="submit_user_account") {
	$username = $_REQUEST['username'];
	$password = $_REQUEST['password'];
	$email = $_REQUEST['email'];
	$query = "INSERT INTO `user_accounts` (`username`,`password`,`email`) VALUES ('$username','$password','$email')";
	mysqli_query($query);
}
?>
