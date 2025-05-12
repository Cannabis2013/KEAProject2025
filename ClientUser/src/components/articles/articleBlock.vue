<script setup>

import PushButton from "@/components/controls/PushButton.vue";

const props = defineProps(["model","onDelete","onUpdate"])
const article = props.model

const handleDelete = props.onDelete ?? function(){}
const handleUpdate = props.onUpdate ?? function(){}

function toDate(dateAsString) {
  const date = new Date(dateAsString)
  const day = date.getDay() < 10 ? '0' + date.getDate() : date.getDate()
  const month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1
  const year = date.getFullYear()
  return `${day}-${month}-${year}`
}

</script>

<template>
  <div class="article-cont">
    <div class="article-head">
      <div>
        <h2>{{ article.headline }}</h2>
        <p style="font-size: .75rem">Af {{ article.author }} // {{ toDate(article.created) }}</p>
      </div>
      <div v-if="article.isOwner" class="button-group">
        <PushButton :onPushed="() => handleUpdate(article.id)" text="Opdater"/>
        <PushButton :onPushed="() => handleDelete(article.id)" text="Slet"/>
      </div>
    </div>
    <p class="article-content">{{ article.content }}</p>
  </div>
</template>

<style scoped lang="css">
.article-cont {
  margin-bottom: 1.5rem;
  background-color: rgba(255,255,255,.1);
  border-radius: 9px;
  padding: 9px;
}

.article-head {
  display: grid;
  grid-template-columns: 1fr min-content ;
}

.article-head > * {
  white-space: nowrap;
  height: min-content;
}

.button-group{
  display: flex;
  column-gap: 9px;
}

.article-content {
  margin: 6px 0 6px 0;
  resize: none;
  white-space: break-spaces;
  width: 100%;
  height: auto;
  background-color: transparent;
  outline: none;
  border: none;
  color: lightskyblue;
  font-size: 1rem;
  font-family: "Poppins", sans-serif;
}
</style>