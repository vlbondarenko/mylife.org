<template>
    <div>
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
            <button  @click="login()">LOGIN</button>
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