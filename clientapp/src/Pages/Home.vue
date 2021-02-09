<template>
    <div id="home">
      
      This is Home Component
      <button @click="showSignInModal=true">Login</button>
      <button @click="showSignUpModal=true">Register</button>
          
      <login v-if="showSignInModal" @closeSignInModal="showSignInModal = false"/>
      <register v-if="showSignUpModal" @closeSignUpModal="showSignUpModal = false"/>
      
    </div>
</template>

<script lang="ts">

import { defineComponent, ref } from 'vue'
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
    const showSignInModal = ref(false)
    const showSignUpModal = ref(false)

    if (store.state.userModule.loggedIn){
      router.push('/user')
    } 

  return{
    showSignInModal,
    showSignUpModal
  }
}
})

</script>