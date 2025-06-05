<script setup>
const props = defineProps(["model"])
</script>
<template>
  <div class="article-card-cont">
    <img class="topic-card-icon" src="/news.png"/>
    <div class="article-card-upper">
      <p class="article-card-head">{{ model.headline }}</p>
      <div class="article-card-meta">
        <p class="article-card-author">Af {{ model.author }}</p>
        <p class="article-card-date">{{ model.date }}</p>
      </div>
    </div>
    <p class="article-card-content">{{ model.shortContent }}</p>
  </div>
</template>
<style lang="css" scoped>
.article-card-cont {
  display: grid;
  grid-template-columns: 80px 1fr;
  grid-template-rows: min-content 1fr min-content;
  background-color: white;
  padding: 12px;
  width: 100%;
  height: min-content;
  border-radius: 6px;
}

.topic-card-icon {
  grid-row: span 3;
  height: 64px;
}

.article-card-upper {
  border-bottom: 1px solid black;
}

.article-card-cont > p,
.article-card-upper > p,
.article-card-meta > p {
  margin: 0;
  color: black !important;
}

.article-card-meta {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.article-card-head {
  font-size: 1.25rem;
  line-height: 1.25rem;
  font-weight: bold;
}

.article-card-author {
  font-style: italic;
  font-size: .7rem;
}

.article-card-date {
  font-style: italic;
  font-size: .7rem;
}

.article-card-content {
  margin-top: .25rem !important;
  font-size: 0.8rem;
  white-space: pre-wrap;
}

.article-card-link {
  font-size: .8rem;
  width: min-content;
  white-space: nowrap;
}
</style>