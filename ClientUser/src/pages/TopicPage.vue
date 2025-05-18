<script setup>
import HttpClient from "@/services/http/httpClient.js";
import {ref} from "vue";
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import {useRoute, useRouter} from "vue-router";
import PostCard from "@/components/Forum/PostCard.vue";
import PushButton from "@/components/controls/PushButton.vue";
import PostForm from "@/components/Forum/PostForm.vue";

const content = document.querySelector("#content");
const router = useRouter();
const route = useRoute()
const topicId = route.params.id
const isLoading = ref(true)
const formVisible = ref(false)
const topic = ref(null)
const updateId = ref(null)
const postToUpdate = ref(null)
const posts = ref([])
const pageIndex = 0
const pageSize = 20;

async function fetchTopic() {
  topic.value = await HttpClient.authGetRequest(`/topic/${topicId}`)
  const fetched = await HttpClient.authGetRequest(`/post/${topicId}/${pageIndex}/${pageSize}`)
  posts.value.push(...(fetched ?? []))
  isLoading.value = false
}

fetchTopic()

function showForm() {
  formVisible.value = true
}

function scrollToForm(elementY){
  content.scrollTo(0, elementY)
}

function hideForm() {
  formVisible.value = false
  updateId.value = null
  postToUpdate.value = null
}

async function deletePost(id) {
  if (!confirm("Sikker?")) return;
  isLoading.value = true
  const result = await HttpClient.authDeleteRequest(`/post/${id}`)
  if (result)
    posts.value = posts.value.filter(post => post.id !== id)
  isLoading.value = false
}

async function updatePosts() {
  isLoading.value = true
  const fetched = await HttpClient.authGetRequest(`/post/${topicId}/${pageIndex}/${pageSize}`)
  posts.value = fetched ?? []
  hideForm()
  isLoading.value = false
}

async function updatePost(id) {
  const fetched = await HttpClient.authGetRequest(`/post/${id}`)
  postToUpdate.value = {
    id: fetched.id,
    message: fetched.message,
    topicId: fetched.topicId
  }
  updateId.value = fetched.id
}

</script>

<template>
  <LoadIndicator v-if="isLoading"/>
  <div class="fluid-container" v-else>
    <PushButton :onPushed="() => router.back()" text="Tilbage"/>
    <h2>{{ topic.title }}</h2>
    <br>
    <div class="topic-msg-cont">
      <p class="topic-author">{{ topic.author }}</p>
      <p class="topic-message">{{ topic.initialMessage }}</p>
      <br>
    </div>
    <div v-for="post in posts">
      <PostCard v-if="updateId != post.id" class="horizontal-center" :onUpdate="updatePost" :post="post"
                :onDelete="deletePost"/>
      <PostForm v-else :onMounted="scrollToForm" :model="postToUpdate" :topicId="topicId" :onCancelled="hideForm" :onCompleted="updatePosts"/>
    </div>
    <div v-if="!formVisible" class="topic-btn-controls">
      <PushButton text="Hent flere svar"/>
      <PushButton :onPushed="showForm" text="Opret svar"/>
    </div>
    <PostForm v-else :onMounted="scrollToForm" :topicId="topicId" :onCancelled="hideForm" :onCompleted="updatePosts"/>
    <br>
  </div>

</template>

<style scoped lang="css">
.topic-msg-cont {
  border-bottom: 2px solid white;
  margin-bottom: 16px;
}

.topic-author {
  font-weight: bold;
  border-radius: 6px;
  width: min-content;
  white-space: nowrap;
  font-size: 1rem;
  padding: 6px;
  background-color: #2f2f2f;
  margin-bottom: 6px;
}

.topic-message {
  font-size: 1rem;
  line-height: 1.5rem;
}

.topic-ft-cont {
  display: flex;
  column-gap: 16px;
  padding-bottom: 9px;
}

.topic-ft-cont > p {
  font-size: 1rem;
}

.posts-cont {
  height: 128px;
  overflow: auto;
}

.topic-btn-controls {
  display: flex;
  column-gap: 1rem;
  justify-content: center;
}
</style>