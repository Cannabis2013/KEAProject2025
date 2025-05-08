<script setup>
import SignInForm from '@/components/user/SignInForm.vue'
import {useRouter} from "vue-router";

const router = useRouter()

function onSuccess(status) {
  if (status)
    router.replace('/')
}

</script>
<template>
  <div class="signIn-cont">
    <div>
      <SignInForm :onSuccess="onSuccess"/>
    </div>
  </div>
</template>
<style lang="css" scoped>
@media (orientation: portrait) {
  .signIn-cont {
    width: 100%;
    height: 100vh;
    display: grid;
    align-items: center;
  }
}
</style>