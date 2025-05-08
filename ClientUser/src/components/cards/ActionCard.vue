<script setup>
import ButtonLink from "@/components/controls/ButtonLink.vue";

const props = defineProps(["title", "info", "imageUrl", "href"])

const linkText = props.linkTitle ?? "Opret"
</script>

<template>
  <div class="user-controls-tile al-box fill-parent">
    <div class="card-slot-cont">
      <h1>{{ title }}</h1>
      <p>{{ info }}</p>
    </div>
    <ButtonLink :text="linkText" :to="href" class="tile-button"/>
  </div>
</template>

<style lang="css" scoped>
.user-controls-tile {
  display: grid;
  grid-template-rows: 1fr min-content;
  align-items: start;
  align-content: start;
  padding: 12px;
  row-gap: .5rem;
  animation: sizeIn .5s ease-in-out;
}

.tile-button {
  width: 128px;
  align-self: end;
  justify-self: flex-end;
}

</style>