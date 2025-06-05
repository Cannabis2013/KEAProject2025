<script>
export default {
  props: ["title", "value"]
}
</script>
<template>
  <div class="info-box">
    <p class="box-title">{{ title }}</p>
    <p class="box-value animate-character"> {{ value }}</p>
  </div>
</template>
<style lang="css" scoped>
.box-title {
  font-weight: bold;
  font-size: 1rem;
  line-height: 1.75rem;
  font-weight: bold;
}

.info-box > p {
  height: min-content;
  display: grid;
  align-items: center;
  justify-content: center;
}

.box-value {
  font-size: 1rem;
  line-height: 1.25rem;
}

.animate-character {
  background-image: linear-gradient(-225deg,
  lightskyblue 0%,
  white 25%,
  lightskyblue 50%,
  black 75%,
  lightskyblue 100%);
  background-size: auto auto;
  background-size: 200% auto;
  background-clip: text;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  animation: textclip 6s linear infinite;
}


@keyframes textclip {
  to {
    background-position: 200% center;
  }
}

@media only screen and (min-width: 768px) {
  .box-title {
    font-size: 2rem;
    line-height: 3.75rem;
    font-weight: unset;
  }

  .box-value {
    font-size: 1rem;
    line-height: unset;
    white-space: nowrap;
  }
}
</style>
