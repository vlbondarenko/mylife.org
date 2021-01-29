<template>
    <div>
       This is User component
       <button class="btn" @click="logout">Logout</button>
    </div>   
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'

export default defineComponent({
    name:'User',
    setup(){
        const store = useStore()
        const router = useRouter()

        if (!store.state.loggedIn){
            router.push('/')
        }

        const logout = () => {
            store.dispatch('userModule/Register')
            router.push('/')
        }
        return{
            logout
        }
    }
})

</script>