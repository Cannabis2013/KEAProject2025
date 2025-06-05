<script setup>
import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";
import {ref} from "vue";

const props = defineProps(["onCompleted", "onCancelled", "model"]);

const model = props.model != null ? toCopy(props.model) : {
  topicId: "",
  title: "",
  category: "",
  initialMessage: ""
}

const processing = ref(false)

function toCopy(topic){
  return {
    topicId: topic.id,
    title: topic.title,
    initialMessage: topic.initialMessage,
    category: topic.category
  }
}

async function handleCompleted() {
  processing.value = true
  const request = props.model ? HttpClient.authPatchRequest : HttpClient.authPostRequest
  const result = await request("/topic", model)
  if (props.onCompleted && result) props.onCompleted()
  processing.value = false
}

const handleCancelRequest = props.onCancelled ?? function () {
}

</script>

<template>
  <div>
    <div class="create-topic-form">
      <input placeholder="Titel" v-model="model.title"/>
      <select class="category-selector" v-model="model.category">
        <option value="" style="color: grey">Vælg kategori</option>
        <option value="Kampe">Kampe</option>
        <option value="Stadion">Stadion</option>
        <option value="Generelt">Generelt</option>
        <option value="Sladder">Sladder</option>
        <option>Salg</option>
      </select>
      <textarea placeholder="Skriv hvad din tråd handler om" v-model="model.initialMessage"/>
    </div>
    <div class="forum-form-buttons">
      <PushButton text="Fortryd" :onPushed="handleCancelRequest"/>
      <PushButton v-if="!processing" text="Færdig" :onPushed="handleCompleted"/>
    </div>
  </div>
</template>

<style scoped lang="css">
.create-topic-form {
  border-radius: 3px;
  width: 100%;
  height: 384px;
  animation: growUp .5s ease-in-out;
  margin-top: 6px;
  display: grid;
  grid-template-rows: min-content min-content 2fr min-content;
  row-gap: 9px;
}

.category-selector{
  font-size: 20px;
}

.create-topic-form > * {
  outline: none;
  padding: 9px;
  border-radius: 6px;
  resize: none;
}

.forum-form-buttons {
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