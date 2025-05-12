<script setup>

import HttpClient from "@/services/http/httpClient.js"
import {ref} from "vue"
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import CreateNewsForm from "@/components/articles/createArticleForm.vue";
import PushButton from "@/components/controls/PushButton.vue";
import articleBlock from "@/components/articles/articleBlock.vue";

const articles = ref([])
const isLoading = ref(false)
const showForm = ref(false)
const article = ref(null)

function fetch() {
  HttpClient.authGetRequest("/articles")
      .then(fetched => {
        articles.value = fetched
      })
}

fetch()

function handleFormRequest() {
  showForm.value = !showForm.value
}

async function createCompleted() {
  isLoading.value = true
  articles.value = await HttpClient.authGetRequest("/articles")
  showForm.value = false
  isLoading.value = false
}

async function deleteArticle(id) {
  if (!confirm("Sure?")) return
  isLoading.value = true
  await HttpClient.authDeleteRequest(`/articles/${id}`)
  articles.value = await HttpClient.authGetRequest("/articles")
  isLoading.value = false
}

async function updateArticle(id) {
  const fetched = await HttpClient.authGetRequest(`/articles/${id}`)
  article.value = {
    id: fetched.id,
    headline: fetched.headline,
    shortContent: fetched.shortContent,
    content: fetched.content
  }
  showForm.value = true
}

function createCancelled() {
  showForm.value = false
}

</script>
<template>
  <LoadIndicator v-if="isLoading"/>
  <div v-else class="fluid-container">
    <h1 class="page-subh">Nyheder</h1>
    <br>
    <PushButton text="Opret nyhed" :onPushed="handleFormRequest"/>
    <CreateNewsForm v-if="showForm" :onCancelled="createCancelled" :onCompleted="createCompleted" :model="article"/>
    <br>
    <articleBlock v-for="article in articles" :onUpdate="updateArticle" :onDelete="deleteArticle" :model="article"/>
  </div>
</template>
<style lang="css" scoped>
.page-head {
  font-size: 2rem;
  font-weight: bold;
}
</style>