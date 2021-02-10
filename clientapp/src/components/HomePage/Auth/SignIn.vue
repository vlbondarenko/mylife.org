<template>
  <div class="modal-wrapper">
    <div class="modal-body">
      <div class="title">Login</div> <button class="close" @click="$emit('closeSignInModal')"><i class="fa fa-close"></i></button>
      <form @submit.prevent="handleSubmit">
        <input
          v-model="emailAdress"
          class="type-one"
          type="text"
          placeholder="Email"
          @blur="v.emailAdress.$touch()"
        />
        <span v-if="v.emailAdress.required.$invalid &&v.emailAdress.$dirty">The email address must be valid </span>
        <input
          v-model="password"
          class="type-one"
          type="password"
          placeholder="Password"
          @blur="v.password.$touch()"
        />
        <span v-if="v.password.required && v.password.$dirty">Name is required!</span>
        <button type="submit" disabled:>
          Sign in
        </button>
      </form>
    </div>
  </div>
</template>

<script lang = "ts">
import router from "@/router";
import { defineComponent, ref } from "vue";
import { useStore } from "vuex";
import { email, required } from '@vuelidate/validators';
import useVuelidate from '@vuelidate/core';

export default defineComponent({
  name: "Login",
  setup() {
    const store = useStore();
    
    const message = ref("");
    const emailAdress = ref("")
    const password = ref("")

    const v = useVuelidate(
      {
        emailAdress: { required, email },
        password: { required }
      },
      { emailAdress, password }
    )

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
            (error) => {
                message.value = error;
            }
        );
    }
    

    return {
        message,
        handleSubmit,
        emailAdress,
        password,
        v
    };
  },
});
</script>

