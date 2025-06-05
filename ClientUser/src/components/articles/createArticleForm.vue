<script setup>

import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";
import {v4 as uuid} from "uuid";
import {imageAsBase64} from "@/services/Images/images.js";
import {onMounted} from "vue";
import Scroll from "@/services/scroll/itemScroll.js"

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

onMounted(function () {
  setTimeout(function(){
    const createComp = document.getElementById(compId)
    const rect = createComp.getBoundingClientRect()
    Scroll(rect.height, rect.y)
  }, 501)
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
  <div :id="compId" class="article-form-cont">
    <input placeholder="Titel" class="fillw" v-model="model.headline"/>
    <textarea class="" placeholder="Skriv et  kort resume af indholdet her" v-model="model.shortContent"/>
    <textarea class="create-content-cont" placeholder="Skriv dit indhold af artiklen her" v-model="model.content"/>
    <input accept=".png,.jpg,.jpeg" type="file" :onchange="handleFile" :id="'file-selector' + compId"
           class="create-file-selector"/>
    <div class="create-btn-group">
      <PushButton text="Fortryd" :onPushed="handleCancelRequest"/>
      <PushButton text="Færdig" :onPushed="handleCompleted"/>
    </div>
  </div>
</template>

<style scoped lang="css">
.article-form-cont {
  display: grid;
  grid-template-rows: min-content 128px 1fr 64px;
  height: 768px;
  animation: growUp .5s ease-in-out;
  z-index: 333;
}

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