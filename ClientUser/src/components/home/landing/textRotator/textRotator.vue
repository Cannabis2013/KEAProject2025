<script setup>
import rotator from './rotator';
import {v4 as uuidv4} from 'uuid';
import {onMounted} from "vue";

const props = defineProps(["words"])
const instanceId = uuidv4()

onMounted(function () {
  rotator(props.words, instanceId)
})

</script>
<template>
  <h1 :id="instanceId" class="txt-rotate" data-period="1000"></h1>
</template>
<style lang="css" scoped>
@media (orientation: portrait) or (max-width: 767px){
  .txt-rotate {
    position: absolute;
    top: 64px;
    left: 8px;
  }
}

@media (orientation: landscape) or (min-width: 768px) {
  .txt-rotate {
    display: grid;
    justify-items: center;
    align-self: center;
    width: min-content;
    color: rgba(211, 211, 211, 0.66) !important;
    font-size: 3rem !important;
  }
}
</style>