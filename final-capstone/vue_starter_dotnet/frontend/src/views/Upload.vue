<template>
  <div id="upload" class="text-center">
    <form class="image-upload">
      <h1 class="h3 mb-3 font-weight-normal">Upload Image</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        There were problems uploading this image. Please try again.
      </div>
      <p>
      <label for="imageUrl" class="sr-only">Image Url</label>
      <input
        type="text"
        id="imageUrl"
        class="form-control"
        placeholder=" Image Url"
        v-model="image.imageUrl"
        required
        autofocus
      />
      </p>
      <p>
      <label for="caption" class="sr-only">Caption</label>
      <input
        type="text"
        id="caption"
        class="form-control"
        placeholder=" "
        v-model="image.caption"
        required
      />
      </p>
      <p>
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Post Image
      </button>
      </p>
    </form>
  </div>
</template>

<script>
export default {
  name: 'upload',
  data() {
    return {
      image: {
        imageUrl: '',
        caption: '',
      },
      registrationErrors: false,
    };
  },
  methods: {
    upload() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/photo/upload`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.image),
      })
        .then((response) => {
          if (response.ok) {
            this.$router.push({ path: '/upload'});
          } else {
            this.registrationErrors = true;
          }
        })
        .then((err) => console.error(err));
    },
  },
};
</script>

<style>
</style>
