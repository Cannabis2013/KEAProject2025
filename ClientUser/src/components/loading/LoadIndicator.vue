<script setup>
const props = defineProps(["message"])
</script>
<template>
  <div class="loading-cont">
    <h1 style="margin: 0;">{{ message ?? "Indlæser" }}</h1>
  </div>
</template>
<style lang="css" scoped>
.loading-cont {
  display: grid;
  align-items: center;
  justify-content: center;
  height: 100%;
  width: 100%;
  animation: flashing 2s ease-in-out infinite;
}

@keyframes flashing {
  0% {
    opacity: 0.1;
  }
  50% {
    opacity: 1;
  }
  100% {
    opacity: 0.1;
  }
}
</style>