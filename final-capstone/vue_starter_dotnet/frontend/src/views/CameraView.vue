<!-- <template>
// <div class="home">
//     <div class="home-nav-container">
//       <div class="home-logo-box">
//         <img id="tegram-logo" src="./../../logo.png" />
//       </div>
//       <div class="center-box">
//         <div id="home-header">TE Gram</div>
//       </div>
//       <div class="right-nav-box">
//         <router-link :to="{ name: 'upload' }">
//           <button class="upload-photo-link">Upload a Photo</button>
//         </router-link>
//         <router-link :to="{ name: 'favorites' }">
//           <button class="upload-photo-link">View All Favorites</button>
//         </router-link>
//         <button v-on:click="logout" id="logout-button">Click to Logout</button>
//         <router-link :to="{ name: 'camera'}">
//           <button class="upload-photo-link">Take a Photo</button>
//         </router-link>
//       </div>
//     </div>
// <div>
//     <div class="camera-modal">
//       <video ref="video" class="camera-stream"/>
//     </div>
//     <form id="post-form" v-on:submit.prevent="postPhoto">
//         <input
//         type="text"
//         name="caption"
//         id="caption"
//         v-model="post.caption"
//         autocomplete="off"
//         placeholder="Add a caption..."
//         />
//         <div>
//             <router-link to="/" tag="button" id="go-back">Go Back</router-link>
//             <span v-on:click="capture">
//               <button class="upload-photo-link">Take Photo</button>
//             </span>
//             <span v-on:submit="postPhoto">
//               <button class="upload-photo-link">Post Photo</button>
//             </span>
//          </div>
//     </form>
//   </div>
// </div>
// </template>

 <script> 
// import vue2Dropzone from "vue2-dropzone";
// import "vue2-dropzone/dist/vue2Dropzone.min.css";
// import auth from "../auth.js";

//     export default {
//     data () {
//       return {
//         mediaStream: null,
//         post: {
//         imageUrl: "",
//         caption: ""
//       }
//       }
//     },
//     mounted () {
//       navigator.mediaDevices.getUserMedia({ video: true })
//         .then(mediaStream => {
//           this.mediaStream = mediaStream
//           this.$refs.video.srcObject = mediaStream
//           this.$refs.video.play()
//         })
//         .catch(error => console.error('getUserMedia() error:', error))
//     },
//     methods: {
//     capture () {
//     let link = document.createElement('a');
//     const mediaStreamTrack = this.mediaStream.getVideoTracks()[0]
//     console.log('Made it here 2')
//     const imageCapture = new window.ImageCapture(mediaStreamTrack)
//     console.log('Made it here 3')
//     return imageCapture.takePhoto().then(blob => {
//       link = URL.createObjectURL(blob);
//       console.log(link);
//       console.log(blob);
//       var reader = new FileReader();
//       reader.readAsDataURL(blob); 
//       reader.onloadend = function() {
//       var base64data = reader.result;                
//       console.log(base64data);
//       let test = cloudinary.v2.uploader.upload(base64data, 
//       function(error, result) {console.log(result, error); });
//       }
//     })
//   },

//     // postPhoto() {
//     //   fetch(`${process.env.VUE_APP_REMOTE_API}/camera`, {
//     //     method: "POST",
//     //     headers: {
//     //       Authorization: "Bearer " + auth.getToken(),
//     //       "Content-Type": "application/json"
//     //     },
//     //     body: JSON.stringify(this.post)
//     //   })
//     //     .then(response => {
//     //       if (response.ok) {
//     //         this.$router.push("/");
//     //       }
//     //     })
//     //     .catch(err => {
//     //       console.log(err);
//     //     });
//     // },
//     destroyed () {
//     const tracks = this.mediaStream.getTracks()
//     tracks.map(track => track.stop())
//     },
//   }
// }
// </script>

// <style scoped>
//     .camera-modal {
//         width: 75%;
//         height: 75%;
//         bottom: 10px;
//         left:150px;
//         position: absolute;
//         z-index: 10;
//     }
//     .camera-stream {
//         width: 100%;
//         max-height: 100%;
//     }
//     .camera-modal-container {
//         position: absolute;
//         bottom: 0;
//         width: 100%;
//         align-items: center;
//         margin-bottom: 24px;
//     }
//     .take-picture-button {
//         display: flex;
//     }
//     .upload-photo-link {
//       font-family: 'Archivo Narrow', sans-serif;
//       font-size: 1.2em;
//     }
//     #upload-header {
//       font-family: 'Pacifico', cursive; 
//       font-size: 3em;
//       text-align: center;
//     }

//     #share {
//       font-family: 'Archivo Narrow', sans-serif;
//       font-size: 1.2em;
//       margin-top: 10px;
//       width: 100px;
//     }

//     #go-back {
//       font-family: 'Archivo Narrow', sans-serif;
//       font-size: 1.2em;
//       margin-top: 10px;
//       width: 100px;
//     }

//     #post-form {
//       margin-bottom: 15px;
//       text-align: center;
//     } 

//     #dropzone {
//       width: 75%;
//       margin: auto;
//     }

//     #caption {
//       width: 50%;
//      height: 50px;
//       margin-top: 10px;
//       text-align: center;
//     }
// </style> -->
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
    return{
      post: {
        imageUrl: "",
        caption: ""
      }
    }
  },
  mounted () {
    let widgetScript = document.createElement('script');
    widgetScript.setAttribute('src', "https://widget.cloudinary.com/v2.0/global/all.js");
    widgetScript.setAttribute('type', "text/javascript");
    document.head.appendChild(widgetScript);
  },
  methods: {
    setupWidget(){
    var myWidget = cloudinary.createUploadWidget({
    cloudName: 'tech-elevator', 
    apiKey: '714725446462368',
    uploadPreset: 'vg8sew4g'}, (error, result) => { 
      if (!error && result && result.event === "success") {
        console.log('Done! Here is the image info: ', result.info.secure_url);
        this.post.imageUrl = result.info.secure_url
    }
  }
)

document.getElementById("upload_widget").addEventListener("click", function(){
    myWidget.open();
  }, false);
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
}
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