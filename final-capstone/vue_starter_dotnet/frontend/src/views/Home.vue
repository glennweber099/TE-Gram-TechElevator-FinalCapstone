<template>
  <div class="home">
    <!-- <div class="home-nav-bar"> -->
      <h1 id="home-header">TE Gram</h1>
      <!-- <div class="right-nav"> -->
        <p><router-link :to="{ name: 'upload' }" class="upload-photo-link">Upload a Photo</router-link></p>
        <p><button v-on:click="logout" id="logout-button">Click to Logout</button></p>
      <!-- </div> -->
    <!-- </div> -->
      <div class="container">
        <div class="images" v-for="photo in photos" v-bind:key="photo.id">
          <div class="item">
            <img v-bind:src="photo.imageUrl" id="photo-url">
            <p><span id="photo-owner">Total Likes:</span><span id="photo-caption">{{photo.totalLikes}}</span></p>
            <p><span id="photo-owner">{{photo.photoOwner}}  </span><span id="photo-caption">{{photo.caption}}</span></p>
        </div>
      </div>
    </div>

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

<style scoped>
@import url('https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Pacifico|Solway&display=swap');

.home-nav-bar {
  display: flex;

}

.home-nav-bar:first-child {
  justify-content: flex-start;
}

.home-nav-bar:last-child() {
  justify-content: flex-end;
}

#home-header {
  font-family: 'Pacifico', cursive; 
  font-size: 4em;
  text-align: center;
}

.upload-photo-link {
  font-family: 'Archivo Narrow', sans-serif;
  font-size: 1.2em;
}

#logout-button {
  font-family: 'Archivo Narrow', sans-serif;
  font-size: 1.2em;
}

#photo-owner {
  font-family: 'Solway', serif;
  font-size: 1.2em;
  font-weight: bolder;
  margin-bottom: 0;
}

.container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.item {
  padding: 50px;
  margin: 15px;
  background-color:rgba(255, 255, 255, 0.7);
  width: 750px;
  border-radius: 10px;
} 

.item > img {
  margin: 0;
  width: 100%;
}

.item > #photo-caption {
  font-family: 'Solway', serif;
}
</style> 
