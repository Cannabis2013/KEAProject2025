<script setup>
import InfoTile from "@/components/tiles/InfoTile.vue";

const props = defineProps(["member"])

function getName() {
  const firstName = props.member.firstName
  const lastName = props.member.lastName
  return `${firstName} ${lastName}`
}

const profileImage = props.member.profileImageAsBase64 ?? "/userTemplate.png"

</script>
<template>
  <div class="member-card-cont al-box fillp">
    <div class="card-image-wrapper">
      <img :src="profileImage" class="fillp"/>
    </div>
    <InfoTile
        :value="getName()"
        title="Navn"
    />
    <InfoTile
        :value="member.id"
        title="Medlems nr."
    />
    <InfoTile
        :value="member.email"
        title="Mail"
    />
  </div>
</template>
<style lang="css" scoped>
.member-card-cont {
  transition: scale .5s ease;
  display: grid;
  align-content: center;
  row-gap: 6px;
  grid-template-rows: min-content min-content min-content min-content;
  justify-items: center;
  justify-content: center;
  padding: 1rem;
}

.card-image-wrapper {
  width: 200px;
  height: auto;
  border-radius: 3px;
  overflow: hidden;
}

.card-details-name {
  font-size: 2rem;
  color: black !important;
  font-weight: bold;
  text-align: center;
}
</style>