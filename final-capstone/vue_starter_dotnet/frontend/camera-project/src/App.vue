<template>
    <div id="app">
    <div>
      <video ref="video" id="video" width="640" height="480" />
    </div>
        <div><button id="snap" v-on:click="capture()">Snap Photo</button></div>
        <canvas ref="canvas" id="canvas" width="640" height="480"></canvas>
    </div>
</template>

<script>
export default {
  name: 'app',
    data() {
      return {
        mediaStream: null,
        video: {},
        canvas: {},
        captures: []
      }
    },
methods: {
    mounted () {
      navigator.mediaDevices.getUserMedia({ video: true })
        .then(mediaStream => {
          this.mediaStream = mediaStream
          this.$refs.video.srcObject = mediaStream
          this.$refs.video.play()
        })
        .catch(error => console.error('getUserMedia() error:', error))
    }
},
methods: {
    capture() {
        this.canvas = this.$refs.canvas;
        var context = this.canvas.getContext("2d").drawImage(this.video, 0, 0, 640, 480);
        this.captures.push(canvas.toDataURL("image/png"));
    }
  }
}
</script>

<style>
    #app {
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
    #video {
        background-color: #000000;
    }
    #canvas {
        display: none;
    }
    li {
        display: inline;
        padding: 5px;
    }
</style>
