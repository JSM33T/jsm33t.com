<?php
if (session_status() == PHP_SESSION_NONE) {
    session_start();
  }


if (!isset($_GET["mode"]))
{
    $mode = "day";
}
else
{
$mode = $_GET["mode"];
$_SESSION["s_mode"] = $mode;
}

?>
<!DOCTYPE html>
<html lang="en">
    <head>
        <title><?php echo $title?></title>
        <meta name="description" content="<?php echo $description ?>">    
        <meta charset="utf-8">
        <meta name="author" content="jsm33t via rhythm / themeforest">
        <!--[if IE]><meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1'><![endif]-->
        <?php if(!isset($meta_tags)){ $meta_tags = 'jsm33t ,Music, blogs, coder , bootlegs';} ?>
        <meta name="keywords" content="<?php echo $meta_tags?>">
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <?php if(!isset($fav_icon)){ $fav_icon = '/resources/images/fav_icon.png';} ?>
        <link rel="shortcut icon" href="<?php echo $fav_icon ?>">
		<link rel="stylesheet" href="/resources/css/maincur.css">
        <link rel="stylesheet" href="/resources/css/bootstrap.min.css">
        <link rel="stylesheet" href="/resources/css/style.css">
        <link rel="stylesheet" href="/resources/css/jsm33t.css">
        <link rel="stylesheet" href="/resources/css/style-responsive.css">
        <link rel="stylesheet" href="/resources/css/animate.min.css">
        <link rel="stylesheet" href="/resources/css/vertical-rhythm.min.css">
        <link rel="stylesheet" href="/resources/css/owl.carousel.css">
        <link rel="stylesheet" href="/resources/css/magnific-popup.css">
        <style>
<?php
  if($_SESSION["s_mode"] == "night")
  {
    echo " html{filter:brightness(80%) grayscale(100%) sepia(70%);}" ;  
  }
  else
  {
    echo " html{filter:brightness(100%);}" ;  
  }
  
?>
        </style>

    </head>
  
    <body class="appear-animate">
        <div class="page-loader">
            <div class="loader">Loading...</div>
        </div>
        <div class="header">
            <div class="progress-container">
                <div class="progress-bar" id="myBar"></div>
            </div>  
        </div>
        <a href="#main" class="btn skip-to-content">Skip to Content</a>
        <div class="page" id="top">
        <?php if(!isset($logo_visibility)) { $logo_visibility = 'd-none d-md-block d-lg-block';} ?>
            <div class="fm-logo-wrap local-scroll <?php echo $logo_visibility ?>">
                <a href="/" class="logo"><img src="/resources/images/logos/j_logo_<?php echo $logo ?>.svg" width="152" height="54" alt="" /></a>
            </div>
            <a href="#" class="fm-button"><span></span>Menu</a>
            <div class="fm-wrapper" id="fullscreen-menu">
                <div class="fm-wrapper-sub">
                    <div class="fm-wrapper-sub-sub">

        <?php 
            if(!isset($home)) { $home = 'href="/" ';}
            if(!isset($music)){ $music = 'href ="/music"' ;}
            if(!isset($gallery)){ $gallery = 'href="/gallery"';}
            if(!isset($repo)){ $repo = 'href="/repository"';}
            if(!isset($me)){ $me = 'href ="/me"' ;}
        ?>

                        <ul class="fm-menu-links local-scroll">
                        
                            <li>  <a <?php echo $home?> >Home</a>       </li>
                            <li>  <a <?php echo $music?> >Music</a>     </li>
                            <li>  <a <?php echo $gallery?> >GALLERY</a> </li>
                            <li>  <a <?php echo $me?> >ABOUT ME</a>     </li>
                            <li>  <a href="/retro" >R3TR0</a>           </li>
                            <li>
                                <a href="#" class="fm-has-sub">Search <i class="fa fa-search"></i></a>
                                <ul class="fm-sub">
                                    <li>
                                        <div class="mn-wrap">
                                            <form method="post" class="form align-center">
                                                <div class="search-wrap inline-block fm-search">             
                                                    <input type="text" class="form-control search-field round"
                                                        size="30" 
                                                        onkeyup="showResult(this.value)"
                                                        placeholder="Search..."
                                                    >
                                                    <div id="livesearch"></div>
                                                </div>
                                            </form>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        
                        <!--My Social Handles -->
                        <div class="fm-social-links offset-md-4 col-md-4 col-sm-12 col-xs-12" style="background: beige;border-radius:20px;">
                            
                            <a href="https://github.com/jsm33t" title="Behance" target="_blank"><i class="fa fa-github"></i></a>
                           <?php 
                          

                           if(!isset($_SESSION["s_mode"]))
                           {
                            $stat = "night";
                           }
                           else
                           {
                            if($_SESSION["s_mode"] == "night")
                            {
                             $stat = "day";
                            }
                          
                            if($_SESSION["s_mode"] == "day")
                            {
                             $stat = "night";
                            }
                           }
                          
                         
                           ?>
                            <a href="<?php echo strtok($_SERVER["REQUEST_URI"], '?')."?mode=".$stat; ?>" title="Spotify" ><i class="fa  fa-lightbulb-o" ></i></a> 
                        </div>
                    </div>
                </div>
                
            </div>
            <!-- End Menu Overlay  -->
