<script setup>
import {ref} from "vue"
import {signIn} from "@/services/identity/auth"
import LoadIndicator from "@/components/loading/LoadIndicator.vue"
import TextInput from "@/components/controls/TextInput.vue"
import PushButton from "@/components/controls/PushButton.vue";

const props = defineProps(["onSuccess"])
const successHandler = props.onSuccess ?? function () {
}
const error = ref(false)
const loading = ref(false)
let email = "hjalte@gmail.com"
let password = "xrpuofni"

function updateEmail(e) {
  email = e.target.value
}

function updatePassword(e) {
  password = e.target.value
}

async function handleSubmit() {
  loading.value = true
  const user = await signIn(email, password)
  error.value = user == null
  loading.value = false
  successHandler(user != null)
}
</script>
<template>
  <div v-if="!loading" class="signin-cont">
    <TextInput :onChange="updateEmail" :placeholder="email" :value="email" class="user-input"/>
    <TextInput :onChange="updatePassword" :placeholder="password" :value="password" class="user-input" masked="true"/>
    <div style="height: 32px;"><span v-if="error" style="color: red">Noget gik galt</span></div>
    <PushButton :onPushed="handleSubmit" style="justify-self: end;" text="Videre"/>
  </div>
  <div v-else class="indicator-wrapper">
    <LoadIndicator/>
  </div>
</template>
<style lang="css" scoped>
.signin-cont {
  display: grid;
  grid-template-columns: 1fr 1fr;
  row-gap: .5rem;
  min-width: 200px;
  max-width: 320px;
  height: min-content;
  justify-self: center;
}

.user-input {
  grid-column: span 2;
}

.indicator-wrapper {
  width: 320px;
  height: 128px;
  justify-self: center;
}
</style>