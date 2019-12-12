<template>
<div id="body" class="fullscreen">
  <div id="logosignin" class="col-md-4" >
    <!-- <div id="nav" class="row-fluid text-center">
          <li id="eventlink"><router-link to="/">Home</router-link></li>  
          This link (^) will show just images without the ability to like or favorite them when a user is not logged in
          <li id="eventlink"><router-link to="/">Log-In</router-link></li>
          <li id="eventlink"><router-link to="/register">Register</router-link></li>
    </div> -->
    <router-view/>
  </div>
</div>
</template>
<style scoped>
.fullscreen {
  position: fixed;
  top: 0px;
  left: 0px;
  bottom:0px;
  right: 0px;
  overflow: auto;
  background:radial-gradient( circle farthest-corner at 13.9% 28.2%, rgba(235,164,73,1) 0%, rgba(252,236,138,1) 90% );

}
 #logosignin img {
    width: 250px;
    height: auto;
    margin: 0 auto;
}
#logosignin {
    height: 350px;
    margin: 20px;
    align-content: center;
}
#logosignin a {
    text-decoration: none;
    color: black;
    padding: 7px;
    border-radius: 10px;
    background-color:white;
    box-sizing: border-box;
    display: block;
 }
#eventnav {
    height: 75px;
}
#logosignin li {
    bottom: 30px;
    list-style: none;
    display: inline-block;
    margin: auto;
    width: 30%;
   }
#eventshow {
    margin-top: 50px;
}
#eventlink {
    margin: 0 auto;
    text-align: center;
    left: 20px;
}
.text-center{
  text-align:center;
}
</style>
