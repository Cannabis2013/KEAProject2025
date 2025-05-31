<script setup>
import {toDateTime, toLetterDate} from "@/services/date/dateFormatting.js";

const props = defineProps(["model","onDelete","onUpdate"])
const article = props.model

const handleDelete = props.onDelete ?? function(){}
const handleUpdate = props.onUpdate ?? function(){}

</script>

<template>
  <div class="article-cont">
    <div class="article-head">
      <div>
        <h2 class="article-headline">{{ article.headline }}</h2>
        <p style="font-size: .75rem">Af {{ article.author }} // {{ toLetterDate(article.created) }}</p>
      </div>
      <div v-if="article.isOwner" class="button-group">
        <img :onclick="() => handleUpdate(article.id)" class="article-controls-img" src="/edit.png"/>
        <img :onclick="() => handleDelete(article.id)" class="article-controls-img" src="/trashcan.png"/>
      </div>
    </div>
    <p class="article-content">{{ article.content }}</p>
    <img v-if="article.imageBase64.length > 0" :src="article.imageBase64" class="article-image center">
  </div>
</template>

<style scoped lang="css">
.article-cont {
  margin-bottom: 1.5rem;
  background-color: rgba(255,255,255,.1);
  border-radius: 9px;
  padding: 9px;
  animation: popUp .3s ease-in-out;;
}

.article-headline{
  white-space: break-spaces;
}

.article-image{
  width: 100%;
  overflow: auto;
  
}

.article-head {
  display: grid;
  grid-template-columns: 1fr min-content ;
}

.article-head > * {
  white-space: nowrap;
  height: min-content;
}

.article-controls-img{
  height: 20px;
  width: 20px;
  cursor: pointer;
}

.article-controls-img:hover {
  scale: 1.25;
  transition: scale 75ms ease-in-out;
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

@keyframes popUp {
  from {opacity: 0}
  to {opacity: 1}
}
</style>