<?php 
if ( !empty( $_POST ) ){
        $hostname = 'localhost';
		$username = 'id1975908_admin';
		$password = 'admin';
		$database = 'id1975908_moodbusters';
		
        $db = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
			
		$form = $_POST;
		$id = $form[ 'id' ];	
			
			
		$stmt = $db->prepare('SELECT * FROM user_data WHERE id=?');
		$stmt->bindParam(1, $id, PDO::PARAM_INT);
		$stmt->execute();
		$row = $stmt->fetch(PDO::FETCH_ASSOC);

		if( ! $row)
		{
			die('nothing found');
		}	
		else
		{
			echo $row['id'], "; ", $row['mood'], "; ", $row['date_time'], "; ", $row['location'];
		}
}	
?>