<script setup>
import ImageCarousel from "@/components/images/ImageCarousel.vue";
import Feed from "@/components/feed/Feed.vue";
import TextRotator from "@/components/textRotator/TextRotator.vue";
import ALTextBanner from "@/components/banners/ALTextBanner.vue";
import AlLogoBanner from "@/components/banners/ALLogoBanner.vue";
import ContactCard from "@/components/contact/ContactCard.vue";
import HistoryChart from "@/components/historyChart/HistoryChart.vue";
</script>
<template>
  <div class="landing-banner-cont">
    <TextRotator class="text-rotation-cont"/>
    <AlLogoBanner/>
    <ALTextBanner />
  </div>
  <div id="home-mid-grid" class="center-content pad16">
    <div id="carousel-cont" class="fillw">
      <ImageCarousel />
    </div>
    <Feed id="feed-cont" class="fillp"/>
  </div>
  <HistoryChart style="margin-bottom: 16rem;"/>
  <div class="contact-cont">
    <h1 class="contact-head">Kontakt</h1>
    <div class="contact-grid">
      <ContactCard mail="storpik@gmail.com" name="Hjalte Grønnegård" phone="12345678" res="Formand"/>
      <ContactCard mail="unterberg@gmail.com" name="Lars Skousen" phone="12345678" res="Presseansvarlig"/>
      <ContactCard mail="mh2012@outlook.dk" name="Martin Hansen" phone="31165870" res="Webmaster"/>
    </div>
  </div>
</template>
<style lang="css" scoped>

.contact-head {
  font-size: 3.5rem !important;
  border-bottom: 3px solid lightskyblue;
  margin: 0 12px 6px 12px;
}

.contact-cont {
  width: 100%;
  display: grid;
  align-items: center;
}

.contact-grid {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  row-gap: 20px;
}

@media (orientation: portrait) or (max-width: 767px){
  .landing-banner-cont {
    height: 100vh;
    width: 100%;
    display: grid;
    position: relative;
    align-items: center;
  }
  
  #home-mid-grid {
    grid-template-rows: 384px 512px;
  }

  #carousel-cont{
    height: 100vh;
  }
}

@media (orientation: landscape) and (min-width: 768px) {
  .landing-banner-cont {
    display: grid;
    height: 100vh;
    grid-template-rows: 1fr min-content 1fr;
    background-color: black;
    position: relative;
    align-items: center;
  }

  .text-rotation-cont {
    margin-left: 128px;
  }
  
  #home-mid-grid{
    display: grid;
    grid-template-columns: 512px min-content;
    justify-content: center;
    column-gap: 40px;
    min-height: 100vh;
  }

  .contact-cont {
    border-top: 3px solid lightskyblue;
    height: 160px;
  }

  .contact-head {
    display: none;
  }

  .contact-grid {
    flex-direction: row;
    justify-content: center;
  }
  #carousel-cont{
    height: 512px;
  }
}

</style>