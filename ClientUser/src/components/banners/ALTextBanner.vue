<template>
  <div class="text-banner">
    <p>PASSION</p>
    <p>SAMMENHOLD</p>
    <p>LOYALITET</p>
  </div>
</template>

<style scoped lang="css">
@media (orientation: landscape) and (min-width: 768px){
  .text-banner {
    width: 100%;
    display: flex;
    justify-content: space-between;
    padding: 0 32px 0 32px;
    align-self: flex-start;
    margin: 0;
  }

  .text-banner > p {
    font-size: 2rem;
    display: grid;
    align-items: center;
    font-weight: bold;
    margin: 0;
    padding: 0;
  }
}

@media (orientation: portrait) or (max-width: 767px){
  .text-banner {
    display: none;
  }
}

</style>