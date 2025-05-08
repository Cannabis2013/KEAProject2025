<script setup>
const props = defineProps(["onChange", "value", "masked"])
const textMode = props.masked ?? false ? "password" : "text"

const onUpdate = props.onChange ?? function () {
}
const value = props.value ?? ""

</script>
<template>
  <div class="input-cont" style="width: 100%">
    <input :onchange="onUpdate" :type="textMode" :value="value" aria-describedby="basic-addon1"
           aria-label="Brugernavn"
           class="input-text" placeholder="Brugernavn" style="outline: none;">
  </div>
</template>
<style lang="css" scoped>
.input-cont {
  width: 100%;
  height: 100%;
}

.input-text {
  width: 100%;
  height: 100%;
  font-size: 1rem;
  padding: 6px;
  border-radius: 6px;
}
</style>