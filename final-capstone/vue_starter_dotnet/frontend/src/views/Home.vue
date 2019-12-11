<template>
  <div class="home">
    <h1>Home</h1>
    <div class="images" v-for="photo in photos" v-bind:key="photo.id">
      <p>{{photo.photoOwner}}</p>
     <img v-bind:src="photo.imageUrl">
      <p>{{photo.caption}}</p>
    </div>
    <p><router-link :to="{ name: 'upload' }">Upload a Photo</router-link></p>
    <p><button v-on:click="logout">Click to Logout</button></p>
    <!-- DONE (just wanted to keep this comment here) This link (^) goes back to the log in screen
    it does not log out the user but when they type in new credidentals it replaces the token 
    replacing the token makes it associated with the user's credidentals that just typed them in
    Not entirely sure if this works but it seems like it does. 
    It will be easier to test once we can get the uploading images to work since we can see which user submitted the images.
    Since it does not fully log out the user in theory
    if the user were to click the home button, they would still be able to see the page you can only see if you were logged in as that user
    There is an "auth.destroyToken(token)" which takes a token (opposite of what was used in Login.vue Line 81)
    but you need the token in order to do that and I am not sure how to access that token from here  -->
  </div>
</template>

<script>
import auth from '../auth';
export default {
  name: 'home',
  methods: {
    logout : function(token) {
      auth.destroyToken(token);
      this.$router.push('/login')
    },
  },
  data() {
    return {
      photos: [],
    }
  },
  created(){
    fetch(`${process.env.VUE_APP_REMOTE_API}/photo`, {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: "Bearer " + auth.getToken()
        }})
        .then(response => {
          return response.json();
        })
        .then(json => {
          console.log(json);
          this.photos = json;
        });
  }
}
</script>