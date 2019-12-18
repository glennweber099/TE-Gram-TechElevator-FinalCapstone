<template>
  <div>
    <button v-on:click="setupWidget" id="upload_widget" class="cloudinary-button">Upload files</button>
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
        <router-link to="/" tag="button" id="go-back">Go Back</router-link>
      </div>
    </form>
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

      document.getElementById("upload_widget").addEventListener(
        "click",
        function() {
          myWidget.open();
        },
        false
      );
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
@import url("https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Pacifico|Solway&display=swap");

#upload-header {
  font-family: "Pacifico", cursive;
  font-size: 3em;
  text-align: center;
}

#share {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1.2em;
  margin-top: 10px;
  width: 100px;
}

#go-back {
  font-family: "Archivo Narrow", sans-serif;
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