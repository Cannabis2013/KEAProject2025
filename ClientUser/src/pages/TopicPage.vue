<script setup>
import HttpClient from "@/services/http/httpClient.js";
import {ref} from "vue";
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import {useRoute} from "vue-router";
import PostCard from "@/components/Forum/PostCard.vue";
import PushButton from "@/components/controls/PushButton.vue";
import PostForm from "@/components/Forum/PostForm.vue";

const route = useRoute()
const topicId = route.params.id
const isLoading = ref(true)
const formVisible = ref(false)
const topic = ref(null)
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

function showForm(){
  formVisible.value = true
}

function hideForm(){
  formVisible.value = false
}

async function updatePosts(){
  isLoading.value = true
  const fetched = await HttpClient.authGetRequest(`/post/${topicId}/${pageIndex}/${pageSize}`)
  posts.value = fetched ?? []
  isLoading.value = false
  formVisible.value = false
}

</script>

<template>
  <LoadIndicator v-if="isLoading"/>
  <div class="fluid-container" v-else>
    <h2>{{ topic.title }}</h2>
    <br>
    <div class="topic-msg-cont">
      <p class="topic-author">{{ topic.author }}</p>
      <p class="topic-message">{{ topic.initialMessage }}</p>
      <br>
    </div>
    <PostCard class="horizontal-center" v-for="post in posts" :post="post"/>
    <div v-if="!formVisible" class="topic-btn-controls">
      <PushButton text="Hent flere svar"/>
      <PushButton  :onPushed="showForm" text="Opret svar"/>
    </div>
    <PostForm v-else :topicId="topicId" :onCancelled="hideForm" :onCompleted="updatePosts"/>
  </div>

</template>

<style scoped lang="css">
.topic-msg-cont {
  border-bottom: 2px solid white;
  margin-bottom: 16px;
}

.topic-author{
  font-weight: bold;
  border-radius: 6px;
  width: min-content;
  white-space: nowrap;
  font-size: 1rem;
  padding: 6px;
  background-color: #2f2f2f;
  margin-bottom: 6px;
}

.topic-message{
  font-size: 1.25rem;
}

.topic-ft-cont{
  display: flex;
  column-gap: 16px;
  padding-bottom: 9px;
}

.topic-ft-cont > p {
  font-size: 14px;
  line-height: 14px;
}

.topic-btn-controls{
  display: flex;
  column-gap: 16px;
  justify-content: center;
}
</style>