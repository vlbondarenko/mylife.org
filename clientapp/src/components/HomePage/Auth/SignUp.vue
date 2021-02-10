<template>
<div class="modal-wrapper">
<div class="modal-body">
      Title <button class="close" @click="$emit('closeSignUpModal')"><i class="fa fa-close"/></button>
      <form @submit.prevent="handleSubmit">     
        <input
          type="text"
          id="firstname"
          placeholder="First Name"
          v-model="firstName"
          @blur="v.firstName.$touch()"
        />  
        <span v-if="v.firstName.$invalid&&v.firstName.$dirty">This field is required</span>
        <input
          type="text"
          id="lastname"
          placeholder="Last Name"
          v-model="lastName"
          @blur="v.lastName.$touch()"
        />    
         <span v-if="v.lastName.$invalid&&v.lastName.$dirty">This field is required</span>
        <input
          type="email"
          id="emailAdress"
          placeholder="Email"
          v-model="emailAdress"
          @blur="v.emailAdress.$touch()"
        /> 
        <span v-if="v.emailAdress.$invalid&&v.emailAdress.$dirty">The email address must be valid</span>
        <input
          type="password"
          id="password"
          placeholder="Password"
          v-model="password"
          @blur="v.password.$touch()"
        />
         <span v-if="v.password.$invalid&&v.password.$dirty">The password must contain at least eight characters</span>
         <input
          type="password"
          id="confirmPassword"
          placeholder="Confirm Password"
          v-model="confirmPassword"
           @blur="v.confirmPassword.$touch()"
        />
        <span v-if="v.confirmPassword.$invalid&&v.confirmPassword.$dirty">Passwords don't match</span>
        <button class="btn btn-primary btn-block w-75 my-4" type="submit">
          Sign up
        </button>
      </form>
      <div
        v-if="message"
        class="alert"
        :class="successful ? 'alert-success' : 'alert-danger'"
      >
        {{ message }}
      </div>
    </div>
</div>
</template>
<script lang="ts">
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import {useVuelidate} from "@vuelidate/core"
import {required,email,minLength, sameAs} from "@vuelidate/validators"

export default defineComponent({
  name: "Register",
  setup() {
    const store = useStore();
    const router = useRouter();
    const message = ref("");
    const successful = ref(false);

    const firstName = ref("")
    const lastName = ref ("")
    const emailAdress = ref ("")
    const password = ref("")
    const confirmPassword = ref("")
    
    const v = useVuelidate({
        firstName:{required},
        lastName:{required},
        emailAdress: { required, email },
        password: { required, minLength:minLength(8) },
        confirmPassword: { required, sameAs:sameAs(password) }
      },
      { firstName,lastName,emailAdress, password,confirmPassword })

    const handleSubmit = () => {
      v.value.$touch()
      if (v.value.$error) return

      const userData = {
        firstName:firstName.value,
        lastName:lastName.value,
        emailAdress:emailAdress.value,
        password:password.value
      }
      store.dispatch("user/Register", userData).then(
        (data) => {
          if (data) {
            router.push("user");
          }
        },
        (error) => {
          message.value = error.message;
          successful.value = false;
        }
      );
    };

    return {
      message,
      successful,
      handleSubmit,
      firstName, lastName, emailAdress, password, confirmPassword,
      v
    };
  },
});
</script>
