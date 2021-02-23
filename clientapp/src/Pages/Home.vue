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

import { defineComponent, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
import ModalRoot from '../components/common/ModalRoot.vue'
import SignUpForm from '../components/auth/SignUpForm.vue'
import SignInForm from '../components/auth/SignInForm.vue'
import useEmitter from '../helpers/emitter'
import ForgotPasswordFormVue from '@/components/auth/ForgotPasswordForm.vue'

export default defineComponent({
  name:'HomePage',
  components:{
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
 
    const openSignInModal = () => {
      emitter.emit('onOpenModal', { component:SignInForm, title: 'Sign In' })
      router.push('/sign-in')
    }

    const openSignUpModal = () => {
      emitter.emit('onOpenModal', { component:SignUpForm, title: 'Sign Up' })
      router.push('/sign-up')
    }

     onMounted  (() => {
        if (route.path =='/sign-up'){
          openSignUpModal()
        }
        if (route.path =='/sign-in'){
          openSignInModal()
        }
        if (route.path =='/forgot-password'){
          emitter.emit('onOpenModal', { component:ForgotPasswordFormVue, title: 'Forgot Password' })
        }
    })

    
  return{
    openSignUpModal,
    openSignInModal,
  }
}
})

</script>