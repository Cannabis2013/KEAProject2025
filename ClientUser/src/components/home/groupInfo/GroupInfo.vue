<script setup>
import DataBox from './DataBox.vue';
</script>
<template>
  <div id="groupInfo" class="groupInfo-cont">
    <div class="groupInfo-grid">
      <DataBox title="Medlemmer" value="24"/>
      <DataBox title="Formand" value="Hjalte Grønnegård"/>
      <DataBox title="Næstformand" value="Lars Skousen"/>
      <DataBox title="Kassér" value="Rasmus Clemmesen"/>
    </div>
  </div>
</template>
<style lang="css" scoped>
.groupInfo-cont {
  width: 100%;
  height: 100%;
  display: grid;
  align-items: center;
  justify-content: center;
}

.groupInfo-grid {
  width: 100%;
  height: 100%;
  display: grid;
}

.info-box {
  height: 100%;
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

@media (min-width: 768px) {
  .groupInfo-grid {
    grid-template-columns: 1fr 1fr;
    grid-template-rows: 1fr 1fr;
  }
}
</style>