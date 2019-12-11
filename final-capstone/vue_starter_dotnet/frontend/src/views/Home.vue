<template>
  <div class="home">
    <h1>Home</h1>
    <p>Images with the option to like/favorite them will appear here. If you are not logged in, this will just show images without the option to like/favorite them</p>
    <p><router-link :to="{ name: 'upload' }">Upload a Photo</router-link></p>
    <p><router-link :to="{ name: 'login' }">Log Out</router-link></p>
    <!-- This link (^) goes back to the log in screen
    it does not log out the user but when they type in new credidentals it replaces the token 
    replacing the token makes it associated with the user's credidentals that just typed them in
    Not entirely sure if this works but it seems like it does. 
    It will be easier to test once we can get the uploading images to work since we can see which user submitted the images.
    Since it does not fully log out the user in theory
    if the user were to click the home button, they would still be able to see the page you can only see if you were logged
    There is an "auth.destroyToken(token)" which takes a token (opposite of what was used in Login.vue Line 81)
    but you need the token in order to do that and I am not sure how to do that.  -->
  </div>
</template>

<script>

export default {
  name: 'home',
}
</script>