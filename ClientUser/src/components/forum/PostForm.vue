<script setup>
import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";
import {onMounted} from "vue";
import {v4 as uuid} from "uuid";
import {ref} from "vue";
import EmojiSelector from "@/components/controls/EmojiSelector.vue";

const content = document.querySelector("#content")
const compUuid = uuid()
const compId = `createComp${compUuid}`
const textId = `textComp${compUuid}`
let textComp = null

const props = defineProps(["onCompleted", "onCancelled", "model", "topicId"]);

const model = ref(props.model ?? {
  id: -1,
  message: "",
  topicId: props.topicId ?? -1,
})

onMounted(function(){
  textComp = document.getElementById(textId)
  const createComp = document.getElementById(compId)
  let h = createComp.getBoundingClientRect().height
  let y = createComp.getBoundingClientRect().y + h
  console.log(`h: ${h}, y: ${y}`)
  setTimeout(() => content.scrollTo(0, y),501)
})

function insertEmoji(emoji) {
  textComp.focus()
  const charSize = emoji.length
  const begin = textComp.selectionStart
  const end = textComp.selectionEnd
  const msg = model.value.message
  model.value.message = msg.slice(0, begin) + emoji + msg.slice(end)
  setTimeout(() => textComp.setSelectionRange(begin + charSize, begin + charSize), 1)
}

async function handleCompleted() {
  if(model.value.message.length <= 0) return
  const request = props.model ? HttpClient.authPatchRequest : HttpClient.authPostRequest
  const result = await request("/post", model.value)
  if (props.onCompleted && result) props.onCompleted()
}

const handleCancelRequest = props.onCancelled ?? function () {
}
</script>

<template>
  <div :id="compId">
    <EmojiSelector style="margin-bottom: 9px;" :onSelect="insertEmoji"/>
    <textarea 
        :id="textId"
        class="post-msg-input fillw" 
        placeholder="Skriv dit indhold her" 
        v-model="model.message">
    </textarea>
    <div class="post-form-buttons">
      <PushButton 
          text="Fortryd" 
          :onPushed="handleCancelRequest"
      />
      <PushButton 
          text="Færdig" 
          :onPushed="handleCompleted"
      />
    </div>
  </div>
</template>

<style scoped lang="css">
.post-msg-input {
  border-radius: 3px;
  height: 384px;
  animation: growUp 500ms ease-in-out;
}

.post-msg-input > * {
  outline: none;
  padding: 9px;
  border-radius: 6px;
  resize: none;
}

.post-form-buttons {
  display: flex;
  justify-content: end;
  column-gap: 9px;
}


@keyframes growUp {
  0% {
    height: 0;
    opacity: 0
  }
  100% {
    height: 384px;
    opacity: 1
  }
}
</style>