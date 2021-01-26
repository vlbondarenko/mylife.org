<template>
  <router-view/>
</template>

<script lang="ts">
import { store } from './store';
import router from './router';

export default{
  
  name: "App",
  mounted(){
    if(store.state.loggedIn){
      router.push('/user')
    } else{
      router.push('/')
    }
  }
}

</script>

