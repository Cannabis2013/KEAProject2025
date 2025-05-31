<script setup>

import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";
import {v4 as uuidv4} from "uuid";
import {imageAsBase64} from "@/services/Images/images.js";

const props = defineProps(["onCompleted", "onCancelled", "model"])

const compId = uuidv4()

const model = props.model ?? {
  id: -1,
  headline: "",
  shortContent: "",
  content: "",
  imageBlob: ""
}

async function handleCompleted() {
  const request = props.model ? HttpClient.authPatchRequest : HttpClient.authPostRequest
  const result = await request("/articles", model)
  if (props.onCompleted && result) props.onCompleted()
}

async function fetchBlob(uri) {
  const res = await fetch(uri)
  if (res.ok)
    return res.blob()
  return undefined
}

function blobToBase64(blob) {
  return new Promise((resolve, _) => {
    const reader = new FileReader();
    reader.onloadend = () => resolve(reader.result);
    reader.readAsDataURL(blob);
  });
}

async function handleFile(e){
  const file = e.target.files[0]
  model.imageBlob = await imageAsBase64(file)
}

const handleCancelRequest = props.onCancelled ?? function () {
}

</script>

<template>
  <div>
    <div class="create-article-form">
      <input placeholder="Titel" v-model="model.headline"/>
      <textarea placeholder="Skriv et  kort resume af indholdet her" v-model="model.shortContent"/>
      <textarea placeholder="Skriv dit indhold af artiklen her" v-model="model.content"/>
      <input accept=".png,.jpg,.jpeg" type="file" :onchange="handleFile" :id="'file-selector' + compId"  class="create-file-selector"/>
    </div>
    <div class="create-btn-group">
      <PushButton text="Fortryd" :onPushed="handleCancelRequest"/>
      <PushButton text="Færdig" :onPushed="handleCompleted"/>
    </div>
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

.create-article-form > * {
  outline: none;
  padding: 9px;
  border-radius: 6px;
  resize: none;
}

.create-btn-group {
  display: flex;
  justify-content: end;
  column-gap: 9px;
}

@keyframes growUp {
  0% {
    height: 0;
    opacity: 0
  }
  50% {
    opacity: 0.05
  }
  90% {
    opacity: .1
  }
  100% {
    height: 384px;
    opacity: 1
  }
}
</style>