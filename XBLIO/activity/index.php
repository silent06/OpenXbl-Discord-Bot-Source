<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Activity Feed Page</title>
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="fontawesome/css/all.min.css">
    <link rel="stylesheet" href="css/templatemo-style.css">
    <!-- BootStrap CSS-->  
    <link rel="stylesheet" href="stylesheet.css">
    <link rel="icon" href="img/favicon.png" type="image/png" sizes="16x16">
    <canvas id=c></canvas>
    <link href="css/canvas.css" rel="stylesheet" type="text/css" media="all" />
	  <script type="text/javascript" src="js/neon.js"></script>
</head>

<?php
    if(!isset($_SESSION))
    {
        session_start();
    }
    include('../config.php'); 
    include('../stealth/sql/Conn.php'); 
    //stealth\sql
    require __DIR__ . '/../vendor/autoload.php';
    use \OpenXBL\Api;
    
     /*GET varibles*/
    $API_KEY=@$_GET['APIKEY'];
    $ACH_XUID=@$_GET['ACHXUID'];
    $ActivityFeed=@$_GET['ActivityFeed'];
    $ActivityHistory=@$_GET['ActivityHistory'];

    /*Json Files */
    $newfilename =   "$ACH_XUID.json";

    $local_pathFeed = $LocalVpsFolder."activity/feed/";
    $local_pathHistory = $LocalVpsFolder."activity/history/";



    if(isset($ActivityFeed)) {
    /*Json varibles*/ 
    $jsondata = file_get_contents($local_pathFeed.$newfilename);
    $json = json_decode($jsondata, true);
    
    }

    if(isset($ActivityHistory)) {
        /*Json varibles*/ 
        $jsondata = file_get_contents($local_pathHistory.$newfilename);
        $json = json_decode($jsondata, true);
        
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
                    <a class="nav-link nav-link-1 active" aria-current="page" href="index.php">Activity Feed</a>
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
    <!--div>class="tm-hero d-flex justify-content-center align-items-center" data-parallax="scroll" data-image-src="img/ab.jpg"</div-->
    <div>
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
            Activity Feed
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
          if(isset($ActivityFeed)) {
            if(!empty($json['activityItems'])) {


              $output2 ="<div><center><b style=color:red; text-align=center;><u> Activity Feed:  </u></center></div>";
              echo $output2;

              foreach($json['activityItems'] as $people): ?>
              <div class="col-12 col-md-4">
                <div class="card mb-5" style=" width: 50rem; background-color: black;">
                <div class="card-header text-white" style="background-color: #32EE3F;"> Feed </div>
                  <img class="card-img-top" src="<?php if(!empty($people['screenshotId']))  { echo "<b style=color:red;> ScreenShot of: <p><u> " .$people['contentTitle'].  " <span href= '".$people['screenshotThumbnail']." '> <img src= '".$people['screenshotThumbnail']."'  width=100 height=100 border=0 ></span></b></u></p> ";}  ?>" alt="<?php ; ?>">
                  <h2 class="h3 mb-0">
                    <?php echo "<dt>" .@$people['name']. "</dt>";?>                 
                  </h2>
                    <p class="lead">
                        <?php if(!empty($people['screenshotId'])) { echo "<b style=color:red;><u> Date: </u> "  .$people['date']. "</b>"; }?>
                    </p>

                    <p class="lead">
                        <?php if(!empty($people['screenshotId'])) { echo "<b style=color:red;> "  .$people['ugcCaption']. "</b>"; }?> 
                    </p>

                    <p class="lead">
                        <?php if(!empty($people['screenshotId'])) { echo "<b style=color:red;><u>Views: </u> "  .$people['numViews']. "</b>"; }?>
                    </p>

                    <?php if(@$people['postType'] == "Link") { ?>


                      <p class="lead">
                        <?php echo "<b style=color:red;> Club Post: <p><u> " .$people['description'].  " <span href= '".$people['itemImage']." '> <img src= '".$people['itemImage']."'  width=100 height=100 border=0 ></span></b></u></p> " ; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<b style=color:red;><u> Number of LIkes: </u> "  .$people['numLikes']. "</b>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<b style=color:red;><u> Number of Comments: </u> "  .$people['numComments']. "</b>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<b style=color:red;><u> Number of Views: </u> "  .$people['numViews']. "</b>"; ?>
                      </p>

                    <?php } else { ?>

                      <p class="lead">
                        <?php echo "<b style=color:red;><u> " .@$people['contentTitle'].  " <span href= '".@$people['contentImageUri']." '> <img src= '".$people['contentImageUri']."'  width=100 height=100 border=0 ></span></b></u> "; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<dt><b style=color:red; ><u> Title ID:  </u>"  .@$people['titleId']. "</b> </dt>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<dt><b style=color:red; ><u> Achievements Earned Date: </u>"  .@$people['date']. "</b></dt>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<dt><b style=color:red; ><u> Description:  </u>"  .$people['description']. "</b> </dt>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<dt><b style=color:red;><u> Author: </u> "  .@$people['authorInfo']['modernGamertag']. "</b</dt>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<dt><b style=color:red;><u> Score Earned: </u> "  .@$people['gamerscore']. "</b></dt>"; ?>
                      </p>

                      <p class="lead">
                        <?php echo "<dt><a><span href= '".@$people['authorInfo']['imageUrl']." '> <img src= '".@$people['authorInfo']['imageUrl']."' width=100 height=100 border=0 />  </span></a></dt>"; ?>
                      </p>
                      <?php } ?>

                </div>
              </div>
        <?php endforeach; 
        } else {
          echo "Sorry Unable to find records?? :(";
        }
        } ?>


        <?php  
          if(isset($ActivityHistory)) {
            if(!empty($json['activityItems'])) {

              $output2 ="<div><center><b style=color:red; text-align=center;><u> Activity History:  </u></center></div>";
              echo $output2;

              foreach($json['activityItems'] as $people): ?>
              <div class="col-12 col-md-4">
                <div class="card mb-5" style=" width: 50rem; background-color: black;">
                <div class="card-header text-white" style="background-color: #32EE3F;"> Feed </div>
                  <img class="card-img-top" src="<?php echo $people['contentImageUri'] ;?>"  alt="<?php ; ?>">
                  <h2 class="h3 mb-0">
                    <?php echo "<dt><b style=color:red;>" .$people['contentTitle']. "</b></dt>";?>                 
                  </h2>
                    <p class="lead">
                      <?php echo "<b style=color:red;><u> " .$people['itemText'].  " <span href= '".$people['itemImage']." '> <img src= '".$people['itemImage']."'  width=100 height=100 border=0 ></span></b></u>" ; ?> 
                    </p>

                    <p class="lead">
                      <?php echo "<dt><b style=color:red; ><u> Title ID:  </u>"  .$people['titleId']. "</b> </dt>";?> 
                    </p>

                    <p class="lead">
                      <?php echo "<dt><b style=color:red; ><u> Achievements Earned Date: </u>"  .$people['date']. "</b></dt>"; ?> 
                    </p>

                    <p class="lead">
                      <?php echo "<dt><b style=color:red;><u> Author: </u> "  .$people['authorInfo']['modernGamertag']. "</b</dt>"; ?> 
                    </p>

                    <p class="lead">
                      <?php echo "<dt><b style=color:red;><u> Score Earned: </u> "  .$people['gamerscore']. "</b></dt>"; ?> 
                    </p>

                    <p class="lead">
                      <?php echo "<dt><a><span href= '".$people['authorInfo']['imageUrl']." '> <img src= '".$people['authorInfo']['imageUrl']."' width=100 height=100 border=0 />  </span></a></dt>"; ?> 
                    </p>

                </div>
              </div>
        <?php endforeach; 

        } else {
          echo "Sorry Unable to find records?? :(";
        }
        } ?>


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