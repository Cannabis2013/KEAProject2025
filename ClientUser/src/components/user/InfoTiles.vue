<script setup>
import InfoTile from "@/components/tiles/InfoTile.vue";

const props = defineProps(["member"])
</script>
<template>
  <div class="tiles-cont">
    <InfoTile :value="member.joinedDate" tileDrop="#1f1f1f" title="Dato tiltrådt"/>
    <InfoTile tileDrop="#1f1f1f" title="Sidste login" value="I går"/>
    <InfoTile tileDrop="#1f1f1f" title="Beskeder" value="510"/>
    <InfoTile tileDrop="#1f1f1f" title="Begivenheder" value="5"/>
  </div>
</template>
<style lang="css" scoped>
.tiles-cont {
  display: none;
}

@media (orientation: landscape) or (min-width: 768px) {
  .tiles-cont {
    width: 100%;
    height: 64px;
    display: grid;
    align-self: end;
    grid-auto-flow: column;
    background-color: #3a3a3a;
    border: 1px solid #3a3a3a;
    column-gap: 1px;
  }
}
</style>