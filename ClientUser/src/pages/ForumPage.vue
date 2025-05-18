<script setup lang="ts">

import {ref} from "vue";
import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient"
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import TopicForm from "@/components/Forum/TopicForm.vue";
import {useRouter} from "vue-router";

const router = useRouter();
const topics = ref([])
const pageSize = 20
let pageIndex = 0
const isLoading = ref(false)
const formVisible = ref(false);

function showForm(){
  formVisible.value = true;
}

function hideForm(){
  formVisible.value = false;
}

async function fetchTopics(index, count) {
  isLoading.value = true;
  const fetched = await HttpClient.authGetRequest(`/topic/${index}/${count}`)
  topics.value.push(...(fetched ?? []))
  isLoading.value = false;
}

fetchTopics(pageIndex, pageSize)

function toTopicPage(id){
  router.push(`/topic/${id}`)
}

async function updateTopics(){
  isLoading.value = true
  topics.value = await HttpClient.authGetRequest(`/topic/${pageIndex}/${pageSize}`)
  hideForm()
  isLoading.value = false
}

</script>

<template>
  <LoadIndicator v-if="isLoading"/>
  <div v-else class="fluid-container">
    <h1>Forum</h1>
    <PushButton v-if="!formVisible" text="Ny tråd" :onPushed="showForm" />
    <TopicForm v-else :onCancelled="hideForm" :onCompleted="updateTopics"/>
    <br>
    <div class="forum-col forum-header">
      <p>Titel</p>
      <p>Kategori</p>
      <p>Poster</p>
      <p>Aktivitet</p>
    </div>
    <div v-for="topic in topics" class="forum-col forum-item" :onclick="() => toTopicPage(topic.id)">
      <p>{{ topic.title }}</p>
      <p>{{ topic.category }}</p>
      <p>{{ topic.postsCount }}</p>
      <p>{{ topic.lastPoster }}</p>
    </div>
    <br>
    <PushButton class="horizontal-center" text="Hent flere.."/>
  </div>
</template>

<style scoped lang="css">
.forum-col {
  display: grid;
  grid-template-columns:1fr 144px 80px 192px;
}

.forum-header {
  border-bottom: 3px solid lightskyblue;
  padding-bottom: 3px;
}

.forum-header>*{
  font-size: 20px;
}

.forum-item{
  border-bottom: 1px dotted lightskyblue;
  padding: 9px 0 9px 0;
  cursor: pointer;
}

.forum-item:hover{
  background-color: rgba(255,255,255,.1);
}

.forum-item>*{
  margin:0;
  padding:0;
  font-size: 20px;
  white-space: nowrap;
}

</style>