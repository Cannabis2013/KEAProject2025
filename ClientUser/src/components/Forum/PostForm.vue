<script setup>
import PushButton from "@/components/controls/PushButton.vue";
import HttpClient from "@/services/http/httpClient.js";

const props = defineProps(["onCompleted", "onCancelled", "model", "topicId"]);

const model = props.model ?? {
  id: -1,
  message: "",
  topicId: props.topicId ?? -1,
}

async function handleCompleted() {
  const request = props.model ? HttpClient.authPatchRequest : HttpClient.authPostRequest
  const result = await request("/post", model)
  if (props.onCompleted && result) props.onCompleted()
}

const handleCancelRequest = props.onCancelled ?? function () {
}

</script>

<template>
  <div>
    <textarea class="post-msg-input" placeholder="Skriv dit indhold her" v-model="model.message"/>
    <div class="post-form-buttons">
      <PushButton text="Fortryd" :onPushed="handleCancelRequest"/>
      <PushButton text="Færdig" :onPushed="handleCompleted"/>
    </div>
  </div>
</template>

<style scoped lang="css">
.post-msg-input {
  border-radius: 3px;
  width: 100%;
  height: 384px;
  animation: growUp .5s ease-in-out;
  margin-top: 6px;
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