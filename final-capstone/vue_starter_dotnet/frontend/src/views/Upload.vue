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
          <router-link :to="{ name: 'camera'}">
            <button class="take-photo">Take Photo</button>
          </router-link>
          <router-link :to="{ name: 'favorites' }">
            <button class="view-favorites">View Favorites</button>
          </router-link>
          <button v-on:click="logout" id="logout-button">Logout</button>
        </div>
      </div>
    </div>
  <div id="new-post" class="container">
    <h2 id="upload-header">Upload a photo to share</h2>
    <form id="post-form" v-on:submit.prevent="sharePhoto">
      <vue-dropzone
        id="dropzone"
        v-bind:options="dropzoneOptions"
        v-on:vdropzone-sending="addFormData"
        v-on:vdropzone-success="getSuccess"
      ></vue-dropzone>
      <input
        type="text"
        name="caption"
        id="caption"
        v-model="post.caption"
        autocomplete="off"
        placeholder="Add a caption..."
      />
      <div class="form-actions">
        <button v-bind:disabled="!canPost" id="share">Share</button>
        <router-link to="/" tag="button" id="go-back">Go Back</router-link>
      </div>
    </form>
  </div>
</div>
</template>

<script>
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import auth from "../auth.js";

export default {
  name: "new-post",
  components: {
    vueDropzone: vue2Dropzone
  },
  data() {
    return {
      dropzoneOptions: {
        // tutorials
        // https://danhough.com/blog/dropzone-cloudinary/ 
        // https://alligator.io/vuejs/vue-dropzone/
        url: "https://api.cloudinary.com/v1_1/tech-elevator/image/upload",  //this is my url for cloudinary
        thumbnailWidth: 250,
        maxFilesize: 2.0,
        acceptedFiles: ".jpg, .jpeg, .png, .gif",
        uploadMultipe: false
      },
      post: {
        imageUrl: "",
        caption: ""
      }
    };
  },
  computed: {
    canPost() {
      return this.post.imageUrl;
    }
  },
  methods: {
    /**
     * Called before sending the request to add additional headers.
     * @function
     */
    addFormData(file, xhr, formData) {
      formData.append("api_key", "714725446462368");  // my api key
      formData.append("upload_preset", "vg8sew4g"); // my upload preset
      formData.append("timestamp", (Date.now() / 1000) | 0);
      formData.append("tags", "vue-app");
    },
    /**
     * Called after an upload success to get the image url.
     * @function
     */
    getSuccess(file, response) {
      this.post.imageUrl = response.secure_url;
    },
    /**
     * POSTs a new Post
     * @function
     */
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
}

.take-photo {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color:rgba(235,164,73,1); 
}

.view-favorites {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color:rgba(235,164,73,1); 
}

#logout-button {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  width: 125px;
  border-radius: 15px;
  background-color:rgba(235,164,73,1); 
}
#upload-header {
  font-family: 'Pacifico', cursive; 
  font-size: 3em;
  text-align: center;
}

#share {
  font-family: 'Archivo Narrow', sans-serif;
  font-size: 1.2em;
  margin-top: 10px;
  width: 100px;
  border-radius: 15px;
  background-color:rgba(235,164,73,1); 
}

#go-back {
  font-family: 'Archivo Narrow', sans-serif;
  font-size: 1.2em;
  margin-top: 10px;
  width: 100px;
  border-radius: 15px;
  background-color:rgba(235,164,73,1); 
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