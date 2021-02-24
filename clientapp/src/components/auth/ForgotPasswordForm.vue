<template>
    <form @submit.prevent="handleSubmit">
        <input
        type="text"
        id="email"
        placeholder="Email"
        v-model="userEmail"
        @blur="v.userEmail.$touch()"
         />
         <span v-if="v.userEmail.$invalid&&v.userEmail.$dirty" >{{v.userEmail.$errors[0].$message}}</span>

        <button @click="hundleSubmit">Restore Password</button>
    </form>      
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import useEmitter from '@/helpers/emitter'
import {required, email } from '@vuelidate/validators';
import useVuelidate from '@vuelidate/core'
import authService from '@/services/authService'
import ShowMessage from './ShowMessage.vue'
import currentComponent from '@/components/auth/ForgotPasswordForm.vue'

export default defineComponent({
    setup (){
        const emitter = useEmitter()
        const userEmail = ref("")

        const v = useVuelidate({
            userEmail:{required, email}
        },
        {userEmail})

        const showMessage = (title:string, props:any) =>{
            emitter.emit('onOpenModal', { component: ShowMessage, title: title, props: props})
        }

        const hundleSubmit = () => {
            v.value.$touch()
            if(v.value.$invalid) return

            authService.forgotPassword(userEmail.value)
                .then(message => {
                    showMessage('Success', {sourceComponentOfModal:null, sourceTitleOfModal:'', message: message})
                }, errorMessage => {
                    showMessage('Failure', {sourceComponentOfModal:currentComponent, sourceTitleOfModal:'Forgot Password', message: errorMessage})
                })
        }

        return {
            hundleSubmit,
            v, userEmail
        }
    }
})
</script>