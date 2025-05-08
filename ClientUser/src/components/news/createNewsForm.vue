<script setup>

import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";

const props = defineProps(["onCompleted","onCancelled"])

let title = ""
let shortContent = ""
let content = ""

async function handleCompleted(){
  const data = {title,shortContent,content}
  var result = await HttpClient.authPostRequest("/articles",data);
  if(props.onCompleted && result) props.onCompleted();
}

function handleCancel(){
  title = ""
  content = ""
  if(props.onCancelled) props.onCancelled();
}

</script>

<template>
  <div class="create-article-form" :onclick="cancel">
    <input placeholder="Titel" v-model="title"/>
    <textarea placeholder="Skriv et  kort resume af indholdet her" v-model="shortContent"/>
    <textarea placeholder="Skriv dit indhold af artiklen her" v-model="content" />
  </div>
  <div class="create-btn-group">
    <PushButton text="Fortryd" :onPushed="handleCancel"/>
    <PushButton text="Færdig" :onPushed="handleCompleted"/>
  </div>
</template>

<style scoped lang="css">
.create-article-form {
  border-radius: 3px;
  width: 100%;
  height: 384px;
  animation: growUp .5s ease-in-out;
  margin-top: 6px;
  display: grid;
  grid-template-rows: min-content 1fr 2fr min-content;
  row-gap: 9px;
}

.create-article-form>*{
  outline: none;
  padding: 9px;
  border-radius: 6px;
  resize: none;
}

.create-btn-group{
  display: flex;
  justify-content: end;
  column-gap: 9px;
}

@keyframes growUp {
  0% {height: 0;opacity: 0}
  50% {opacity: 0.05}
  90% {opacity: .1}
  100% {height: 384px;opacity: 1}
}
</style>