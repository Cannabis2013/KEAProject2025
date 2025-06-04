<script setup>
import {ref} from "vue";
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import HttpClient from "@/services/http/httpClient.js";
import ArticleCard from "@/components/home/Feed/ArticleCard.vue"
import {useRouter} from "vue-router";
import TopicCard from "@/components/home/Feed/TopicCard.vue";

const articles = ref([])
const activeTopics = ref([])
const loading = ref(false)

async function fetchData() {
  loading.value = true
  articles.value = await HttpClient.authGetRequest("/articles/count/5") ?? []
  activeTopics.value = await HttpClient.authGetRequest("/topic/active/5") ?? []
  loading.value = false
}

fetchData()

</script>
<template>
  <div v-if="!loading" class="home-col-section">
    <h2 class="home-col-title">Seneste nyheder</h2>
    <div class="home-col">
      <p v-if="articles.length <= 0">Ingen nyheder at vise</p>
      <ArticleCard v-for="article in articles" :model="article"/>
    </div>
    <h2 class="home-col-title">Seneste debatter</h2>
    <div class="home-col">
      <p v-if="activeTopics.length <= 0">Ingen aktive tråde</p>
      <TopicCard v-for="topic in activeTopics" :model="topic"/>
    </div>
  </div>
  <LoadIndicator v-else/>
</template>
<style lang="css" scoped>
.home-col {
  display: grid;
  align-content: start;
  row-gap: 12px;
}

@media only screen and (min-width: 768px) {
  .home-col-section {
    display: grid;
    grid-template-rows: min-content 1fr;
    grid-template-columns: 384px 384px;
    grid-auto-flow: column;
    column-gap: 1rem;
    width: min-content;
  }
  
  .feed-topic-tile{
    padding: 12px;
    background-color: white;
    border-radius: 6px;
  }
  
  .feed-topic-tile>*{
    color: black
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