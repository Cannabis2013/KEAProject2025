<script setup>
import {ref} from 'vue'
import {signedIn} from "@/services/identity/auth"
import UserDetailsDash from '@/components/user/UserDetails.vue'
import SignInForm from "@/components/user/SignInForm.vue";

const props = defineProps(["onNavigation"])

const isSignedIn = ref(false)

signedIn().then(status => isSignedIn.value = status)

function updateStatus(status) {
  isSignedIn.value = status
}

function handleSignOut() {
  isSignedIn.value = false
}

</script>
<template>
  <SignInForm v-if="!isSignedIn" :onSuccess="updateStatus"/>
  <UserDetailsDash v-else :onNavigationRequest="props.onNavigation" :onSignOut="handleSignOut"/>
</template>
<style lang="css" scoped></style>