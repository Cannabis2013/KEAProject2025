<script setup>
const props = defineProps(["model"])
</script>
<template>
  <div class="article-tile-cont">
    <img class="topic-tile-icon" src="/news.png"/>
    <div class="article-tile-upper">
      <p class="article-tile-head">{{ model.title }}</p>
      <div class="article-tile-meta">
        <p class="article-tile-author">Af {{ model.author }}</p>
        <p class="article-tile-date">{{ model.date }}</p>
      </div>
    </div>
    <p class="article-tile-content">{{ model.content }}</p>
  </div>
</template>
<style lang="css" scoped>
.article-tile-cont {
  display: grid;
  grid-template-columns: 80px 1fr;
  grid-template-rows: min-content 1fr min-content;
  column-gap: 12px;
  background-color: white;
  padding: 12px;
  width: 100%;
  height: min-content;
  border-radius: 6px;
}

.topic-tile-icon {
  grid-row: span 3;
  height: 64px;
}

.article-tile-upper {
  border-bottom: 1px solid black;
}

.article-tile-cont > p,
.article-tile-upper > p,
.article-tile-meta > p {
  margin: 0;
  color: black !important;
}

.article-tile-meta {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.article-tile-head {
  font-size: 1.25rem;
  line-height: 1.25rem;
  font-weight: bold;
}

.article-tile-author {
  font-style: italic;
  font-size: .7rem;
}

.article-tile-date {
  font-style: italic;
  font-size: .7rem;
}

.article-tile-content {
  margin-top: 16px !important;
  font-size: 0.8rem;
  white-space: pre-wrap;
}

.article-tile-link {
  font-size: .8rem;
  width: min-content;
  white-space: nowrap;
}
</style>