<template>
  <div class="landing-banner">
    <div class="banner-cont">
      <div class="white-banner"></div>
      <div class="blue-banner">
        <p>AMAGER</p>
        <p>LOYALIST</p>
      </div>
    </div>
    <div class="al-logo-image"></div>
  </div>
</template>
<style lang="css" scoped>
@media (orientation: portrait) or (max-width: 767px){
  .landing-banner {
    height: 170px;
    width: 100%;
    display: grid;
    justify-content: center;
  }
  
  .banner-cont {
    display: none;
  }

  .al-logo-image {
    height: 170px;
    width: 274px;
    background: url("/al_logo_4.png") no-repeat;
    background-size: cover;
    animation: scaler 2s linear 0s infinite;
  }
}

@media (orientation: landscape) and  (min-width: 768px) {
  .landing-banner {
    position: relative;
    height: 300px;
    width: 100%;
    display: grid;
    align-items: center;
    justify-content: unset;
  }

  .banner-cont {
    display: grid;
    grid-template-rows: 1fr 1fr;
    height: 160px;
    width: 100%;
    position: absolute;
    bottom: 20px;
  }

  .white-banner {
    background-color: white;
  }

  .blue-banner {
    background-color: lightskyblue;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 32px 0 32px;
  }

  .blue-banner > p {
    font-size: 3.2rem;
    line-height: 2.9rem;
    height: 100%;
    display: grid;
    align-items: center;
    color: white;
    font-weight: bold;
    margin: 0;
    padding: 0;
  }

  .al-logo-image {
    position: absolute;
    left: calc(50% - 245px);
    bottom: 0;
    height: 300px;
    width: 490px;
    background: url("/al_logo_4.png") no-repeat;
    background-size: cover;
    animation: scaler 2s linear 0s infinite;
  }
}

@keyframes scaler {
  0% {
    scale: 1
  }

  45% {
    scale: 1
  }

  50% {
    scale: 1.05
  }

  55% {
    scale: 1;
  }

  100% {
    scale: 1
  }
}
</style>