<template>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal" id="login-header">Welcome to TE-Gram</h1>
      <div
        id="invalid-credentials"
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        id="success-registering"
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering! Please sign in.</div>
      <p>
        <label
          for="username"
          class="sr-only"
          style="font-family: 'Girassol', cursive; font-size: 1.5em; margin-right: 5px;"
        >
          <strong>Username:</strong>
        </label>
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
        <label for="password" class="sr-only">
          <strong>Password:</strong>
        </label>
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
        <button type="submit" class="login-button">
          <strong>Log in</strong>
        </button>
      </p>
      <p class="create-new-account-link">
        Don't have an account?
        <router-link :to="{ name: 'register' }">
          <strong>Sign up</strong>
        </router-link>
      </p>
    </form>
  </div>
</template>

<script>
import auth from "../auth";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/account/login`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.user)
      })
        .then(response => {
          if (response.ok) {
            return response.text();
          } else {
            this.invalidCredentials = true;
          }
        })
        .then(token => {
          if (token != undefined) {
            if (token.includes('"')) {
              token = token.replace(/"/g, "");
            }
            auth.saveToken(token);
            this.$router.push("/");
          }
        })
        .catch(err => console.error(err));
    }
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Pacifico|Solway&display=swap");

#login-header {
  font-family: "Pacifico", cursive;
  font-size: 3em;
  text-align: center;
}

.create-new-account-link {
  font-family: "Girassol", cursive;
  font-size: 1.2em;
}

.sr-only {
  font-family: "Girassol", cursive;
  font-size: 1.5em;
  margin-right: 5px;
}

.login-button {
  font-family: "Archivo Narrow", sans-serif;
  font-size: 1em;
  font-weight: bold;
}

#success-registering {
  font-weight: bolder;
  font-size: 1.3em;
  color: orangered;
  font-family: "Solway", serif;
}

#invalid-credentials {
  font-weight: bolder;
  font-size: 1.3em;
  color: red;
  font-family: "Solway", serif;
}
</style>
