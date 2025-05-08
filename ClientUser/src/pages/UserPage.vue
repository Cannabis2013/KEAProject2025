<script setup>
import {ref} from 'vue';
import {signedInMember} from "@/services/members/members.js";
import MemberCard from "@/components/cards/MemberCard.vue";
import LoadIndicator from '@/components/loading/LoadIndicator.vue';
import InfoTiles from "@/components/user/InfoTiles.vue";
import ActionCard from "@/components/cards/ActionCard.vue";

const isLoaded = ref(false)
let member

signedInMember().then(m => {
  member = m
  isLoaded.value = member !== null
})

</script>
<template>
  <div v-if="isLoaded" class="user-page-cont">
    <div class="user-page-grid">
      <MemberCard :member="member" class="user-page-card"/>
      <ActionCard
          info="En besked kan være for alle eller målrettet enkelte eller
        flere medlemmer."
          title="Beskeder"
      />
      <ActionCard
          info="En begivenhed bør indeholde dato, tid og lokation."
          title="Begivenheder"
      />
      <ActionCard
          info="Nyheder skal som minimum udfyldes med forfatter,
          overskrift, et kort resumé, og ikke mindst indhold."
          title="Nyhed"
      />
      <ActionCard
          info="Ret email addresse."
          linkTitle="Skift"
          title="E-Mail"
      />
      <ActionCard
          info="Ret nuværende profilbillede eller tilføj et til samlingen."
          linkTitle="Skift"
          title="Profilbillede"
      />
      <ActionCard
          info="Nulstil dit kodeord. Bør indeholde mindst 8 karaktere
                hvoraf mindst et af dem er et tal."
          linkTitle="Skift"
          title="Skift kodeord"
      />
      <ActionCard
          href="/nyBruger"
          info="Opret en ny bruger."
          linkTitle="Opret"
          title="Bruger"
      />
    </div>
    <InfoTiles :member="member" class="user-bottom-tiles"/>
  </div>
  <LoadIndicator v-else/>
</template>
<style lang="css" scoped>
h1 {
  white-space: nowrap;
  font-size: 2rem;
}

.user-page-cont {
  height: 100vh;
  width: 100vw;
}

.user-page-grid {
  display: grid;
  row-gap: 6px;
}

@media (orientation: portrait) or (max-width: 767px) {
  .user-page-grid {
    justify-self: center;
    padding: 1rem;
  }
}

@media (orientation: landscape) and (min-width: 768px) {
  .user-page-cont {
    display: grid;
    grid-template-rows: 1fr min-content;
    align-items: center;
  }

  .user-page-grid {
    justify-self: center;
    overflow: auto;
    padding: 1rem;
    height: min-content;
  }
}

@media (orientation: landscape) and (min-width: 1280px) {
  .user-page-grid {
    grid-template-columns: 1fr repeat(3, 300px);
    column-gap: 6px;
    width: 1300px;
  }

  .user-page-card {
    grid-row: span 3;
  }

  @keyframes sizeIn {
    from {
      width: 0;
      height: 0
    }
    to {
      width: 100%;
      height: 320px
    }
  }
}
</style>