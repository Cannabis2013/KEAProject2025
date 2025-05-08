<script setup>

</script>
<template>
  <div id="auth-cont" class="horizontal-center">
    <div style="height: 64px;"></div>
    <h1 id="auth-head" class="horizontal-center">ULOVLIG INDTRÆNGEN!!</h1>
    <p id="auth-text" class="horizontal-center">
      Siden du prøver at tilgå kan ikke tilgås af andre end medlemmer af Amager Loyalist
      eller formanden.<br><br>
      Gentagne forsøg på indtrængen vil blive meldt til PET!!<br><br>
      Tryk her for at komme tilbage til start igen:
      <span><RouterLink replace to="/">Hjem</RouterLink></span>
    </p>
  </div>
</template>
<style lang="css" scoped>
#auth-cont {
  width: 100% !important;
  padding: 6px;
}

#auth-head {
  color: red !important;
  width: min-content;
  white-space: nowrap;
  font-size: 3rem;
  animation: poppin 1s ease-in-out infinite;
}

#auth-text {
  text-align: center;
}

@keyframes poppin {
  from {
    scale: 1;
  }

  50% {
    scale: 1.05;
  }

  100% {
    scale: 1;
  }
}
</style>