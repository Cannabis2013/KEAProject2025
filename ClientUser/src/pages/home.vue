<script setup>
import Landing from "@/components/home/landing/landing.vue"
import Contact from '@/components/home/contact/contact.vue';
import NewsFeed from "@/components/news/newsFeed.vue";
</script>
<template>
  <Landing/>
  <div class="feed-cont horizontal-center">
    <div></div>
    <div></div>
    <NewsFeed/>
  </div>
  <Contact/>
</template>
<style lang="css" scoped>

.feed-cont {
  display: grid;
  padding: 9px;
  min-height: 100vh;
}

@media (orientation: landscape) and (min-width: 1280px) {
  .feed-cont {
    width: 1280px;
    grid-template-columns:1fr 1fr 1fr;
    column-gap: 9px;
  }
}
</style>