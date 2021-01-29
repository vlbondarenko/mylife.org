<template>
    <div id="home">
      
        This is Home Component
        <button @click="openLoginWindow">Login</button>

        <button @click="openRegisterWindow">Register</button>
      
    
    <login style="position:fixed" v-show="showLoginWindow" >
        <template v-slot:header>
          <div class="title">Login</div> <button class="close" @click="closeLoginWindow"></button>
        </template>
    </login>
    
      
      <register v-show="showRegisterWindow">
        <template v-slot:header>
          Title <button @click="closeRegisterWindow"></button>
        </template>
      </register>
    </div>
</template>

<script lang="ts">

import { defineComponent,ref } from 'vue'
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
    const showLoginWindow = ref(false)
    const showRegisterWindow = ref(false)

   if (store.state.loggedIn){
     router.push('/user')
   } 

   const openLoginWindow = () => {
    showLoginWindow.value= true  
   }

  const openRegisterWindow = () => {
    showRegisterWindow.value= true  
   }

   const closeLoginWindow = () => {
    showLoginWindow.value= false  
   }

  const closeRegisterWindow = () => {
    showRegisterWindow.value= false  
   }

  return{
    showLoginWindow,
    showRegisterWindow,
    openLoginWindow,
    openRegisterWindow,
    closeLoginWindow,
    closeRegisterWindow
  }
}
})

</script>