<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal" id="register-header">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        There were problems registering this user.
      </div>
        <table class="center-table">
          <tbody>
            <tr>
              <td>
                <label for="username" class="sr-only">Username</label>
              </td>
              <td>
                <input
                  type="text"
                  id="username"
                  class="form-control"
                  placeholder="Username"
                  v-model="user.username"
                  required
                  autofocus
                />
              </td>
            </tr>
            <tr>
              <td>
                <label for="password" class="sr-only">Password</label>
              </td>
              <td>
                <input
                  type="password"
                  id="password"
                  class="form-control"
                  placeholder="Password"
                  v-model="user.password"
                  required
                />
              </td>
            </tr>
            <tr>
              <td>
                <label for="confirmPassword" class="sr-only">Confirm Password</label>
              </td>
              <td>
                <input
                  type="password"
                  id="confirmPassword"
                  class="form-control"
                  placeholder="Confirm Password"
                  v-model="user.confirmPassword"
                  required
                />
                </td>
            </tr>
            <tr>
                <td> 
                  <label for="email" class="sr-only">Email</label>
                </td>
                <td>
                  <input
                  type="text"
                  id="email"
                  class="form-control"
                  placeholder="Email"
                  v-model="user.email"
                  required
                  autofocus
                />
                </td>
            </tr>
          </tbody>
        </table>
      <p>
        <button id="create-account-button" class="btn btn-lg btn-primary btn-block" type="submit">
          Create Account
      </button>
      </p>
      <p class="login-link">Have an account?
        <router-link :to="{ name: 'login' }">
          Log in
        </router-link>
      </p>
    </form>
  </div>
</template>

<script>
export default {
  name: 'register',
  data() {
    return {
      user: {
        email: '',
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
    };
  },
  methods: {
    register() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/account/register`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.user),
      })
        .then((response) => {
          if (response.ok) {
            this.$router.push({ path: '/login', query: { registration: 'success' } });
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
@import url('https://fonts.googleapis.com/css?family=Archivo+Narrow|Girassol|Pacifico|Solway&display=swap');

#register-header {
  font-family: 'Pacifico', cursive; 
  font-size: 3em;
  text-align: center;
}
.sr-only {
  font-family: 'Girassol', cursive; 
  font-size: 1.5em; 
  margin-right: 5px; 
}

tr td:first-child {
  text-align: left;
}

.center-table {
  margin: auto;
}

#create-account-button {
  font-family: 'Archivo Narrow', sans-serif;
  font-size: 1em;
  font-weight: bold;
}

.login-link {
  font-family: 'Girassol', cursive;
  font-size: 1.2em;
}
</style>

