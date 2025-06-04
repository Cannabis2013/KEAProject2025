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
const articleToUpdate = ref(null)
const updateId = ref(null)
let pageIndex = 0
const pageSize = 20
const moreAvailable = ref(true)

async function fetchArticles() {
  const fetched = await HttpClient.authGetRequest(`/articles/${pageIndex}/${pageSize}`)
  if (!fetched) return
  pageIndex += pageSize
  moreAvailable.value = fetched.length > 0
  articles.value.push(...fetched)
}

fetchArticles()

function showForm() {
  formVisible.value = !formVisible.value
}

async function createCompleted() {
  isLoading.value = true
  pageIndex = 0
  articles.value = []
  fetchArticles()
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
  articleToUpdate.value = {
    id: fetched.id,
    headline: fetched.headline,
    shortContent: fetched.shortContent,
    content: fetched.content
  }
  updateId.value = fetched.id
}

function hideForm() {
  updateId.value = null
  articleToUpdate.value = null
  formVisible.value = false
}

</script>
<template>
  <LoadIndicator v-if="isLoading"/>
  <div v-else class="fluid-container">
    <h1>Nyheder</h1>
    <br>
    <PushButton class="sticktop article-create-btn" v-if="!formVisible" text="Opret nyhed" :onPushed="showForm"/>
    <CreateNewsForm
        v-if="formVisible"
        class="create-form-cont sticktop"
        :onCancelled="hideForm"
        :onCompleted="createCompleted"
    />
    <div v-for="article in articles">
      <CreateNewsForm
          v-if="updateId === article.id"
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
    <PushButton v-if="moreAvailable" style="margin-bottom: 1.5rem;" class="center" :onPushed="fetchArticles"
                text="Hent flere.."/>
  </div>
</template>
<style lang="css" scoped>
.create-form-cont {
  background-color: rgb(63, 63, 63, 1);
  padding: 9px;
  border-radius: 6px;
}

.article-create-btn {
  float: left;
}
</style>