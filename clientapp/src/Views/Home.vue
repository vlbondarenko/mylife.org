<template>
    <div id="home">
      <div>
        This is Home Component
      </div>
      <login />
      <button @click="goTo">Go to user</button>
    </div>
</template>

<script lang="ts">
import router from '@/router'
import { defineComponent } from 'vue'
import Login from '../components/Auth/Login.vue'

export default defineComponent({
  components:{
    Login,
  },

  setup(){
    const goTo = () =>{
      router.push('/user')
    }

    return {
      goTo
    }
  }
})

</script>
