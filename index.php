<?php
   $title = "JSM33T";
   $description = "JSM33T | Personal website";
   $logo = "white";
   $home = ' href="/" onClick="return false;" class="active" ';
   $logo_visibility = " d-sm-block d-lg-block d-lg-block";
   $filters = " brightness(80%) grayscale(100%) sepia(70%)";
   include $_SERVER['DOCUMENT_ROOT'] . "/includes/head.php";
?>      


<main id="main" >
   <section class="home-section bg-dark-alfa-70 parallax-2" data-background="/resources/images/home.jpg" id="home">
      <div class="js-height-full container">
         <div class="home-content">
            <div class="home-text">
               <div class="font-alt mod-breadcrumbs">
                  <h3 class="hs-line-8 font-alt mb-50 mb-xs-30">
                     <center> <span class="d-lg-none d-md-none d-lg-inline-block">&nbsp;</span><span>HOME</span></center>
                  </h3>
               </div>
               <h2 class="hs-line-14 ind_14 font-alt mb-50 mb-xs-0">
                  <span class="text-rotate">
                     soundtracks,
                     Codes,
                     Archives,
                     People
                  </span>
               </h2>
               <span class="d-lg-none d-md-none d-lg-inline-block"><br></span>
               <div class="local-scroll">
                  <a href="https://www.youtube.com/watch?v=mTWvfrzzgTI" class="btn btn-mod btn-border-w btn-circle btn-medium lightbox mfp-iframe d-none d-sm-inline-block">PLAY REEL</a>
                  <span class="d-none d-sm-inline-block">&nbsp;</span>
                  <a  href="#about"  class="btn btn-mod btn-border-w btn-circle btn-medium ">SEE MORE</a>
               </div>
            </div>
         </div>
         <div class="local-scroll">
            <a href="#about" class="scroll-down"><i class="fa fa-angle-down scroll-down-icon"></i><span class="sr-only">Scroll to the next section</span></a>
         </div>
      </div>
   </section>
   <section class="page-section bg-light-alfa-90 parallax-9" data-background="/resources/images/home.jpg" id="about">
      <div class="container relative">
         <h2 class="section-title font-alt align-left mb-70 mb-sm-40">
            About Me
         </h2>
         <div class="section-text mb-50 mb-sm-20">
            <div class="row">
               <div class="col-md-4">
                  <blockquote>
                     <p>
                        Some random mental gymnastics quote from a random place
                     </p>
                     <footer>
                        Lorem Ipsum
                     </footer>
                  </blockquote>
               </div>
               <div class="col-md-4 col-sm-6 mb-sm-50 mb-xs-30 justify">
                  Jasmeet Singh a.k.a JSM33T is an electronic music producer from Jharsuguda, IN. known for his rendition and fusion of Bollywood with various genres which gained support from DJ nYk, Asad Gujral, DJ Richard, Akbar Sami and many more.
               </div>
               <div class="col-md-4 col-sm-6 mb-sm-50 mb-xs-30 justify">
                  His recent original "Melancholy" released worldwide and gained support from some great names from the industry including Angus Powel.
               </div>
            </div>
         </div>
      </div>
   </section>
   <!-- Call Action Section -->  
   <section class="page-section pt-0 pb-0 banner-section bg-dark-alfa-70" data-background="/resources/images/home.jpg">
      <div class="container relative">
         <div class="row">
            <div class="col-sm-6">
               <div class="mt-140 mt-lg-80 mb-140 mb-lg-80">
                  <div class="banner-content">
                     <h3 class="banner-heading font-alt">Did you check my latest composition??</h3>
                     <div class="banner-decription">
                        Melancholy , an original soundtrack , is out now on various platforms worldwide.
                     </div>
                     <div class="local-scroll">
                        <a href="/music/melancholy" class="btn btn-mod btn-w btn-medium btn-round">DOWNLOAD / STREAM</a>
                     </div>
                  </div>
               </div>
            </div>
            <div class="col-sm-6 banner-image wow fadeInUp">
               <img src="/resources/images/promo.png"" alt="" />
            </div>
         </div>
      </div>
   </section>

   <section class="page-section bg-light-alfa-90 parallax-9" data-background="/resources/images/home.jpg" id="about">
      <div class="container relative">
         <ul class="works-grid work-grid-3 work-grid-gut clearfix font-alt hover-white" id="work-grid">
            <li class="work-item mix wow fadeInLeft" data-wow-delay="0.3s">
               <a href="/music" class="work-ext-link">
                  <div class="work-img rcorner10">
                     <img src="/resources/images/home/home_thumbs/music.jpg" alt="Work" />
                  </div>
                  <div class="work-intro">
                     <h3 class="work-title">Music</h3>
                     <div class="work-descr">
                        -Jsm33t
                     </div>
                  </div>
               </a>
            </li>

            <li class="work-item mix wow fadeInLeft" data-wow-delay="0.2s">
               <a href="/repository" class="work-ext-link">
                  <div class="work-img rcorner10">
                     <img src="/resources/images/home/home_thumbs/repository.jpg" alt="Work" />
                  </div>
                  <div class="work-intro">
                     <h3 class="work-title">Repository</h3>
                     <div class="work-descr">
                        -Jsm33t
                     </div>
                  </div>
               </a>
            </li>
            <li class="work-item mix music wow fadeInLeft" data-wow-delay="0.1s">
               <a href="#" class="work-ext-link">
                  <div class="work-img rcorner10">
                     <img src="/resources/images/home/home_thumbs/playlists.jpg" alt="Work" />
                  </div>
                  <div class="work-intro">
                     <h3 class="work-title">Playlists</h3>
                     <div class="work-descr">
                        -Jsm33t
                     </div>
                  </div>
               </a>
            </li>
            <li class="work-item mix mobile wow fadeInRight" data-wow-delay="0.1s">
               <a href="/retro" class="work-ext-link">
                  <div class="work-img rcorner10">
                     <img src="/resources/images/home/home_thumbs/retro.jpg" alt="Work" />
                  </div>
                  <div class="work-intro">
                     <h3 class="work-title">Retro</h3>
                     <div class="work-descr">
                        -Jsm33t
                     </div>
                  </div>
               </a>
            </li>
            <li class="work-item mix mobile wow fadeInRight" data-wow-delay="0.2s">
               <a href="/gallery" class="work-ext-link">
                  <div class="work-img rcorner10">
                     <img src="/resources/images/home/home_thumbs/gallery.jpg" alt="Work" />
                  </div>
                  <div class="work-intro">
                     <h3 class="work-title">Gallery</h3>
                     <div class="work-descr">
                        -Jsm33t
                     </div>
                  </div>
               </a>
            </li>
            <li class="work-item mix mobile wow fadeInRight" data-wow-delay="0.3s">
               <a href="/blogs" onClick="return false;" class="work-ext-link">
                  <div class="work-img rcorner10">
                     <img src="/resources/images/home/home_thumbs/gallery.jpg" alt="Work" />
                  </div>
                  <div class="work-intro">
                     <h3 class="work-title">Blog</h3>
                     <div class="work-descr">
                        -Jsm33t
                     </div>
                  </div>
               </a>
            </li>
         </ul>
      </div>
   </section>
     
<?php
   $path = $_SERVER['DOCUMENT_ROOT'] . "/database/testimonials.xml";
   $xmlfile = file_get_contents($path);
   $new = simplexml_load_string($xmlfile);
   $con = json_encode($new);
   $json = json_decode($con, true);  
?>              
   <section class="page-section bg-dark bg-dark-alfa-90 fullwidth-slider" data-background="/resources/images/home.jpgt">       
      <?php
      foreach($json as $elem)  
      {
         for ($x = 0; $x < count($elem[0]['content']); $x++) 
         {  
         ?>
            <div>
               <div class="container relative">
                     <div class="row">
                        <div class="col-lg-10 offset-lg-1 col-xl-8 offset-xl-2 align-center">
                           <!-- Section Icon -->
                           <div class="section-icon">
                                 <span class="icon-quote"></span>
                           </div>
                           <!-- Section Title --><h3 class="small-title font-alt">What people say?</h3>
                           <blockquote class="testimonial white">
                                 <p>
                                 <?php  echo $elem[0]['content'][$x]['title']; ?>
                                 </p>
                              <footer class="testimonial-author">
                                 <?php  
                                    echo $elem[0]['content'][$x]['author']; 
                                 ?>
                                 <?php 
                                    echo "<br><br><img src='resources/images/testimonials/". $elem[0]['content'][$x]['image'] .".jpg' class='rcorner_round' width='140px'><br>" ; 
                                 ?>
                              </footer>
                           </blockquote>
                        </div>
                     </div>
               </div>
            </div>
         <?php
         }
      }       
      ?>
   </section>
</main>
      
<?php include $_SERVER['DOCUMENT_ROOT'] . "/includes/foot.php"; ?>