<script setup>
import Link from "@/components/controls/link.vue";
import routerLinks from "@/router/routerLinks";
import {onMounted} from "vue";
import {useRouter} from 'vue-router';
import UserView from "@/components/user/UserView.vue";

const links = routerLinks.filter(link => link.href !== "/login");

const router = useRouter()
let dashContainer = null

onMounted(() => {
  dashContainer = document.querySelector(".dash-cont")
})

function handleNavigationRequest(href) {
  router.push(href)
}

function toggleMenu() {
  const status = dashContainer.style.display
  if (status !== "block") {
    dashContainer.style.display = "block"
    setTimeout(() => {
      dashContainer.style.opacity = "1"
    }, 1);
  } else {
    dashContainer.style.opacity = "0"
    setTimeout(() => {
      dashContainer.style.display = "none"
    }, 300);
  }
}
</script>
<template>
  <img :onclick="toggleMenu" class="nav-handle" src="/dashboard-handle.png"/>
  <div class="dash-cont">
    <div class="dash-inner-cont">
      <h1 class="dash-head">Dashboard</h1>
      <div class="links-cont">
        <Link
            v-for="link in links"
            :link="link"
            :onHandleUrl="handleNavigationRequest"
            :onLinkClicked="toggleMenu"
        />
      </div>
      <UserView :onNavigation="toggleMenu"/>
    </div>
  </div>
</template>
<style lang="css" scoped src="./dashboard.css"></style>