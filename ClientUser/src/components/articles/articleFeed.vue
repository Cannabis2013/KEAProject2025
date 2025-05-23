<script setup>
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
    .catch(error => {
      failed.value = true;
      loading.value = false;
    })

function cardModel(article){
  return {
    title: article.headline,
    author: article.author,
    content: article.shortContent,
    image: "/news.png"
  }
}

</script>
<template>
  <div v-if="!loading" class="home-col-section">
    <h1 class="home-col-title">Nyheder</h1>
    <div class="home-col">
      <p v-if="failed">
        Kunne ikke indlæses. Serveren er højst sandsynlig nede.
      </p>
      <div class="news-tile" v-for="article in articles">
        <img class="tile-icon" src="/news.png"/>
        <div class="news-tile-upper">
          <p class="news-tile-head">{{ article.title }}</p>
          <div class="news-tile-meta">
            <p class="news-tile-author">Af {{ article.author }}</p>
            <p class="news-tile-date">{{ article.date }}</p>
          </div>
        </div>
        <p class="news-tile-preview">{{ article.content }}</p>
      </div>
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

.news-tile {
  display: grid;
  grid-template-columns: 80px 1fr;
  grid-template-rows: min-content 1fr min-content;
  column-gap: 12px;
  background-color: white;
  padding: 12px;
  width: 100%;
  height: min-content;
  border-radius: 6px;
}

.tile-icon {
  grid-row: span 3;
  height: 80px;
}

.news-tile-upper {
  border-bottom: 1px solid black;
}

.news-tile > p,
.news-tile-upper > p,
.news-tile-meta > p {
  margin: 0;
  color: black !important;
}

.news-tile-meta {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.news-tile-head {
  font-size: 1.25rem;
  line-height: 1.25rem;
  font-weight: bold;
}

.news-tile-author {
  font-style: italic;
  font-size: .7rem;
}

.news-tile-date {
  font-style: italic;
  font-size: .7rem;
}

.news-tile-preview {
  margin-top: 16px !important;
  font-size: 0.8rem;
  white-space: pre-wrap;
}

.news-tile-link {
  font-size: .8rem;
  width: min-content;
  white-space: nowrap;
}
</style>