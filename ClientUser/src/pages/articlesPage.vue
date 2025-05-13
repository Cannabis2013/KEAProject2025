<script setup>
import HttpClient from "@/services/http/httpClient.js"
import {ref} from "vue"
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import CreateNewsForm from "@/components/articles/createArticleForm.vue";
import PushButton from "@/components/controls/PushButton.vue";
import articleBlock from "@/components/articles/articleBlock.vue";

const articles = ref([])
const isLoading = ref(false)
const formVisible = ref(false)
const article = ref(null)
const updateId = ref(null)
let lastIndex = 0
const count = 20
const moreAvailable = ref(true)
function fetch(index, fetchCount) {
  HttpClient.authGetRequest(`/articles/${index}/${fetchCount}`)
      .then(fetched => {
        articles.value.push(...fetched)
      })
}

fetch(lastIndex, count)

function fetchMore() {
  lastIndex += count
  fetch(lastIndex, count)
  moreAvailable.value = articles.value.length % 20 == 0
}

function showForm() {
  formVisible.value = !formVisible.value
}

async function createCompleted() {
  isLoading.value = true
  articles.value = await HttpClient.authGetRequest("/articles")
  hideForm()
  isLoading.value = false
}

async function deleteArticle(id) {
  if (!confirm("Sure?")) return
  isLoading.value = true
  await HttpClient.authDeleteRequest(`/articles/${id}`)
  articles.value = articles.value.filter(a => a.id !== id)
  setTimeout(() => isLoading.value = false, 1000)
}

async function updateArticle(id) {
  const fetched = await HttpClient.authGetRequest(`/articles/${id}`)
  article.value = {
    id: fetched.id,
    headline: fetched.headline,
    shortContent: fetched.shortContent,
    content: fetched.content
  }
  updateId.value = fetched.id
}

function hideForm() {
  updateId.value = null
  article.value = null
  formVisible.value = false
}

</script>
<template>
  <LoadIndicator v-if="isLoading"/>
  <div v-else class="fluid-container">
    <h1 class="page-subh">Nyheder</h1>
    <br>
    <PushButton v-if="!formVisible" text="Opret nyhed" :onPushed="showForm"/>
    <CreateNewsForm
        v-else class="create-form-cont"
        :onCancelled="hideForm"
        :onCompleted="createCompleted"
    />
    <br>
    <div v-for="article in articles">
      <CreateNewsForm
          v-if="updateId === article.id"
          style="margin-bottom: 1.5rem;"
          :onCancelled="hideForm"
          :onCompleted="createCompleted"
          :model="article"
      />
      <articleBlock
          v-else
          :onUpdate="updateArticle"
          :onDelete="deleteArticle"
          :model="article"
      />
    </div>
    <PushButton v-if="moreAvailable" style="margin-bottom: 1.5rem;" class="horizontal-center" :onPushed="fetchMore" text="Hent flere.."/>
  </div>
</template>
<style lang="css" scoped>
.page-head {
  font-size: 2rem;
  font-weight: bold;
}

.create-form-cont {
  position: sticky;
  top: 0;
  left: 0;
  background-color: rgb(63, 63, 63, .8);
  padding: 9px;
  border-radius: 6px;
}
</style>