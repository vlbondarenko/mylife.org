<template>
    <div id="home">
      <div>
        This is Home Component
      </div>
      <login />
    </div>
</template>

<script lang="ts">

import { defineComponent } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Login from '../components/Auth/Login.vue'

export default defineComponent({
  name:'HomePage',
  components:{
    Login,
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
