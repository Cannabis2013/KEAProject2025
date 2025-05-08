<script setup>

import HttpClient from "@/services/http/httpClient.js"
import {ref} from "vue"
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import CreateNewsForm from "@/components/news/createNewsForm.vue";
import PushButton from "@/components/controls/PushButton.vue";

const articles = ref([])
const isLoading = ref(false)
const showModal = ref(false)

function fetchArticles() {
  HttpClient.authGetRequest("/articles")
      .then(a => {
        articles.value = a
      })
}

fetchArticles()

function handleFormRequest() {
  showModal.value = !showModal.value
}

function createCompleted() {
  showModal.value = false
  fetchArticles()
}

function createCancelled() {
  showModal.value = false
}

function toDate(dateAsString) {
  const date = new Date(dateAsString)
  const day = date.getDay() < 10 ? '0' + date.getDate() : date.getDate()
  const month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1
  const year = date.getFullYear()
  return `${day}-${month}-${year}`
}

</script>
<template>
  <LoadIndicator v-if="isLoading"/>
  <div v-else class="fluid-container">
    <h1 class="page-subh">Nyheder</h1>
    <PushButton text="Opret nyhed" :onPushed="handleFormRequest"/>
    <br>
    <CreateNewsForm v-if="showModal" :onCancelled="createCancelled" :onCompleted="createCompleted"/>
    <br>
    <div v-for="article in articles" class="article-cont">
      <div class="article-head">
        <h2>{{ article.headline }}</h2>
        <p style="font-size: .75rem">Skrevet af: {{ article.author }}</p>
      </div>
      <p class="article-content">{{ article.content }}</p>
      <p>Skrevet den {{ toDate(article.created) }}</p>
    </div>
  </div>
</template>
<style lang="css" scoped>
.page-head {
  font-size: 2rem;
  font-weight: bold;
}

.article-cont {
  margin-bottom: 1.5rem;
}

.article-head {
  border-bottom: 1px solid lightskyblue;
}

.article-head > * {
  white-space: nowrap;
  height: min-content;
}

.article-content {
  margin: 6px 0 6px 0;
  resize: none;
  white-space: break-spaces;
  width: 100%;
  height: auto;
  background-color: transparent;
  outline: none;
  border: none;
  color: lightskyblue;
  font-size: 1rem;
  font-family: "Poppins", sans-serif;
}
</style>