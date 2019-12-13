<template>
  <div class="home">
    <div class="home-nav-container">
      <div class="home-logo-box">
        <img id="tegram-logo" src="./../../logo.png"/>
      </div>
      <div class="center-box">
        <div id="home-header">TE Gram</div>
      </div>
      <div class="right-nav-box">
        <router-link :to="{ name: 'upload' }"><button class="upload-photo-link">Upload a Photo</button></router-link>
        <button v-on:click="logout" id="logout-button">Click to Logout</button>
        <router-link :to="{ name: 'camera'}"><button class="upload-photo-link">Take a Photo</button></router-link>
      </div>
    </div>
      <div class="container">
        <div class="images" v-for="photo in photos" v-bind:key="photo.id">
          <div class="item">
            <img v-bind:src="photo.imageUrl" id="photo-url">
            <p id="like-logo" v-on:click="toggleLike"></p>
            <p id="likes" v-if="photo.totalLikes > 1"><span>{{photo.totalLikes}}</span><span> likes</span></p>
            <p id="likes" v-if="photo.totalLikes == 1"><span>{{photo.totalLikes}}</span><span> like</span></p>
            <p><span id="photo-owner">{{photo.photoOwner}}  </span><span id="photo-caption">{{photo.caption}}</span></p>
            <!-- add comments to flexbox item -->
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
    toggleLike() {
       
    }
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
      }
    })
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

.home-nav-container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  margin-bottom: 20px;
  border-bottom: solid rgba(255, 255, 255, 0.7);
  border-top: solid rgba(255, 255, 255, 0.7);
}

.home-logo-box {
  display: flex;
  justify-content: flex-start;
  width: 33%;
}

.center-box {
  display: flex;
  justify-content: center;
  align-self: center;
  width: 33%;
}

.right-nav-box {
  display: flex;
  align-self: center;
  justify-content: flex-end;
  margin-right: 0;
  width: 33%;
}

#home-header {
  font-family: 'Pacifico', cursive; 
  font-size: 4em;
  align-content: center;
}
#tegram-logo {
  align-self: center;
  width: 150px;
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
  width: 600px;
  border-radius: 10px;
} 

.item > img {
  margin: 0;
  width: 100%;
}

.item > #photo-caption {
  font-family: 'Solway', serif;
}

#likes {
  font-family: 'Solway', serif;
  font-size: 1.2em;
  margin: 0;
}
</style> 

