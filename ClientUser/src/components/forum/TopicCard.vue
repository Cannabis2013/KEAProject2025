<script setup>
import {RouterLink} from "vue-router"
const props = defineProps(["model"])

</script>
<template>
  <div class="topic-tile-cont fillw pad16 gapx16 round6">
    <img class="topic-tile-icon" src="/message2.png"/>
    <p class="topic-tile-content"> 
      {{ model.author }} har skrevet et indlæg i 
      <span><RouterLink class="topic-tile-link" :to="'topic/' + model.id">{{ model.title }}</RouterLink></span>
    </p>
    
  </div>
</template>
<style lang="css" scoped>
.topic-tile-cont {
  display: grid;
  grid-template-columns: 80px 1fr;
  background-color: white;
  height: min-content;
}

.topic-tile-icon {
  height: 64px;
}

.topic-tile-upper {
  border-bottom: 1px solid black;
}

.topic-tile-content {
  margin: 0;
  color: black !important;
  font-size: 1rem;
  white-space: pre-wrap;
}

.topic-tile-link{
  justify-self: end;
  grid-column: span 2;
  color: darkblue;
}
</style>