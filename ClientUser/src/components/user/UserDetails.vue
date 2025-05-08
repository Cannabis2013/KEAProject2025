<script setup>
import {logOut, signedIn as isSignedIn} from '@/services/identity/auth';
import {ref} from 'vue';
import ButtonLink from "@/components/controls/ButtonLink.vue";
import {signedInMember} from "@/services/members/members.js";

const props = defineProps(["onSignOut", "onNavigationRequest"])

const name = ref("")
const signedIn = ref(isSignedIn())
const profileImage = ref("/userTemplate.png")

function getName(member) {
  const firstName = member.firstName
  const lastName = member.lastName
  return `${firstName} ${lastName}`
}

signedInMember()
    .then(member => {
      if (member == null) return
      name.value = getName(member)
      profileImage.value = member?.profileImage !== "" ? profileImage.value : "./userTemplate.png"
    })

function handleSignOut() {
  logOut()
  props.onSignOut()
  signedIn.value = false
}

function handleNavigationRequest() {
  if (props.onNavigationRequest)
    props.onNavigationRequest()
}
</script>
<template>
  <div v-if="signedIn" class="user-mini-cont">
    <img :src="profileImage" class="user-mini-img"/>
    <div class="user-data-cont">
      <p class="user-data-name">{{ name }}</p>
      <div class="user-data-links">
        <ButtonLink :onLinkClicked="handleNavigationRequest" text="Detaljer" to="/user"/>
        <ButtonLink :onLinkClicked="handleSignOut" text="Log ud" to="/"/>
      </div>
    </div>
  </div>
</template>
<style lang="css" scoped>
@media (orientation: portrait) and (max-width: 768px) {
  .user-mini-cont {
    display: grid;
    justify-items: center;
    margin-top: 1rem;
  }

  .user-mini-img {
    width: 64px;
    height: auto;
  }
}

@media (min-width: 768px) {
  .user-mini-cont {
    height: min-content;
    display: grid;
    grid-template-columns: 192px 1fr;
    align-items: center;
    column-gap: 24px;
    padding: 12px;
    animation: fadeIn .3s ease-in-out;
  }

  .user-mini-img {
    height: 192px;
    width: 192px;
  }

  .user-data-cont {
    display: grid;
    grid-template-rows: min-content min-content;
    align-content: space-between;
    height: 100%;
  }

  .user-data-name {
    font-size: 3rem;
    line-height: 3.7rem;
    font-weight: bold;
    height: min-content;
  }

  .user-data-rank {
    height: min-content;
    font-size: 1rem;
    line-height: 1rem;
    margin: 0;
  }

  .user-data-links {
    display: grid;
    row-gap: 6px;
    height: min-content;
    width: 96px;
  }

  @keyframes fadeIn {
    from {
      opacity: 0;
    }

    to {
      opacity: 1;
    }
  }
}
</style>