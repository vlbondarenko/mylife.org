<template>
    <div id="home">
      <div>
        This is Home Component
      </div>
      <login />
      <register/>
    </div>
</template>

<script lang="ts">

import { defineComponent } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Login from '../components/Auth/Login.vue'
import Register from '../components/Auth/Register.vue'

export default defineComponent({
  name:'HomePage',
  components:{
    Login,
    Register
  },
  setup(){
    const store=useStore()
    const router= useRouter()

   if (store.state.loggedIn){
     router.push('/user')
   } 
  }
})

</script>
