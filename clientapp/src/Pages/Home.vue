<template>
    <div id="home">
      
      This is Home Component
      <button @click="openSignInModal()">Login</button>
      <button @click="openSignUpModal()">Register</button>
          
      <sign-in v-if="showSignInModal" @closeSignInModal="closeSignInModal()"/>
      <sign-up v-if="showSignUpModal" @closeSignUpModal="closeSignUpModal()"/>
      
    </div>
</template>

<script lang="ts">

import { defineComponent, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import SignIn from '../components/HomePage/Auth/SignIn.vue'
import SignUp from '../components/HomePage/Auth/SignUp.vue'

export default defineComponent({
  name:'HomePage',
  components:{
    SignIn,
    SignUp
  },
  setup(){
    const store = useStore()
    const router = useRouter()
    const route = useRoute()

    if (store.state.user.loggedIn){
      router.push('/user') 
    } 

    //Logic for opening/closening the sign in modal window
    //If the modal window was open before the page was reloaded, then it remains open after the reload
    const showSignInModal = route.path =='/sign-in'? ref(true):ref(false)
   
    const openSignInModal = () => {
      showSignInModal.value = true
      router.push('sign-in')
    }

    const closeSignInModal = () => {
      showSignInModal.value = false
      router.go(-1)
    }


    //Logic for opening/closening the sign up modal window
    const showSignUpModal = route.path =='/sign-up'? ref(true):ref(false)

    const openSignUpModal = () => {
      showSignUpModal.value = true
      router.push('sign-up')
    }

    const closeSignUpModal = () => {
      showSignUpModal.value = false
      router.go(-1)
    }

    
  return{
    showSignInModal,
    showSignUpModal,
    openSignUpModal,
    closeSignUpModal,
    openSignInModal,
    closeSignInModal
  }
}
})

</script>