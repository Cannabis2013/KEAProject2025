<script setup>

defineProps(["onPushed", "text"])

</script>

<template>
  <div :onclick="onPushed" class="push-cont">
    <p class="push-text"> {{ text }} </p>
  </div>
</template>

<style lang="css" scoped>
.push-cont {
  padding: 6px;
  background-color: lightgrey;
  width: min-content;
  height: min-content;
  cursor: pointer;
  border-radius: 0.2rem;
}

.push-cont:hover {
  box-shadow: 0 0 4px white;
}

.push-cont:active {
  scale: .9;
}

.push-text {
  color: black;
  white-space: nowrap;
  text-align: center;
}
</style>