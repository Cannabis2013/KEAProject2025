<script setup></script>
<template>
  <div id="err-cont" class="center">
    <div style="height: 64px;"></div>
    <h1 id="error-head" class="center">Druk alarm!!</h1>
    <p id="error-text">
      Siden du prøver at tilgå findes ikke. Du bør stoppe indtaget af øl øjeblikkeligt!
      <br> Tryk her for at komme tilbage til start igen:
      <span><RouterLink to="/">Hjem</RouterLink></span>
    </p>
  </div>
</template>
<style lang="css" scoped>
#err-cont {
  width: 100%;
  padding: 6px;
}

#error-head {
  color: red !important;
  width: min-content;
  white-space: nowrap;
  font-size: 3rem;
  animation: poppin 1s ease-in-out infinite;
}

@keyframes poppin {
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

@media only screen and (min-width: 768px) {
  #err-cont {
    width: 640px;
  }

  #error-head {
    font-size: 5rem;
  }
}
</style>