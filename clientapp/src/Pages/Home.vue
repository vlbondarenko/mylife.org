<template>
    <div id="home">
      
      This is Home Component
      <button @click="openLoginModal">Login</button>
      <button @click="openRegisterModal">Register</button>
          
      <login/>
      <register/>
      
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

    if (store.state.userModule.loggedIn){
      router.push('/user')
    } 

    //Logic for opening and closing a modal login window
    //showLoginModal - this is the variable that determines the state of the modal window: open or closed
    //The value of this variable and the function to change its value are passed to the child component by provide() functions
    const showLoginModal = ref(false)

    const openLoginModal = () => {
      showLoginModal.value= true  
    }

    const closeLoginModal = () => {
      showLoginModal.value= false  
    }

    provide('showLoginModal',showLoginModal)
    provide('closeLoginModal',closeLoginModal)
    

    //Logic for opening and closing a modal register window
    const showRegisterModal = ref(false)

    const openRegisterModal = () => {
      showRegisterModal.value= true  
    }

    const closeRegisterModal = () => {
      showRegisterModal.value= false  
    }

    provide('showRegisterModal',showRegisterModal)
    provide('closeRegisterModal',closeRegisterModal)


  return{
    showLoginModal,
    showRegisterModal,
    openLoginModal,
    openRegisterModal,
    closeLoginModal,
    closeRegisterModal
  }
}
})

</script>