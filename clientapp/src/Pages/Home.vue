<template>
    <div id="home">
      
        This is Home Component
        <button @click="openLoginWindow">Login</button>

        <button @click="openRegisterWindow">Register</button>
      
    
    <login/>
    
      
      <register v-show="showRegisterWindow">
        <template v-slot:header>
          Title <button @click="closeRegisterWindow"></button>
        </template>
      </register>
    </div>
</template>

<script lang="ts">

import { defineComponent,provide,ref } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
import Login from '../components/HomePage/Auth/Login.vue'
import Register from '../components/HomePage/Auth/Register.vue'

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

   if (store.state.userModule.loggedIn){
     router.push('/user')
   } 

   const openLoginWindow = () => {
    showLoginWindow.value= true  
    console.log(showLoginWindow.value)
   }

  const openRegisterWindow = () => {
    showRegisterWindow.value= true  
   }

   const closeLoginWindow = () => {
    showLoginWindow.value= false  
    console.log(showLoginWindow.value)
   }

  const closeRegisterWindow = () => {
    showRegisterWindow.value= false  
   }

  provide('showLogin',showLoginWindow)
  provide('closeLogin',closeLoginWindow)

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