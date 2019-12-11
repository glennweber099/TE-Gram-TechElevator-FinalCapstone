<template>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Hello and Welcome to TE-Gram</h1>
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
       <strong> Invalid username and password! </strong>
      </div>
      <div class="alert alert-success" role="alert" v-if="this.$route.query.registration">
        <strong> Thank you for registering, please sign in. </strong>
      </div>
          <p>
            <label for="username" class="sr-only"><strong>Username</strong></label>
          <input
            type="text"
            id="username"
            class="form-control"
            placeholder="Username"
            v-model="user.username"
            required
            autofocus
          />
          </p>
          <p>
          <label for="password" class="sr-only"><strong>Password</strong></label>
          <input
            type="password"
            id="password"
            class="form-control"
            placeholder="Password"
            v-model="user.password"
            required
          />
          </p>
          <p>
          <router-link :to="{ name: 'register' }"><strong>Need to Sign Up?</strong></router-link>
          </p>
          <p>
          <button type="submit"><strong>Sign in</strong></button>
          </p>
    </form>
  </div>
</template>

<script>
import auth from '../auth';

export default {
  name: 'login',
  components: {},
  data() {
    return {
      user: {
        username: '',
        password: '',
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/account/login`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.user),
      })
        .then((response) => {
          if (response.ok) {
            return response.text();
          } else {
            this.invalidCredentials = true;
          }
        })
        .then((token) => {
          if (token != undefined) {
            if (token.includes('"')) {
              token = token.replace(/"/g, '');
            }
            auth.saveToken(token);
            this.$router.push('/');
          }
        })
        .catch((err) => console.error(err));
    },
  },
};
</script>

<style>

</style>
