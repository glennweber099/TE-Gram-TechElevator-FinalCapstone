<template>
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
        uploadMultipe: true
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
@import url('https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Pacifico|Solway&display=swap');

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
}

#go-back {
  font-family: 'Archivo Narrow', sans-serif;
  font-size: 1.2em;
  margin-top: 10px;
  width: 100px;
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