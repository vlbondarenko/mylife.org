<template>
    <div id="home">
      
      This is Home Component
      <button @click="openSignInModal()">Login</button>
      <button @click="openSignUpModal()">Register</button>
          
      <sign-in v-if="showSignInModal" @closeSignInModal="closeSignInModal()"/>
      <modal-root/>
    </div>
</template>

<script lang="ts">

import { defineComponent, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import SignIn from '../components/HomePage/Auth/SignIn.vue'
import ModalRoot from '../components/common/ModalRoot.vue'
import SignUpForm from '../components/auth/SignUpForm.vue'
import useEmitter from '../helpers/emitter'

export default defineComponent({
  name:'HomePage',
  components:{
    SignIn,
    ModalRoot,
  },
  setup(){
    const store = useStore()
    const router = useRouter()
    const route = useRoute()
    const emitter = useEmitter()

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




    


    const openSignUpModal = () => {
      emitter.emit('onOpenModal', { component:SignUpForm, title: 'Sign Up' })
      router.push('sign-up')
    }

     onMounted  (() => {
        if (route.path =='/sign-up'){
          openSignUpModal()
        }
    })

    
  return{
    showSignInModal,
    openSignUpModal,
    openSignInModal,
    closeSignInModal,
  }
}
})

</script>