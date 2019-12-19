<template>
  <div class="home">
    <div class="home-nav-container">
      <div class="home-logo-box">
        <img id="tegram-logo" src="./../../logo.png" />
      </div>
      <div class="center-box">
        <div id="home-header">TE Gram</div>
      </div>
      <div class="right-nav-box">
        <div class="right-nav-column">
          <!-- <router-link :to="{ name: 'upload' }">
            <button class="upload-photo">Upload Photo</button>
          </router-link> -->
          <router-link :to="{ name: 'camera'}">
            <button class="upload-photo">Upload Photo</button>
          </router-link>
          <router-link :to="{ name: 'favorites' }">
            <button class="view-favorites">View Favorites</button>
          </router-link>
          <button v-on:click="logout" id="logout-button">Logout</button>
        </div>
      </div>
    </div>
    <div class="container">
      <div class="images" v-for="photo in photos" v-bind:key="photo.id">
        <div class="item">
          <router-link id="photo-url" v-bind:to="{name:'detail', params: { photoId: photo.id}}"><img v-bind:src="photo.imageUrl" id="photo-url" style="margin: 0; width: 100%;"/></router-link>
         <div class="button-container">
            <div class="button-item">
              <div class="button-item-like">
                <div class="like" v-if="photo.IsLikedByUser == true">
                  <div class="liked-logo" v-on:click="toggleLike(photo.id)">💗</div>
                </div>
                <div v-else class="button-item-like">
                  <div class="not-liked-logo" v-on:click="toggleLike(photo.id)">♡</div>
                </div>
              </div>
              <div class="button-item-favorite">
                <div class="favorite" v-if="photo.isFavoritedByUser == true">
                  <div class="favorited-logo" v-on:click="toggleFavorite(photo.id)">⭐</div>
                </div>
                <div class="button-item-favorite" v-else>
                  <div class="not-favorited-logo" v-on:click="toggleFavorite(photo.id)">☆</div>
                </div>
              </div>
          </div>
        </div>
          <p id="likes" v-if="photo.totalLikes > 1">
            <span>{{photo.totalLikes}} likes</span>
          </p>
          <p id="likes" v-if="photo.totalLikes == 1">
            <span>{{photo.totalLikes}} like</span>
          </p>
          <p>
            <span id="photo-owner">{{photo.photoOwner}}</span>
            <span id="photo-caption"> {{photo.caption}}</span>
          </p>
          <!-- put comments in container here -->
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
    but you need the token in order to do that and I am not sure how to access that token from here-->
  </div>
</template>

<script>
import auth from "../auth";
// import { ok } from "assert";
export default {
  name: "home",
  methods: {
    logout: function(token) {
      auth.destroyToken(token);
      this.$router.push("/login");
    },
    toggleLike(photoId) {
      let like = {
        photoId: photoId,
      };
      fetch(`${process.env.VUE_APP_REMOTE_API}/like/togglelike`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + auth.getToken()
        },
        body: JSON.stringify(like)
      })
        .then(response => {
          if (response.ok) {
            return response.json();
          }
        })
        .then(text => {
          this.photos.forEach(photo => {
            if (photo.id === photoId) {
              photo.totalLikes = text.totalLikes;
              photo.IsLikedByUser = text.liked;
            }
          });
        })
        .then(err => console.error(err));
    },
      toggleFavorite(photoId) {
      let favorite = {
        photoId: photoId,
      };
      fetch(`${process.env.VUE_APP_REMOTE_API}/favorite/togglefavorite`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + auth.getToken()
        },
        body: JSON.stringify(favorite)
      })
        .then(response => {
          if (response.ok) {
            return response.json();
          }
        })
        .then(text => {
          this.photos.forEach(photo => {
            if (photo.id === photoId) {
              photo.isFavoritedByUser = text.favorited;
            }
          });
        })
        .then(err => console.error(err));
      },
      detail: function(photoId) {   
        this.$router.push("/{photoId}")
      }
  },
  data() {
    return {
      photos: []
    };
  },
  created() {
    fetch(`${process.env.VUE_APP_REMOTE_API}/photo`, {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
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
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Londrina+Outline|Pacifico|Solway&display=swap');
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
  flex-direction: row;
  justify-content: flex-end;
  align-self: center;
  margin-right: 0;
  width: 33%;
}

.right-nav-column {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
}

#home-header {  
  font-family: "Pacifico", cursive;
  font-size: 4em;
  align-content: center;
}

#tegram-logo {
  align-self: center;
  width: 150px;
}

.upload-photo {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
  margin-bottom: 3px;
}

.upload-photo:hover {
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(255,230,67,1) 0%, rgba(255,80,80,1) 100.2% );
  box-shadow: white 2px 2px;
}

.take-photo {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
  margin-bottom: 3px;
}

.take-photo:hover {
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(255,230,67,1) 0%, rgba(255,80,80,1) 100.2% );
  box-shadow: white 2px 2px;
}

.view-favorites {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
  margin-bottom: 3px;
}

.view-favorites:hover {
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(255,230,67,1) 0%, rgba(255,80,80,1) 100.2% );
  box-shadow: white 2px 2px;
}

#logout-button {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
}

#logout-button:hover {
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(255,230,67,1) 0%, rgba(255,80,80,1) 100.2% );
  box-shadow: white 2px 2px;
}

#photo-owner {
  font-family: "Solway", serif;
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
  background-color: rgba(255, 255, 255, 0.7);
  width: 600px;
  border-radius: 10px;
}

.item >  img {
  margin: 0;
  width: 100%;
}

.item > #photo-caption {
  font-family: "Solway", serif;
}

#likes {
  font-family: "Solway", serif;
  font-size: 1.2em;
  margin: 0;
}

.not-liked-logo {
  font-size: 2em;
  margin-bottom: 5px;
}

.liked-logo {
  font-size: 1.4em;
  margin-bottom: 5px;
}

.favorited-logo {
  font-size: 1.3em;
  margin-bottom: 5px;
  margin-top: 7px;
}

.not-favorited-logo {
  font-size: 2em;
  margin-bottom: 5px;
}

.button-container {
  display: flex;
  flex-direction: column;
}

.button-item {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas: "like favorite";
}   

.button-item-like {
  grid-area: like;
  justify-self: left;
}

.button-item-favorite {
  grid-area: favorite;
  justify-self: right;
}
</style> 

