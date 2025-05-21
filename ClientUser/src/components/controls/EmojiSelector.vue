<script setup>

const props = defineProps(["onSelect"])

const smileys = ["🤣", "🙂", "🥰", "☺️",
  "🥲", "🤥", "🥶", "🤬","🫠"]

const gestures = ["🖕🏼","🤘🏼","👊🏼","👉🏼","👈🏼"]

const selectHandler = props.onSelect ?? function () { }

</script>

<template>
  <div class="emojis-cont">
    <p v-for="smiley in smileys" :onclick="() => selectHandler(smiley)"> {{ smiley }} </p>
    <p v-for="finger in gestures" :onclick="() => selectHandler(finger)"> {{ finger }} </p>
  </div>
</template>

<style scoped lang="css">
.emojis-cont {
  height: min-content;
  border-radius: 6px;
  display: flex;
  column-gap: 1.5px;
  padding: 3px;
  background-color: rgba(245, 222, 179, .1);
}

.emojis-cont>p {
  height: 100%;
  display: grid;
  align-items: center;
  cursor: pointer;
  user-select: none;
  transition: scale 50ms ease-in-out;
}

.emojis-cont>p:hover {
  text-shadow: lightgray 0 0 1px;
}

.emojis-cont>p:active {
  scale: 0.90;
}
</style>