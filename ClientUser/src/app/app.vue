<script setup>
import Dashboard from '@/components/dashboard/Dashboard.vue';
import MobileNavBar from '@/components/dashboard/MobileNavBar.vue';
</script>
<template>
  <!-- 
    For mobile and small tablets 
    
    MobileNavbar is a bootstrap-like navigationbar with hamburger menu and a foldable
    view containing navigation links
  -->
  <MobileNavBar/>
  <!--
    For desktop and tablets 

    Dashboard provides links and a login form and supports basic user functionality.
  -->
  <Dashboard/>
  <RouterView/>
</template>
