<script setup>
const props = defineProps(["link", "iconPath", "onHandleUrl", "onLinkClicked"])

const aniClass = "animatable"

function handleClick(el) {
  performAnimation(el.target.parentElement)
  setTimeout(() => {
    props.onHandleUrl(props.link.href)
    props.onLinkClicked()
    resetAnimation(el.target.parentElement)
  }, 125);
}

function performAnimation(el) {
  if (!el.classList.contains(aniClass))
    el.classList.add(aniClass)
}

function resetAnimation(el) {
  if (el.classList.contains(aniClass))
    el.classList.remove(aniClass)
}

</script>
<template>
  <div :onclick="handleClick" class="link-cont">
    <img :src="props.link.icon" class="link-icon"/>
    <p class="link-text">{{ props.link.title }} </p>
  </div>
</template>
<style lang="css" scoped>
.link-cont {
  display: grid;
  height: 48px;
  justify-items: center;
  align-items: center;
  column-gap: 6px;
  grid-template-columns: min-content 1fr;
  border-radius: 6px;
  padding: 6px;
  background-color: white;
  cursor: pointer;
}

.link-cont:hover {
  box-shadow: white 0 0 12px;
}

.link-icon {
  width: 32px;
  height: 32px;
}

.link-text {
  width: 100%;
  height: 100%;
  font-size: 1.25rem;
  line-height: 1.25rem;
  white-space: nowrap;
  margin: 0;
  color: black !important;
  display: grid;
  align-items: center;
}

.animatable {
  animation: scaleUpDown 125ms ease-in-out 1;
}

@keyframes scaleUpDown {
  0% {
    scale: 1;
  }

  50% {
    scale: 1.25;
  }

  100% {
    scale: 1;
  }
}
</style>