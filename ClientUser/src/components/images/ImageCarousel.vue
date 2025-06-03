<script setup>
import {onMounted, ref} from "vue";

let slideIndex = 0;
let slides = [];
let dots = [];

// Next/previous controls
function plusSlides(n) {
  slideIndex += n
  showSlides();
}

// Thumbnail image controls
function currentSlide(n) {
  slideIndex = n
  showSlides();
}

function showSlides() {
  let i;

  if (slideIndex >= slides.length)
    slideIndex = 0

  if (slideIndex < 0)
    slideIndex = slides.length - 1

  for (i = 0; i < slides.length; i++)
    slides[i].style.display = "none";

  for (i = 0; i < dots.length; i++)
    dots[i].className = dots[i].className.replace(" active", "");

  slides[slideIndex].style.display = "block";
  dots[slideIndex].className += " active";
}

onMounted(() => {
  slides = document.querySelectorAll(".mySlides");
  dots = document.querySelectorAll(".dot");
  showSlides();
})

</script>

<template>
  <!-- Slideshow container -->
  <div class="slideshow-container fillp">

    <div class="carousel-wrapper fillp center-content">
      <img src="/garageSale.jpg" class="mySlides fade carousel-image">
      <img src="/al-logo.png" class="mySlides fade carousel-image">
      <img src="/hamburger.png" class="mySlides fade carousel-image">
    </div>

    <a class="prev" :onclick="() => plusSlides(-1)">&#10094;</a>
    <a class="next" :onclick="() => plusSlides(1)">&#10095;</a>
    <div style="text-align:center" class="carousel-dots fillw">
      <span class="dot" :onclick="() => currentSlide(0)"></span>
      <span class="dot" :onclick="() => currentSlide(1)"></span>
      <span class="dot" :onclick="() => currentSlide(2)"></span>
    </div>
  </div>
</template>

<style scoped lang="css">
/* Slideshow container */
.slideshow-container {
  position: relative;
  margin: auto;
}

/* Hide the images by default */
.mySlides {
  display: none;
}

.carousel-dots{
  position: absolute;
  bottom: 10px;
}

.carousel-image {
  max-width: 100%;
  max-height: 100%;
  overflow: auto;
}

/* Next & previous buttons */
.prev, .next {
  cursor: pointer;
  position: absolute;
  top: 50%;
  width: auto;
  margin-top: -22px;
  padding: 16px;
  color: white;
  font-weight: bold;
  font-size: 18px;
  transition: 0.6s ease;
  border-radius: 0 3px 3px 0;
  user-select: none;
}

.carousel-wrapper{
  position: absolute;
}

/* Position the "next button" to the right */
.next {
  right: 0;
  border-radius: 3px 0 0 3px;
}

/* On hover, add a black background color with a little bit see-through */
.prev:hover, .next:hover {
  background-color: rgba(0, 0, 0, 0.8);
}

/* Caption text */
.text {
  color: #f2f2f2;
  font-size: 15px;
  padding: 8px 12px;
  position: absolute;
  bottom: 8px;
  width: 100%;
  text-align: center;
}

/* Number text (1/3 etc) */
.numbertext {
  color: #f2f2f2;
  font-size: 12px;
  padding: 8px 12px;
  position: absolute;
  top: 0;
}

/* The dots/bullets/indicators */
.dot {
  cursor: pointer;
  height: 15px;
  width: 15px;
  margin: 0 2px;
  background-color: #bbb;
  border-radius: 50%;
  display: inline-block;
  transition: background-color 0.6s ease;
}

.active, .dot:hover {
  background-color: #717171;
}

/* Fading animation */
.fade {
  animation-name: fade;
  animation-duration: 1.5s;
}

@keyframes fade {
  from {
    opacity: .4
  }
  to {
    opacity: 1
  }
}
</style>