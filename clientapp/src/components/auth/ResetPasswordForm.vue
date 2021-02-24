<template>
      <form @submit.prevent="handleSubmit">
        <input
          v-model="password"
          class="type-one"
          type="password"
          placeholder="Password"
          @blur="v.password.$touch()"
        />
        <span v-if="v.password.$invalid &&v.password.$dirty">{{v.password.$errors[0].$message}}</span>
        <input
          v-model="confirmPassword"
          class="type-one"
          type="password"
          placeholder="Confirm Password"
          @blur="v.confirmPassword.$touch()"
        />
        <span v-if="v.confirmPassword.$invalid && v.confirmPassword.$dirty">{{v.confirmPassword.$errors[0].$message}}</span>
        <button type="submit" >
          Reset Password
        </button>
      </form>
</template>

<script>
import { defineComponent, ref } from "vue";
import { required, minLength, sameAs, helpers } from "@vuelidate/validators"
import useVuelidate from '@vuelidate/core';
import useEmitter from "@/helpers/emitter";
import ShowMessage from "./ShowMessage.vue";
import currentComponent from "./ResetPasswordForm.vue";
import authService from '@/services/authService'

export default defineComponent({
  name: "ResetPasswordForm",
  setup() {
    const emitter = useEmitter()

    const password = ref("")
    const confirmPassword = ref("")

    const requiredWithCustomErrorMessage = helpers.withMessage('This field is required',required)
    const minLenthWithCustomErrorMessage = helpers.withMessage('The password must contain at least eight characters',minLength(8))
    const sameAsWithCustomErrorMessage = helpers.withMessage('Passwords don\'t match',sameAs(password))

    const v = useVuelidate(
      {
        password: { required: requiredWithCustomErrorMessage, minLength:minLenthWithCustomErrorMessage},
        confirmPassword: { required:requiredWithCustomErrorMessage, sameAs:sameAsWithCustomErrorMessage}
      },
      {  password, confirmPassword })

     const showMessage = (title, props) =>{
        emitter.emit('onOpenModal', { component: ShowMessage, title: title, closeButton:false, props: props})
    }

    function handleSubmit () {
        v.value.$touch()
        if(v.value.$error)  return
    
        authService.resetPassword(password.value)
            .then(message => {
                showMessage('Success', {sourceComponentOfModal:null, sourceTitleOfModal:'', message: message})
            }, errorMessage => {
                showMessage('Failure', {sourceComponentOfModal:currentComponent, sourceTitleOfModal:'Reset Password', message: errorMessage})
            })
        }
    
    return {
        handleSubmit,
        password,
        confirmPassword,
        v
    };
  },
});
</script>

