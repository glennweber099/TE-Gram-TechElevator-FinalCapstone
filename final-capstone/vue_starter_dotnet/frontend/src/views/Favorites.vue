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
        <div class="right-nav-top-row">
        <router-link :to="{ name: 'upload' }">
          <button class="upload-photo">Upload Photo</button>
        </router-link>
        <router-link :to="{ name: 'camera'}">
          <button class="take-photo">Take Photo</button>
        </router-link>
        </div>
        <div class="right-nav-bottom-row">
        <button v-on:click="logout" id="logout-button">Logout</button>
        <router-link to="/" tag="button" id="go-back">Go Back</router-link>
        </div>
      </div>
    </div>
    <div class="container">
      <div class="images" v-for="photo in photos" v-bind:key="photo.id">
        <div class="item">
          <img v-bind:src="photo.imageUrl" id="photo-url"/>
        </div>
      </div>
    </div>
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
      }
  },
  data() {
    return {
      photos: []
    };
  },
  created() {
    fetch(`${process.env.VUE_APP_REMOTE_API}/favorite/getallfavoritesbyuser`, {
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
@import url("https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Pacifico|Solway&display=swap");

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
  flex-direction: column;
  align-self: center;
  justify-content: flex-end;
  margin-right: 0;
  width: 33%;
}
/* 
.right-nav-top-row {
  justify-content: flex-end;
}

.right-nav-bottom-row {
  justify-content: flex-end;
} */

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
  width: 25%;
}

.take-photo {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 25%;
}

#logout-button {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 25%;
}

#go-back {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 25%;
}

#photo-owner {
  font-family: "Solway", serif;
  font-size: 1.2em;
  font-weight: bolder;
  margin-bottom: 0;
}

.container {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  align-items: center;
  padding: 50px;
  margin: 15px;
  background-color: rgba(255, 255, 255, 0.7);
  border-radius: 10px;
  justify-content: center;
}

.item > img {
  width: 350px;
  height: 300px;
  justify-content: center;

}
</style> 

