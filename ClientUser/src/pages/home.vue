<script setup>
import Landing from "@/components/home/landing/landing.vue"
import Contact from '@/components/home/contact/contact.vue';
import Feed from "@/components/home/Feed/Feed.vue";
</script>
<template>
  <Landing/>
  <Feed/>
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
    grid-template-columns:384px 384px;
    column-gap: 9px;
    justify-content: center;
  }
}
</style>