<script setup>
import ImageCarousel from "@/components/images/ImageCarousel.vue";
import Landing from "@/components/home/landing/landing.vue"
import Contact from '@/components/home/contact/contact.vue';
import Feed from "@/components/home/Feed/Feed.vue";
</script>
<template>
  <Landing/>
  <div id="home-mid-grid" class="fillp pad16 center-content">
    <div id="carousel-cont" class="fillw pad16">
      <ImageCarousel />
    </div>
    <Feed id="feed-cont" class="fillp"/>
  </div>
  <Contact/>
</template>
<style lang="css" scoped>

@media (orientation: portrait) or (max-width: 767px){
  #home-mid-grid {
    grid-template-rows: 384px 512px;
  }
}

@media (orientation: landscape) and (min-width: 768px) {
  #home-mid-grid{
    grid-template-columns: 512px min-content;
    justify-content: center;
    column-gap: 40px;
  }
}

#carousel-cont{
  border-radius: 6px;
  height: 512px;
}
</style>