<template>
    <div>
        <p>{{ message }}</p>
    </div>  
    <div v-show="showButton" >
        <button @click="onBack">Back</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import useEmitter from '@/helpers/emitter'
import SignUpForm from '../auth/SignUpForm.vue'

export default defineComponent({
    props:{
        message:String,
        showButton:Boolean
    },
    setup (){
        const emitter = useEmitter()

        const onBack = () => {
            emitter.emit('open', {component:SignUpForm, title:'signUp'})
        }

        return {
            onBack
        }
    }
})
</script>