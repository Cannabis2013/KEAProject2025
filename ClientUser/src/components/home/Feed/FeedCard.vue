<script setup>
const props = defineProps(["model"])
</script>
<template>
  <div class="feed-tile-cont">
    <img class="tile-icon" :src="model.image"/>
    <div class="feed-tile-upper">
      <p class="feed-tile-head">{{ model.title }}</p>
      <div class="feed-tile-meta">
        <p class="feed-tile-author">Af {{ model.author }}</p>
        <p class="feed-tile-date">{{ model.date }}</p>
      </div>
    </div>
    <p class="feed-tile-content">{{ model.content }}</p>
  </div>
</template>
<style lang="css" scoped>
.feed-tile-cont {
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

.feed-tile-upper {
  border-bottom: 1px solid black;
}

.feed-tile-cont > p,
.feed-tile-upper > p,
.feed-tile-meta > p {
  margin: 0;
  color: black !important;
}

.feed-tile-meta {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.feed-tile-head {
  font-size: 1.25rem;
  line-height: 1.25rem;
  font-weight: bold;
}

.feed-tile-author {
  font-style: italic;
  font-size: .7rem;
}

.feed-tile-date {
  font-style: italic;
  font-size: .7rem;
}

.feed-tile-content {
  margin-top: 16px !important;
  font-size: 0.8rem;
  white-space: pre-wrap;
}

.feed-tile-link {
  font-size: .8rem;
  width: min-content;
  white-space: nowrap;
}
</style>