<script setup>
import ActionCard from "@/components/cards/ActionCard.vue";
</script>

<template>
  <div class="action-cards-cont">
    <ActionCard
        :bullets="['Vælg synlighed']"
        title="Opret besked"
    />
    <ActionCard
        :bullets="['Vælg sted', 'Vælg tid','Vælg synlighed']"
        title="Opret begivenhed"
    />
    <ActionCard
        :bullets="['Vælg billede', 'Vælg forfatter','Vælg synlighed', 'Opret indhold']"
        title="Opret nyhed"
    />
    <ActionCard
        :bullets="['Vælg synlighed']"
        linkTitle="Skift"
        title="Skift email"
    />
    <ActionCard
        :bullets="['jpeg/png/webm', 'Helst portræt for bedste resultat']"
        linkTitle="Skift"
        title="Skift profilbillede"
    />
    <ActionCard
        :bullets="['Mindst 8 bogstaver', 'Bør helst have minimum et stort bogstav','Bør indeholde mindst et tal','Bør indeholde mindst et specialtegn']"
        linkTitle="Skift"
        title="Skift kodeord"
    />
  </div>
</template>

<style lang="css" scoped>
@media (orientation: portrait) or (max-width: 767px) {
  .action-cards-cont {
    display: grid;
    row-gap: 1rem;
    overflow: auto;
  }
}

@media (orientation: landscape) or (min-width: 768px) {
  .action-cards-cont {
    display: grid;
    grid-template-columns: repeat(auto-fill, 320px);
    gap: .1rem;
    height: 100%;
    justify-content: center;
    overflow: auto;
  }
}
</style>