<?php
$ACH_XUID=@$_GET['ACHXUID'];


$NumberoFHistory=@$_GET['NumberoFHistory'];
$NumberoFActivity=@$_GET['NumberoFActivity'];
    /*Json Files */
    $newfilename =   "$ACH_XUID.json";

    $local_pathFeed = "/var/www/html/XBLIO/activity/feed/";
    $local_pathHistory = "/var/www/html/XBLIO/activity/history/";


if(isset($NumberoFActivity)) {

    $jsondata = file_get_contents($local_pathFeed.$newfilename);
    $json = json_decode($jsondata, true);
    $Number = $json['numItems'];
    
    echo $Number;
    
}

if(isset($NumberoFHistory)) { 

    $jsondata = file_get_contents($local_pathHistory.$newfilename);
    $json = json_decode($jsondata, true);
    $Number =  $json['numItems'];
    echo $Number;
  
}
?>