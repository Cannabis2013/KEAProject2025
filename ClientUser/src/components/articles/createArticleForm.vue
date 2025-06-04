<script setup>

import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";
import {v4 as uuid} from "uuid";
import {imageAsBase64} from "@/services/Images/images.js";
import {onMounted} from "vue";

const props = defineProps(["onCompleted", "onCancelled", "model"])

const model = props.model ?? {
  id: -1,
  headline: "",
  shortContent: "",
  content: "",
  imageBlob: ""
}

const compUuid = uuid()
const compId = `articleCreate${compUuid}`
const content = document.querySelector("#content")

onMounted(function () {
  const createComp = document.getElementById(compId)
  const wHeight = window.innerHeight
  const rect = createComp.getBoundingClientRect()
  const height = rect.height
  const y = rect.y
  let sy = window.scrollY
  if(y <= wHeight / 2)
    sy -= wHeight - y - height
  else
    sy += y - (wHeight - height)
  setTimeout(() => window.scrollTo(0, sy), 501)
})

async function handleCompleted() {
  const request = props.model ? HttpClient.authPatchRequest : HttpClient.authPostRequest
  const result = await request("/articles", model)
  if (props.onCompleted && result) props.onCompleted()
}

async function handleFile(e) {
  const file = e.target.files[0]
  model.imageBlob = await imageAsBase64(file)
}

const handleCancelRequest = props.onCancelled ?? function () {
}

</script>

<template>
  <div :id="compId">
    <input placeholder="Titel" class="fillw" v-model="model.headline"/>
    <textarea class="fillw" placeholder="Skriv et  kort resume af indholdet her" v-model="model.shortContent"/>
    <textarea class="fillw" placeholder="Skriv dit indhold af artiklen her" v-model="model.content"/>
    <input accept=".png,.jpg,.jpeg" type="file" :onchange="handleFile" :id="'file-selector' + compId"
           class="create-file-selector"/>
    <div class="create-btn-group">
      <PushButton text="Fortryd" :onPushed="handleCancelRequest"/>
      <PushButton text="Færdig" :onPushed="handleCompleted"/>
    </div>
  </div>
</template>

<style scoped lang="css">
textarea, input {
  outline: none;
  padding: 9px;
  border-radius: 6px;
  resize: none;
  margin-bottom: 6px;
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