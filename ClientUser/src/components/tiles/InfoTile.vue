<script setup>
import {ref} from "vue";

const props = defineProps(["title", "value", "tileDrop", "textColor"])

const backDrop = ref(props.tileDrop ?? "transparent")
const textColor = ref(props.textColor ?? "lightskyblue");
</script>
<template>
  <div class="tile-cont">
    <p class="tile-title">{{ title }}</p>
    <p class="tile-value"> {{ value }} </p>
  </div>
</template>

<style lang="css" scoped>
.tile-cont {
  min-width: 64px;
  width: 100%;
  height: 100%;
  background-color: v-bind(backDrop);
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.tile-cont > p {
  text-align: center;
  margin: 0;
  white-space: nowrap;
  color: v-bind(textColor) !important;
}

.tile-title {
  font-size: 1.25rem;
  font-weight: bold;
}

.tile-value {
  font-size: 1.25rem;
}
</style>