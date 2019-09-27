<?php
if ( !empty( $_POST ) ){
        $hostname = 'localhost';
		$username = 'id1975908_admin';
		$password = 'admin';
		$database = 'id1975908_moodbusters';
		
	$db = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
	
	$form = $_POST;
	$mood = $form[ 'mood' ];
	$date_time= $form[ 'date_time' ];
	$location= $form[ 'location' ];
	$sql = "INSERT INTO user_data (mood, date_time, location) VALUES ( :mood, :date_time, :location)";
	
	$query = $db->prepare( $sql );
	$query->execute( array( ':mood'=>$mood, ':date_time'=>$date_time, ':location'=>$location ) );
}
?>