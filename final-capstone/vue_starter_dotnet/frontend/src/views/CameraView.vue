<template>
    <div class="camera-modal">
        <video ref="video" class="camera-stream"/>
        <div class="camera-modal-container">
            <span v-on:click="capture">
              <button class="upload-photo-link">Take Photo</button>
            </span>
            <span>
             <router-link to="/" tag="button" id="go-back" class="upload-photo-link">Go Back</router-link>
            </span>
        </div>
    </div>
</template>

<script>
    export default {
    data () {
      return {
        mediaStream: null
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
    const mediaStreamTrack = this.mediaStream.getVideoTracks()[0]
    const imageCapture = new window.ImageCapture(mediaStreamTrack)
    return imageCapture.takePhoto().then(blob => {
        console.log(blob)
    })
    }
    }
    }
</script>

<style scoped>
    .camera-modal {
        width: 100%;
        height: 100%;
        top: 0;
        left: 0;
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
</style>