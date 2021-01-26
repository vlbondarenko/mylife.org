<template>
    <div class="login-panel">
        <h1>Login</h1>
        Email
        <br />
        <input placeholder="player@example.com" v-model="email" />
        <br /><br />
        Password:
        <br />
        <input  placeholder="*********" type="password" v-model="password" />
        <br /><br />
        <div>
            <button class="btn btn-primary center" @click="login()">LOGIN</button>
        </div>
        <br><br>
    </div>  
</template>

<script lang="ts">
import { store } from '@/store'

export default {
    name:'Login',
    methods:{
        login  () {
            store.dispatch('setLoggedIn', true)
        }
    }
}
</script>

<style>

.logout-btn {
  position: absolute;
  top: 50%;
  right: 1.5%;
  font-size: 1rem;
}

.login-panel {
  position: absolute;
  top: 35%;
  right: 15%;
}

.center {
  position: absolute;
  top: 80%;
  left: 50%;
  transform: translate(-50%, -25%);
}

.h1.center{
    top:10%;
    left: 50%;
}

</style>