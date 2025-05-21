<script setup>
import {toDate} from "@/services/date/dateFormatting.js";

const props = defineProps(["post", "onDelete", "onUpdate"])

const handleDelete = props.onDelete ?? function () {
}
const handleUpdate = props.onUpdate ?? function () {
}

const post = props.post
</script>

<template>
  <div class="post-cont">
    <div class="post-body">
      <p class="post-author"> {{ post.author }} </p>
      <p class="post-message"> {{ post.message }} </p>
    </div>
    <div class="post-controls">
      <p class="post-date"> {{ toDate(post.created) }} </p>
      <img v-if="post.isOwner" src="/edit.png" class="post-icons" :onclick="() => handleUpdate(post.id)"/>
      <img v-if="post.isOwner" src="/trashcan.png" class="post-icons" :onclick="() => handleDelete(post.id)"/>
    </div>
    
  </div>
</template>

<style scoped lang="css">
.post-cont {
  display: grid;
  grid-template-columns: 1fr min-content;
  column-gap: 1rem;
  margin-bottom: 1rem;
  justify-content: space-between;
}

.post-body{
  padding-bottom: 9px;
  border-bottom: 2px solid lightskyblue;
}

.post-author {
  font-weight: bold;
  border-radius: 6px;
  width: min-content;
  white-space: nowrap;
  font-size: 1rem;
  padding: 6px;
  background-color: #2f2f2f;
}

.post-message {
  white-space: break-spaces;
  font-size: 1rem;
  line-height: 1.5rem;
  user-select: text;
}

.post-controls {
  display: grid;
  grid-template-rows: min-content min-content;
  grid-template-columns: min-content min-content;
  row-gap: 9px;
  width: 100%;
  height: 100%;
  justify-items: center;
}

.post-date {
  padding: 6px;
  white-space: nowrap;
  border-radius: 6px;
  background-color: #3f1f1f;
  grid-column: span 2;
}

.post-icons {
  width: 32px;
  height: 32px;
  cursor: pointer;
}
</style>