<template>
<div>
    <div class="camera-modal">
      <video ref="video" class="camera-stream"/>
    </div>
    <form id="post-form" v-on:submit.prevent="postPhoto">
        <input
        type="text"
        name="caption"
        id="caption"
        v-model="post.caption"
        autocomplete="off"
        placeholder="Add a caption..."
        />
        <div>
            <router-link to="/" tag="button" id="go-back">Go Back</router-link>
            <span v-on:click="capture">
              <button class="upload-photo-link">Take Photo</button>
            </span>
            <span v-on:submit="postPhoto">
              <button class="upload-photo-link">Post Photo</button>
            </span>
         </div>
    </form>
  </div>
</template>

<script>
import auth from "../auth.js";

    export default {
    data () {
      return {
        mediaStream: null,
        post: {
        imageUrl: "",
        caption: ""
      }
      }
    },
    mounted () {
      navigator.mediaDevices.getUserMedia({ video: true })
        .then(mediaStream => {
          this.mediaStream = mediaStream
          this.$refs.video.srcObject = mediaStream
          this.$refs.video.play()
        })
        .catch(error => console.error('getUserMedia() error:', error))
    },
    methods: {
    capture () {
    let link = document.createElement('a');
    const mediaStreamTrack = this.mediaStream.getVideoTracks()[0]
    console.log('Made it here 2')
    const imageCapture = new window.ImageCapture(mediaStreamTrack)
    console.log('Made it here 3')
    return imageCapture.takePhoto().then(blob => {
      link.href = URL.createObjectURL(blob);
      console.log(link.href);
      this.post.imageUrl = link.href;
      })
    },

    postPhoto() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/camera`, {
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
    },
    destroyed () {
    const tracks = this.mediaStream.getTracks()
    tracks.map(track => track.stop())
    },
  }
}
</script>

<style scoped>
    .camera-modal {
        width: 75%;
        height: 75%;
        bottom: 10px;
        left:150px;
        position: absolute;
        z-index: 10;
    }
    .camera-stream {
        width: 100%;
        max-height: 100%;
    }
    .camera-modal-container {
        position: absolute;
        bottom: 0;
        width: 100%;
        align-items: center;
        margin-bottom: 24px;
    }
    .take-picture-button {
        display: flex;
    }
    .upload-photo-link {
      font-family: 'Archivo Narrow', sans-serif;
      font-size: 1.2em;
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