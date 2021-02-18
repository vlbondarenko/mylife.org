<template>
    <div>
       This is User component
       <button class="btn" @click="logout">Logout</button>
       <div>
           {{user}}
       </div>
    </div>   
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'

export default defineComponent({
    name:'User',
    setup(){
        const store = useStore()
        const router = useRouter()
        const user = ref (localStorage.getItem('user'))

        if (!store.state.user.loggedIn){
            router.push('/')
        }

        const logout = () => {
            store.dispatch('user/Logout')
            router.push('/')
        }
        return{
            logout,
            user
        }
    }
})

</script>