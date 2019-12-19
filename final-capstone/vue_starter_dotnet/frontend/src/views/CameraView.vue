<template>
  <div class="take-photo-page">
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
          <router-link :to="{ name: 'favorites' }">
            <button class="view-favorites">View Favorites</button>
          </router-link>  
          <button v-on:click="logout" id="logout-button">Logout</button>
        </div>
      </div>
    </div>
  <router-link to="/" tag="button" id="go-back">Go Back</router-link> 
  <div class="take-photo-header">Upload a photo to share</div>
  <div>
    <div class="widget-container">
      <button v-on:click="setupWidget" id="upload_widget" class="cloudinary-button">Upload Photo</button>
    </div>
    <form id="post-form" v-on:submit.prevent="sharePhoto">
      <input
        type="text"
        name="caption"
        id="caption"
        v-model="post.caption"
        autocomplete="off"
        placeholder="Add a caption..."
      />
      <div class="form-actions">
        <button id="share">Share</button>
      </div>
    </form>
  </div>
  </div>
</template>

<script>
import auth from "../auth.js";
export default {
  data() {
    return {
      post: {
        imageUrl: "",
        caption: ""
      }
    };
  },
  mounted() {
    let widgetScript = document.createElement("script");
    widgetScript.setAttribute(
      "src",
      "https://widget.cloudinary.com/v2.0/global/all.js"
    );
    widgetScript.setAttribute("type", "text/javascript");
    document.head.appendChild(widgetScript);
  },
  methods: {
    setupWidget() {
      var myWidget = cloudinary.createUploadWidget(
        {
          cloudName: "tech-elevator",
          apiKey: "714725446462368",
          uploadPreset: "vg8sew4g"
        },
        (error, result) => {
          if (!error && result && result.event === "success") {
            console.log(
              "Done! Here is the image info: ",
              result.info.secure_url
            );
            this.post.imageUrl = result.info.secure_url;
          }
        }
      );
          myWidget.open();
    },
    sharePhoto() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/photo/upload`, {
        method: "POST",
        headers: {
          Authorization: "Bearer " + auth.getToken(),
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.post)
      })
        .then(response => {
          if (response.ok) {
            this.$router.push("/");
          }
        })
        .catch(err => {
          console.log(err);
        });
    }
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
  background-color:rgba(235,164,73,1); 
  margin-bottom: 5px;
  box-shadow: gray 2px 2px;

}

.upload-photo:hover {
  background-color: rgb(251, 255, 2);
  box-shadow: black 2px 2px;
}

.view-favorites {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px; 
  margin-bottom: 5px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
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

.take-photo-header {
  font-family: 'Girassol', cursive;
  font-size: 3em;
  text-align: center;
  margin-bottom: 25px;
}

.cloudinary-button {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  font-weight: bold;
  width: 200px;
  border-radius: 15px;
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(2,255,197,1) 18%, rgba(0,131,231,1) 100.2% );
  color: black;
  box-shadow: gray 2px 2px;
  margin-bottom: 15px;
}

.widget-container {
  display: flex;
  flex-direction: row;
  justify-content: center;
}

#share {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  margin-top: 10px;
  width: 100px;
  border-radius: 15px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
  margin-top: 20px;
}

#share:hover {
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(255,230,67,1) 0%, rgba(255,80,80,1) 100.2% );
  box-shadow: white 2px 2px;
  /* transform:skew(-10deg); */
}

#go-back {
font-family: "Archivo Narrow", sans-serif;  
  font-size: 1.2em;
  margin-top: 10px;
  width: 100px;
  border-radius: 15px;
  background-color: rgba(255,80,80,.7);
  box-shadow: gray 2px 2px;
  margin-left: 25px;
  margin-bottom: 5px;
}

#go-back:hover {
  background-image: radial-gradient( circle farthest-corner at 10% 20%, rgba(255,230,67,1) 0%, rgba(255,80,80,1) 100.2% );
  box-shadow: white 2px 2px;
}

#post-form {
  margin-bottom: 15px;
  text-align: center;
}

#dropzone {
  width: 75%;
  margin: auto;
}

#caption {
  width: 50%;
  height: 50px;
  margin-top: 10px;
  text-align: center;
}
</style>