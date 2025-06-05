<script setup>
const props = defineProps(["name", "mail", "phone", "res"])
</script>
<template>
  <div class="contact-card">
    <p style="font-size: 1.5rem;white-space: nowrap;">{{ props.name }}</p>
    <p style="font-size: .8rem;line-height: .8rem;font-style: italic;">{{ props.res }}</p>
    <div class="contact-spacer" style="--space:16px"></div>
    <div class="contact-col">
      <div class="card-logo mail-logo"></div>
      <a :href="`mailto:${props.mail}`" class="contact-mail">{{ props.mail }}</a>
    </div>
    <div class="contact-spacer" style="--space:6px"></div>
    <div class="contact-col">
      <div class="card-logo phone-logo"></div>
      <a :href="`tel:${props.phone}`"> {{ props.phone }}</a>
    </div>
  </div>
</template>
<style lang="css" scoped>
.contact-card {
  width: 256px;
  padding: 8px;
}

.contact-card > p {
  margin: 0;
}

.contact-col {
  display: grid;
  grid-template-columns: 32px auto;
  align-items: center;
}

.contact-col > a {
  line-height: 24px;
  font-size: 18px;
  display: grid;
  align-items: center;
}

.card-logo {
  height: 20px;
  width: 20px;
}

.phone-logo {
  background: url("./phone.png") no-repeat;
  background-size: cover;
}

.mail-logo {
  background: url("./mail.png") no-repeat;
  background-size: cover;
}

.contact-spacer {
  height: var(--space);
}
</style>