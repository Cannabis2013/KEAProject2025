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
      dashContainer.style.height = "100vh"
    }, 1);
  } else {
    dashContainer.style.height = "0"
    setTimeout(() => {
      dashContainer.style.display = "none"
    }, 300);
  }
}
</script>
<template>
  <img :onclick="toggleMenu" class="nav-handle" src="/dashboard-handle.png"/>
  <div class="dash-cont fillsw">
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
<style lang="css" scoped>
.nav-handle, .dash-cont, .dash-inner-cont {
  display: none;
}

.dash-cont{
  transition: height 0.5s ease-in-out;
  height: 0;
  background-color: #3f3f3f;
}

@media only screen and (min-width: 768px) {
  .nav-handle {
    display: block;
    position: absolute;
    top: 9px;
    left: 9px;
    z-index: 800;
    width: 64px;
    height: 64px;
    cursor: pointer;
    border-bottom-right-radius: 6px;
  }

  .nav-handle:active {
    box-shadow: white 0 0 15px;
  }

  .dash-inner-cont {
    display: grid;
    grid-template-columns: 1fr 1fr;
    grid-template-rows: min-content 1fr;
    align-items: start;
    position: absolute;
    left: 128px;
    right: 0;
    top: 0;
    bottom: 0;
  }

  .dash-head {
    font-size: 6rem;
    font-weight: bold;
    grid-column: span 2;
  }

  .links-cont {
    display: grid;
    grid-template-columns: repeat(auto-fill, 192px);
    grid-auto-rows: min-content;
    column-gap: 20px;
    justify-content: start;
    overflow: scroll;
    gap: 20px;
    height: 100%;
    padding: 9px;
  }
}
</style>