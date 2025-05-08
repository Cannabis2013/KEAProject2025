<script setup>
import {useRouter} from "vue-router";

const props = defineProps(["onLinkClicked", "to", "text"])
const buttonText = props.text ?? "Link"
const router = useRouter()

function handleClick() {
  if (props.to)
    router.push(props.to)
  if (props.onLinkClicked)
    props.onLinkClicked()
}

</script>
<template>
  <div :onclick="handleClick" class="btn-lnk-cont"> {{ buttonText }}</div>
</template>
<style lang="css" scoped>
.btn-lnk-cont {
  padding: .2rem;
  display: grid;
  align-items: center;
  justify-items: center;
  border-radius: .5rem;
  cursor: pointer;
  color: white;
  background-color: #1f1f1f;
}

.btn-lnk-cont:hover {
  box-shadow: white 0 0 6px;
}

.btn-lnk-cont:active {
  scale: .9;
}

</style>