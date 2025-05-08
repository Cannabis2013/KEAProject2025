<script setup>
const props = defineProps(["article"])
</script>
<template>
  <div class="news-tile">
    <img class="tile-icon" src="/news.png"/>
    <div class="news-tile-upper">
      <p class="news-tile-head">{{ article.headline }}</p>
      <div class="news-tile-meta">
        <p class="news-tile-author">Af {{ article.author }}</p>
        <p class="news-tile-date">{{ article.date }}</p>
      </div>
    </div>
    <p class="news-tile-preview">{{ article.shortContent }}</p>
    <RouterLink class="news-tile-link" to="/news">Læs mere</RouterLink>
  </div>
</template>
<style lang="css" scoped>
.news-tile {
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

.tile-icon {
  grid-row: span 3;
  height: 80px;
}

.news-tile-upper {
  border-bottom: 1px solid black;
}

.news-tile > p,
.news-tile-upper > p,
.news-tile-meta > p {
  margin: 0;
  color: black !important;
}

.news-tile-meta {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.news-tile-head {
  font-size: 1.25rem;
  line-height: 1.25rem;
  font-weight: bold;
}

.news-tile-author {
  font-style: italic;
  font-size: .7rem;
}

.news-tile-date {
  font-style: italic;
  font-size: .7rem;
}

.news-tile-preview {
  margin-top: 16px !important;
  font-size: 0.8rem;
  white-space: pre-wrap;
}

.news-tile-link {
  font-size: .8rem;
  width: min-content;
  white-space: nowrap;
}
</style>