<script setup>
import {toDate} from "@/services/date/dateFormatting.js";

const props = defineProps(["post","onDelete","onUpdate"])

const handleDelete = props.onDelete ?? function(){}
const handleUpdate = props.onUpdate ?? function(){}

const post = props.post
</script>

<template>
  <div class="post-cont">
    <p class="post-author"> {{ post.author }} </p>
    <p class="post-date"> {{ toDate(post.created) }} </p>
    <p class="post-message"> {{ post.message }} </p>
    <div v-if="post.isOwner" class="post-controls">
      <img src="/edit.png" class="post-delete" :onclick="() => handleUpdate(post.id)"/>
      <img src="/trashcan.png" class="post-delete" :onclick="() => handleDelete(post.id)"/>
    </div>
  </div>
</template>

<style scoped lang="css">
.post-cont {
  display: grid;
  grid-template-columns: min-content min-content;
  grid-template-rows: 1fr min-content;
  row-gap: 9px;
  margin-bottom: 1rem;
  border-bottom: 2px solid white;
  justify-content: space-between;
  padding-bottom: 9px;
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

.post-date {
  padding: 6px;
  white-space: nowrap;
  border-radius: 6px;
  background-color: #3f1f1f;
}

.post-message {
  white-space: break-spaces;
  font-size: 1rem;
  line-height: 1.5rem;
  grid-column: span 2;
  user-select: text;
}

.post-controls{
  display: flex;
  column-gap: 9px;
  grid-column: 2 / 2;
  justify-self: end;
  width: min-content;
  height: 20px;
  margin: 0;
  padding:0;
}

.post-delete{
  width: 20px;
  height: 20px;
  cursor: pointer;
}
</style>