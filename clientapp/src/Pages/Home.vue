<template>
    <div id="home">
      
      This is Home Component
      <button @click="openSignInModal()">Login</button>
      <button @click="openSignUpModal()">Register</button>
       <button @click="openModal()">Open Modal</button>
          
      <sign-in v-if="showSignInModal" @closeSignInModal="closeSignInModal()"/>
      <sign-up v-if="showSignUpModal" @closeSignUpModal="closeSignUpModal()"/>
      <modal-root/>
    </div>
</template>

<script lang="ts">

import { defineComponent, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import SignIn from '../components/HomePage/Auth/SignIn.vue'
import SignUp from '../components/HomePage/Auth/SignUp.vue'
import ModalRoot from '../components/common/ModalRoot.vue'
import SignUpForm from '../components/auth/SignUpForm.vue'
import useEmitter from '../helpers/emitter'

export default defineComponent({
  name:'HomePage',
  components:{
    SignIn,
    SignUp,
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


    //Logic for opening/closening the sign up modal window
    const showSignUpModal = ref (false) //route.path =='/sign-up'? ref(true):ref(false)

    const openSignUpModal = () => {
      showSignUpModal.value = true
      router.push('sign-up')
    }

    const closeSignUpModal = () => {
      showSignUpModal.value = false
      router.go(-1)
    }


    


    const openModal = () => {
      emitter.emit('open', { component:SignUpForm, title: 'Loh' })
      router.push('sign-up')
    }

     onMounted  (() => {
        if (route.path =='/sign-up'){
          openModal()
        }
    })

    
  return{
    showSignInModal,
    showSignUpModal,
    openSignUpModal,
    closeSignUpModal,
    openSignInModal,
    closeSignInModal,
    openModal
  }
}
})

</script>