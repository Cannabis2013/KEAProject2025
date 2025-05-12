<script setup>
import articleCard from "./articleCard.vue";
import {ref} from "vue";
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import HttpClient from "@/services/http/httpClient.js";

const articles = ref([])
const loading = ref(true)
const failed = ref(false)
const fetched = ref(false)

HttpClient.authGetRequest("/articles/count/5")
    .then(fetchedArticles => {
      articles.value = fetchedArticles;
      fetched.value = true
      loading.value = false
    })

setTimeout(() => {
  if (fetched.value)
    return
  failed.value = true;
  loading.value = false;
}, 1500)

</script>
<template>
  <div v-if="!loading" class="home-col-section">
    <h1 class="home-col-title">Nyheder</h1>
    <div class="home-col">
      <p v-if="failed">
        Kunne ikke indlæses. Serveren er højst sandsynlig nede.
      </p>
      <articleCard v-for="article in articles" :article="article"/>
    </div>
  </div>
  <LoadIndicator v-else/>
</template>
<style lang="css" scoped>
.home-col{
  display: grid;
  align-content: start;
  row-gap: 12px;
}

@media only screen and (min-width: 768px) {
  .home-col-section {
    display: grid;
    grid-template-rows: min-content 1fr;
    width: 100%;
    padding: 0;
    overflow: hidden;
  }

  .home-col-title {
    font-size: 1.5rem;
    border-bottom: 6px solid var(--al-blue);
  }

  .home-col {
    overflow: scroll;
    padding: 12px 0 12px 0;
  }
}
</style>