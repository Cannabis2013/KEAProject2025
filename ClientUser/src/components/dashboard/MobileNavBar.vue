<script setup>
import {onMounted} from 'vue';
import Link from "@/components/controls/link.vue";
import routerLinks from "@/router/routerLinks";
import {useRouter} from 'vue-router';

const router = useRouter()

let displayMode = ""
let foldableDiv = null

onMounted(() => {
  foldableDiv = document.querySelector(".mobile-nav-foldable")
  foldableDiv.style.display = "none"
})

function toggleMenu() {
  displayMode = foldableDiv.style.display
  if (displayMode === "none") {
    foldableDiv.style.display = "grid"
    setTimeout(() => {
      foldableDiv.style.opacity = "1";
    }, 1)
  } else {
    foldableDiv.style.opacity = "0";
    setTimeout(() => {
      foldableDiv.style.display = "none"
    }, 500)
  }
}

function navigateTo(path) {
  router.push(path)
}
</script>
<template>
  <div class="mobile-nav-cont">
    <div :onclick="toggleMenu" class="mobile-nav-handle"></div>
    <div class="mobile-nav-foldable">
      <Link v-for="link in routerLinks" :link="link" :onHandleUrl="navigateTo"
            :onLinkClicked="toggleMenu"/>
    </div>
  </div>
</template>
<style lang="css" scoped>
.mobile-nav-cont,
.mobile-nav-handle {
  display: none;
}

@media (orientation: portrait) or (max-width: 767px) {
  .mobile-nav-cont {
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    height: 64px;
    display: grid;
    z-index: 999;
  }

  .mobile-nav-handle {
    position: absolute;
    right: 6px;
    top: 6px;
    display: block;
    background: url("/hamburger.png") center no-repeat;
    background-size: cover;
    width: 40px;
    height: 40px;
    z-index: 999;
  }

  .mobile-nav-foldable {
    display: none;
    padding: 64px 6px 6px 6px;
    background-color: black;
    row-gap: 6px;
    align-content: start;
    height: 100vh;
    opacity: 0;
    transition: opacity .5s ease-in-out;
  }
}
</style>