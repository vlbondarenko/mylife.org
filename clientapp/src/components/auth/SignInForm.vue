<template>
      <form @submit.prevent="handleSubmit">
        <input
          v-model="emailAdress"
          class="type-one"
          type="text"
          placeholder="Email"
          @blur="v.emailAdress.$touch()"
        />
        <span v-if="v.emailAdress.$invalid &&v.emailAdress.$dirty">{{v.emailAdress.$errors[0].$message}}</span>
        <input
          v-model="password"
          class="type-one"
          type="password"
          placeholder="Password"
          @blur="v.password.$touch()"
        />
        <span v-if="v.password.$invalid && v.password.$dirty">{{v.password.$errors[0].$message}}</span>
        <button type="submit" >
          Sign in
        </button>
      </form>
      <a @click="openForgotPasswordForm">Forgot Password?</a>
</template>

<script lang = "ts">
import router from "@/router";
import { defineComponent, ref } from "vue";
import { useStore } from "vuex";
import {required } from '@vuelidate/validators';
import useVuelidate from '@vuelidate/core';
import useEmitter from "@/helpers/emitter";
import ShowMessage from "./ShowMessage.vue";
import currentComponent from '../auth/SignInForm.vue';
import ForgotPasswordForm from "./ForgotPasswordForm.vue";

export default defineComponent({
  name: "Login",
  setup() {
    const store = useStore()
    const emitter = useEmitter()

    const emailAdress = ref("")
    const password = ref("")

    const v = useVuelidate(
      {
        emailAdress: { required},
        password: { required }
      },
      { emailAdress, password }
    )

    const openForgotPasswordForm = () =>{
      emitter.emit('onOpenModal', {component: ForgotPasswordForm, title:'Forgot Password'})
      router.push('/forgot-password')
    }

    function handleSubmit () {
      v.value.$touch()
      if(v.value.$error)  return
      const loginData = {
        email: emailAdress.value,
        password:password.value
      }
        store.dispatch("user/Login", loginData).then(
            (data) => {
                if (data) router.push("/user");
            },
            (errorMessage) => {
                emitter.emit('onOpenModal', {component:ShowMessage, title: 'Something went wrong!', props: {message:errorMessage, sourceComponentOfModal:currentComponent, sourceTitleOfModal:'Sign In'} })
            }
        );
    }
    

    return {
        handleSubmit,
        emailAdress,
        password,
        v, openForgotPasswordForm
    };
  },
});
</script>

<style scoped>
a:hover{
    border-bottom: 1px solid rgb(158, 158, 158);
    cursor: pointer;
}
</style>

