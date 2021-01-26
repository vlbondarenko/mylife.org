<template>
    <div>
        <button @click="logout()">Logout</button>
    </div>   
</template>

<script lang="ts">
import { store } from '@/store'
import router from '@/router'

export default {
    methods:{
        logout(){
            store.dispatch('setLoggedIn', false)
            router.push('/')
        }
    }
}

</script>