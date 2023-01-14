<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Achievements Page</title>
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="fontawesome/css/all.min.css">
    <link rel="stylesheet" href="css/templatemo-style.css">
    <!-- BootStrap CSS-->  
    <link rel="stylesheet" href="stylesheet.css">
    <link rel="icon" href="img/favicon.png" type="image/png" sizes="16x16">
</head>


<?php

    if(!isset($_SESSION))
    {
        session_start();
    }
    include('../config.php'); 
    include('../stealth/sql/Conn.php'); 
    require __DIR__ . '/../vendor/autoload.php';
    use \OpenXBL\Api;
    
     /*GET varibles*/
    $API_KEY=@$_GET['APIKEY'];
    $ACH_XUID=@$_GET['ACHXUID'];
    $achievements=@$_GET['achievements'];
    $achievementslist=@$_GET['achievementslist'];
    $achievementsSpecificlist=@$_GET['achievementsSpecificlist'];
    $gameachievementshistory=@$_GET['gameachievementshistory'];
    $achievementstats=@$_GET['achievementstats'];

    /*Json Files */
    $newfilename =   "$ACH_XUID.json";
    $newfilename2 =   "SpecificGameAchievements.json";
    $newfilename3 =   "achievements.json";
    $newfilename4 =   "achievementstats.json";
    $local_path = $LocalVpsFolder."ach/files/";


    if(isset($achievementslist)) {
    /*Json varibles*/ 
    $jsondata = file_get_contents($local_path.$newfilename);
    $json = json_decode($jsondata, true);
    
    }

    if(isset($achievementsSpecificlist)) {
    $jsondata2 = file_get_contents($local_path.$newfilename2);
    $json2 = json_decode($jsondata2, true);

    }

    if(isset($achievements)) {
    $jsondata3 = file_get_contents($local_path.$newfilename3);
    $json3 = json_decode($jsondata3, true);

    }


    if(isset($achievementstats)) {
      $jsondata4 = file_get_contents($local_path.$newfilename4);
      $json4 = json_decode($jsondata4, true);
  
      }


      $CPUKEYForStats = @$_GET["CPUKEYForStats"];
      $APIKEYForStats;
      /*Get API Key */
      if(isset($CPUKEYForStats)) {
          $sql = "SELECT * FROM `OpenXbl` WHERE `CPUKEY`='" . $CPUKEYForStats . "' LIMIT 1";
          $result = $conn->query($sql);
          if ($result->num_rows > 0) 
          {
              while($row = $result->fetch_assoc())
              {		        
                $APIKEYForStats=($row["APIKEY"]);
                
            }
          }
          else   
          {
            echo "Not Registered";
          }
      }
?>


<body>
    <!-- Page Loader -->
    <div id="loader-wrapper">
        <div id="loader"></div>
        <div class="first"></div>
        <div class="loader-section section-left"></div>
        <div class="loader-section section-right"></div>

    </div>
    <nav class="navbar navbar-expand-lg">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">
                <i class="fas fa-film mr-2"></i>
                OpenXBL Bot Web Page
            </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <i class="fas fa-bars"></i>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav ml-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a class="nav-link nav-link-1 active" aria-current="page" href="index.php">Achievements</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link nav-link-3" href="about.php">About</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link nav-link-4" href="contact.php">Contact</a>
                </li>
            </ul>
            </div>
        </div>
    </nav>

    <div class="tm-hero d-flex justify-content-center align-items-center" data-parallax="scroll" data-image-src="img/ab.jpg">
        <form class="d-flex tm-search-form">
            <input class="form-control tm-search-input" type="search" placeholder="Search" aria-label="Search">
            <button class="btn btn-outline-success tm-search-btn" type="submit">
                <i class="fas fa-search"></i>
            </button>
        </form>
    </div>

    <div class="container-fluid tm-container-content tm-mt-60">
        <div class="row mb-4">
            <h2 class="col-6 tm-text-primary">
                Achievement List
            </h2>
            <div class="col-6 d-flex justify-content-end align-items-center">
                <form action="" class="tm-text-primary">
                    Page <input type="text" value="1" size="1" class="tm-input-paging tm-text-primary"> of 200
                </form>
            </div>
        </div>
        <div class="row tm-mb-90 tm-gallery">
            
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12 mb-5">
                <figure class="effect-ming tm-video-item">
                

                     
                     
                    <figcaption class="d-flex align-items-center justify-content-center">

                    
                    </figcaption>                    
                </figure>
                <div class="d-flex justify-content-between tm-text-gray">

                </div>
            </div>

            <div class="container py-5">
  <div class="row">
    
        <?php  
          if(isset($achievements))  {
            if(!empty($json3['titles'])) {
              foreach($json3['titles'] as $people): ?>
              <div class="col-12 col-md-4">
                <div class="card mb-5" style="width: 18rem;">
                  <img class="card-img-top" src="<?php echo $people['displayImage'];?>" alt="<?php echo $people['name']; ?>">
                  <h2 class="h3 mb-0">
                    <?php echo " <dt><b><u> Title ID: </b></u>"  .$people['titleId']. "</dt> ";?>                 
                  </h2>
                    <p class="lead">
                      <?php echo " <dt><b><u> Achievements Earned: </b></u>"  .$people['achievement']['currentAchievements']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Total Achievements: </b></u> "  .$people['achievement']['totalAchievements']. "</dt>"; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Current Score: </b></u> "  .$people['achievement']['currentGamerscore']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Total Score: </b></u> "  .$people['achievement']['totalGamerscore']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Player Progress: </b></u>"  .$people['achievement']['progressPercentage']. "%</dt> "; ?> 
                    </p>

                </div>
              </div>
        <?php endforeach; 
        } else {

          echo "Sorry Unable to find your title records?? :(";
        }
        } ?>



        <?php  
          if(isset($achievementslist))  {
            if(!empty($json['titles'])) {
              foreach($json['titles'] as $people): ?>
              <div class="col-12 col-md-4">
                <div class="card mb-5" style="width: 18rem;">
                  <img class="card-img-top" src="<?php echo $people['displayImage'];?>" alt="<?php echo $people['name']; ?>">
                  <h2 class="h3 mb-0">
                    <?php echo " <dt><b><u> Title ID: </b></u>"  .$people['titleId']. "</dt> ";?>                 
                  </h2>
                    <p class="lead">
                      <?php echo " <dt><b><u> Achievements Earned: </b></u>"  .$people['achievement']['currentAchievements']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Total Achievements: </b></u> "  .$people['achievement']['totalAchievements']. "</dt>"; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Current Score: </b></u> "  .$people['achievement']['currentGamerscore']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Total Score: </b></u> "  .$people['achievement']['totalGamerscore']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Player Progress: </b></u>"  .$people['achievement']['progressPercentage']. "%</dt> "; ?> 
                    </p>

                </div>
              </div>
        <?php endforeach; 
        } else {

          echo "Sorry Unable to find your title records?? :(";
        }
        } ?>



        <?php  
          if(isset($achievementsSpecificlist))  {
            if(!empty($json2['achievements'])) {
              foreach($json2['achievements'] as $people): ?>
              <div class="col-12 col-md-4">
                <div class="card mb-5" style="width: 18rem;">
                  <img class="card-img-top" src="<?php echo $people['mediaAssets'][0]['url'];?>" alt="">
                  <h2 class="h3 mb-0">
                    <?php echo " <dt><b style=color:red;><u> Achievement Name: </b></u>"  .$people['name']. "</dt> ";?>                 
                  </h2>
                    <p class="lead">
                      <?php echo " <dt><b><u> ProgessSate: </b></u>"  .$people['progressState']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Time Unlocked: </b></u>"  .$people['progression']['timeUnlocked']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php $iSsecert = $people['isSecret'] ? "true":"false"; echo " <dt><b><u> isSecret?: </b></u> "  .$iSsecert. "</dt>"; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Description: </b></u> "  .$people['description']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> AchievementType: </b></u> "  .$people['achievementType']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Rare?: </b></u>"  .$people['rarity']['currentCategory']. "</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> Rare Percentage: </b></u>"  .$people['rarity']['currentPercentage']. "%</dt> "; ?> 
                    </p>

                    <p class="lead">
                      <?php echo " <dt><b><u> GameScore: </b></u>"  .$people['rewards'][0]['value']. "</dt> "; ?> 
                    </p>

                </div>
              </div>
                <?php
                  $TotalRecords = "<b><u>  Total Number of Achievements:" .$json2['pagingInfo']['totalRecords']. "</b></u>";
                  $Moreinfo = "<b><u> Showing only 150. If theirs a number here means there's more than 150 achievements. Holy shit!" .$json2['pagingInfo']['continuationToken']. "</b></u>";
                ?>
            <?php endforeach; 

            echo $TotalRecords;
            echo $output1;
            echo $Moreinfo;   
          } else {

            echo "Sorry Unable to find your title records?? :(";
          }
        } ?>

    <p><li><strong>
      <?php
      
        if(isset($achievementstats)) 
        {
          if(!empty($json4['groups']))
          {
      
              $xbox = new Api($APIKEYForStats);
              $data = $xbox->get('account');
              $newfilename = "profile.json";
              //$local_path = "/var/www/html/XBLIO/ach/files/";

              // Write the contents back to the file
              file_put_contents($local_path.$newfilename, $data);
              $jsondata5 = file_get_contents($local_path.$newfilename);
              $json5 = json_decode($jsondata5, true);
      
              $gamertag = $json5['profileUsers'][0]['settings'][2]['value'];
              $profilepicture = $json5['profileUsers'][0]['settings'][0]['value'];
       
              //echo "User Stats For: $gamertag";
              echo " <dt><b style=color:red;><u> " .$gamertag.  " <span href= '".$profilepicture." '> <img src= '".$profilepicture."'  width=300 height=300 border=0 ></span></b></u></dt>  " ;

              foreach ($json4['groups'] as $people) 
              {
                /*Gay way to do it for json but w/e */
                $output1 .= " <dt><b><u> Title ID: </b></u>"  .$people['titleid']. "</dt> ";
                $output1 .= " <dt> <u> Achievement Stats: </u> </dt> ";
                $output1 .= " <dt><b style=color:red;> " .$people['statlistscollection'][0]['stats'][0]['value'].   " </b> ".$people['statlistscollection'][0]['stats'][0]['groupproperties']['DisplayName']. " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][1]['value'].  " </b> ".$people['statlistscollection'][0]['stats'][1]['groupproperties']['DisplayName']. " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u> </u>" .$people['statlistscollection'][0]['stats'][2]['value'].  " </b> ".$people['statlistscollection'][0]['stats'][2]['groupproperties']['DisplayName'].  " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][3]['value'].  " </b> ".$people['statlistscollection'][0]['stats'][3]['groupproperties']['DisplayName']. " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][4]['value'].  " </b> ".$people['statlistscollection'][0]['stats'][4]['groupproperties']['DisplayName']. " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>   </u>" .$people['statlistscollection'][0]['stats'][5]['value'].  "</b>  ".$people['statlistscollection'][0]['stats'][5]['groupproperties']['DisplayName']. " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][6]['value'].  " </b> ".$people['statlistscollection'][0]['stats'][6]['groupproperties']['DisplayName'].  " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][7]['value'].  "</b> ".$people['statlistscollection'][0]['stats'][7]['groupproperties']['DisplayName']. " </dt> ";
                $output1 .= " <dt><b style=color:red;> <u> </u>" .$people['statlistscollection'][0]['stats'][8]['value'].  " </b> " .$people['statlistscollection'][0]['stats'][8]['groupproperties']['DisplayName']." </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][9]['value'].  "</b>  ".$people['statlistscollection'][0]['stats'][9]['groupproperties']['DisplayName']." </dt> ";
                $output1 .= " <dt><b style=color:red;> <u>  </u>" .$people['statlistscollection'][0]['stats'][10]['value'].  " </b> ".$people['statlistscollection'][0]['stats'][10]['groupproperties']['DisplayName']. " </dt> ";
                           
              }                         
                echo $output1;
                echo " <dt><b style=color:red;> Total Minutes Played: </b>"  .$json4['statlistscollection'][0]['stats'][0]['value']. "</dt> ";
  
          } 
          else {

            echo "Sorry Unable to find your title records?? :(";
          }
        }


      ?>
      </strong></li></p>  
    </div>          
</div>



        </div> <!-- row -->
        <div class="row tm-mb-90">
            <div class="col-12 d-flex justify-content-between align-items-center tm-paging-col">
                <a href="javascript:void(0);" class="btn btn-primary tm-btn-prev mb-2 disabled">Previous</a>
                <div class="tm-paging d-flex">
                    <a href="javascript:void(0);" class="active tm-paging-link">1</a>
                </div>
                <a href="javascript:void(0);" class="btn btn-primary tm-btn-next">Next Page</a>
            </div>            
        </div>
    </div> <!-- container-fluid, tm-container-content -->

    <footer class="tm-bg-gray pt-5 pb-3 tm-text-gray tm-footer">
        <div class="container-fluid tm-container-small">
            <div class="row">
                <div class="col-lg-6 col-md-12 col-12 px-5 mb-5">
                    <h3 class="tm-text-primary mb-4 tm-footer-title">About OpenXBL</h3>
                    <p> <a rel="sponsored" href="https://xbl.io/">OpenXBL</a>  is an unofficial Xbox Live API designed around developer friendly documentation. The best part, it's free!</p>
                    <p>OpenXBL <a rel="sponsored" href="https://discord.gg/x6kk8M2"> Join Discord</a></p>
                    <p>GitHub <a rel="sponsored" href="https://github.com/silent06/OpenXbl-Discord-Bot-Source">Get your OwN Bot!</a> </p>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6 col-12 px-5 mb-5">
                    <h3 class="tm-text-primary mb-4 tm-footer-title">Our Links</h3>
                    <ul class="tm-footer-links pl-0">
                        <li><a href="#">Support</a></li>
                        <li><a href="#">Contact</a></li>
                    </ul>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6 col-12 px-5 mb-5">
                    <ul class="tm-social-links d-flex justify-content-end pl-0 mb-5">

                    </ul>
                    <a href="#" class="tm-text-gray text-right d-block mb-2">Terms of Use</a>
                    <a href="#" class="tm-text-gray text-right d-block">Privacy Policy</a>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 col-md-7 col-12 px-5 mb-3">
                    Copyright 2022 SilentLive.gq All rights reserved.
                </div>
                <div class="col-lg-4 col-md-5 col-12 px-5 text-right">
                    Designed by <a href="https://silentLive.gq" class="tm-text-gray" rel="sponsored" target="_parent">-Silent</a>
                </div>
            </div>
        </div>
    </footer>
    
    <script src="js/plugins.js"></script>
    <script>
        $(window).on("load", function() {
            $('body').addClass('loaded');
        });
    </script>
</body>
</html>