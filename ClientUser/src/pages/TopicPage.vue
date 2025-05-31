<script setup>
import HttpClient from "@/services/http/httpClient.js";
import {ref} from "vue";
import LoadIndicator from "@/components/loading/LoadIndicator.vue";
import {useRoute, useRouter} from "vue-router";
import PostItem from "@/components/Forum/PostItem.vue";
import PushButton from "@/components/controls/PushButton.vue";
import PostForm from "@/components/Forum/PostForm.vue";
import {toDate, toDateTime} from "@/services/date/dateFormatting.js";
import TopicForm from "@/components/Forum/TopicForm.vue";

const router = useRouter()
const route = useRoute()
const topicId = route.params.id
const isLoading = ref(false)
const topicFormVisible = ref(false)
const postFormVisible = ref(false)
const topic = ref(null)
const updateId = ref(null)
const postToUpdate = ref(null)
const posts = ref([])
const pageIndex = 0
const pageSize = 20;

async function fetchTopic() {
  isLoading.value = true
  topic.value = await HttpClient.authGetRequest(`/topic/${topicId}`)
  const fetched = await HttpClient.authGetRequest(`/post/${topicId}/${pageIndex}/${pageSize}`)
  posts.value.push(...(fetched ?? []))
  isLoading.value = false
}

fetchTopic()

function showTopicForm(status){
  topicFormVisible.value = status
}

function showPostForm() {
  postFormVisible.value = true
}

function hidePostForm() {
  postFormVisible.value = false
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
  hidePostForm()
  isLoading.value = false
}

async function updateCompleted(){
  posts.value = []
  await fetchTopic()
  showTopicForm(false)
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

async function deleteTopic() {
  if(!confirm("Sure?")) return
  const result = await HttpClient.authDeleteRequest(`/topic/${topicId}`)
  if(result) router.push("/topics")
}

</script>

<template>
  <LoadIndicator v-if="isLoading"/>
  <div class="fluid-container" v-else>
    <PushButton :onPushed="() => router.push('/topics')" text="Til debat"/>
    <div>
      <h2 style="font-size: 2.5rem">{{ topic.title }}</h2>
      <p> 
        Kategori: {{ topic.category }}
        <br>
        Oprettet: {{ toDateTime(topic.created) }}
        <br>
        Oprettet af: {{ topic.author }}
      </p>
      <img v-if="topic.isOwner" class="topic-controls-img" style="margin-right: 6px;" src="/edit.png" :onclick="() => showTopicForm(true)"/>
      <img v-if="topic.isOwner" class="topic-controls-img" src="/trashcan.png" :onclick="deleteTopic"/>
    </div>
    <TopicForm v-if="topicFormVisible" :model="topic" :onCompleted="updateCompleted" :onCancelled="() => showTopicForm(false)"/>
    <br/>
    <p class="topic-init-msg">{{topic.initialMessage}}</p>
    <br>
    <div v-for="post in posts">
      <PostItem v-if="updateId != post.id" class="center" :onUpdate="updatePost" :post="post"
                :onDelete="deletePost"/>
      <PostForm v-else :model="postToUpdate" :topicId="topicId" :onCancelled="hidePostForm"
                :onCompleted="updatePosts"/>
    </div>
    <br>
    <div v-if="!postFormVisible" class="topic-btn-controls">
      <PushButton text="Hent flere svar"/>
      <PushButton :onPushed="showPostForm" text="Opret svar"/>
    </div>
    <PostForm v-else :topicId="topicId" :onCancelled="hidePostForm" :onCompleted="updatePosts"/>
    <br>
  </div>
</template>

<style scoped lang="css">
.topic-controls-img {
  width: 20px;
  height: 20px;
  cursor: pointer;
}

.topic-init-msg{
  width: 100%;
  padding-bottom: 9px;
  border-bottom: 3px solid lightskyblue;
  font-size: 24px;
  white-space: break-spaces;
}

.topic-msg-cont {
  border-bottom: 2px solid lightskyblue;
  margin-bottom: 1rem;
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
  line-height: 1rem;
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