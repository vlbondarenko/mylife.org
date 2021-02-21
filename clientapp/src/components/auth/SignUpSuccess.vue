<template>
    <div>
        Congratulations!
    </div>
     <div>
      <button @click="onBack">Back</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import useEmitter from '@/helpers/emitter'
import SignUpForm from '../components/auth/SignUpForm.vue'

export default defineComponent({

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