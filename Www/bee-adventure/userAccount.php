<?php
mysql_connect("localhost","root","");
mysql_select_db("unity");
/**
 * DISABLED
 
if($_REQUEST['action']=="show_useraccount") {
	$query = "SELECT * FROM `highscores` ORDER BY `score` DESC";
	$result = mysql_query($query);
	while($array = mysql_fetch_array($result)) {
		echo $array['name']."</next>";
		echo $array['score']."</next>";
	}
}

 *
 **/
if($_REQUEST['action']=="submit_useraccount") {
	$username = $_REQUEST['username'];
	$password = $_REQUEST['password'];
	$email = $_REQUEST['email'];
	$query = "INSERT INTO `useraccounts` (`username`,`password`,`email`) VALUES ('$username','$password','$email')";
	mysql_query($query);
}
?>