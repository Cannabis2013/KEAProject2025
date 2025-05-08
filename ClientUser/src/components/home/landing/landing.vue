<script setup>
import textRotator from './textRotator/textRotator.vue';
import alBanner from '@/components/banners/logoBanner/alBanner.vue';
</script>
<template>
  <div class="landing-banner-cont">
    <textRotator class="text-rotation-cont" words='["Passion","Sammenhold","Loyalitet"]'/>
    <alBanner/>
    <div class="text-banner">
      <p>PASSION</p>
      <p>SAMMENHOLD</p>
      <p>LOYALITET</p>
    </div>
  </div>
</template>
<style lang="css" scoped src="./landing-mobile.css"></style>
<style lang="css" scoped src="./landing-large.css"></style>