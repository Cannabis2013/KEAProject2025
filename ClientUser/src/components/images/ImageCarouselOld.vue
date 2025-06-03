<script setup>
import {onMounted, ref} from "vue";
import {v4 as uuid} from "uuid";

const compId = uuid()

const contId = "caro" + compId
let wrapOne = "wrapOne" + compId
let wrapTwo = "wrapTo" + compId
let imgOne = "image" + compId
let imgTwo = "image" + compId
let imgOneEl = undefined
let imgTwoEl = undefined
let contEl = undefined
let wrapOneEl = undefined;
let wrapTwoEl = undefined;
let width = 0
let index = 0

const images = ["/garageSale.jpg", "/home.png", "/al-logo.png"]

onMounted(() => {
  contEl = document.getElementById(contId);
  wrapOneEl = document.getElementById(wrapOne);
  wrapTwoEl = document.getElementById(wrapTwo);
  imgOneEl = document.getElementById(imgOne);
  imgTwoEl = document.getElementById(imgTwo);
  width = contEl.getBoundingClientRect().width
  wrapOneEl.style.left = "0px";
  wrapTwoEl.style.left = `${width}px`;
})

function nextImage() {
  if (index + 1 >= images.length) return
  index++
  if (wrapOneEl.style.left === "0px") {
    wrapOneEl.style.left = `-${width}px`;
    wrapTwoEl.style.left = "0px";
  } else if (wrapTwoEl.style.left === "0px") {
    wrapOneEl.classList.remove("slide-transition")
    wrapOneEl.style.left = `${width}px`;
    imgOneEl.src = images[index]
    wrapTwoEl.style.left = `-${width}px`;
    wrapOneEl.classList.add("slide-transition");
    wrapOneEl.style.left = "0px"
  }
}

function previousImage() {
  if (index <= 0) return
  index--;
  wrapOneEl.style.left = `0px`;
  wrapTwoEl.style.left = `${width}px`;
}
</script>

<template>
  <div :id="contId" class="carousel-cont fillw">
    <div :id="wrapOne" class="image-wrapper slide-transition">
      <img :id="imgOne" class="carousel-image" :src="images[0]"/>
    </div>
    <div v-if="images.length > 1" :id="wrapTwo" class="image-wrapper slide-transition">
      <img :id="imgTwo" class="carousel-image" :src="images[1]"/>
    </div>
    <div class="carousel-btns fillw">
      <button :onclick="previousImage"><</button>
      <button :onclick="nextImage">></button>
    </div>
  </div>
</template>

<style scoped lang="css">
.carousel-cont {
  position: relative;
  overflow: hidden;
  height: 100%;
}

.slide-transition {
  transition: left .5s linear;
}

.no-transition {
  transition: none;
}

.image-wrapper {
  position: absolute;
  top: 0;
  width: 100%;
  bottom: 48px;
  overflow: hidden;
  display: grid;
  align-items: center;
}

.carousel-btns {
  height: 32px;
  display: flex;
  justify-content: center;
  position: absolute;
  bottom: 0;
}

.carousel-image {
  height: auto;
  width: 100%;
}
</style>