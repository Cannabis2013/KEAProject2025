<script setup>
import ContactCard from "./card.vue"
</script>
<template>
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

@media only screen and (min-width: 768px) {
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
}
</style>