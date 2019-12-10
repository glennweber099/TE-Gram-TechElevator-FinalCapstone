<template>
  <div class="home">
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
     <router-link :to="{ name: 'upload' }">Upload a Photo</router-link>
  </div>
</template>

<script>
export default {
  name: 'home'
}
</script>
