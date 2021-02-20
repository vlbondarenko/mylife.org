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
        <span v-if="v.firstName.$invalid&&v.firstName.$dirty">{{v.firstName.$errors[0].$message}}</span>
        <input
          type="text"
          id="lastname"
          placeholder="Last Name"
          v-model="lastName"
          @blur="v.lastName.$touch()"
        />    
         <span v-if="v.lastName.$invalid&&v.lastName.$dirty">{{v.lastName.$errors[0].$message}}</span>
        <input
          type="text"
          id="userEmail"
          placeholder="Email"
          v-model="userEmail"
          @blur="v.userEmail.$touch()"    
        /> 
        <span v-if="v.userEmail.$invalid&&v.userEmail.$dirty">{{v.userEmail.$errors[0].$message}}</span>
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
        <button type="submit">
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
<script>
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useVuelidate } from "@vuelidate/core"
import { required, email, minLength, sameAs, helpers } from "@vuelidate/validators"

export default defineComponent({
  name: "Register",
  setup() {
    const store = useStore();
    const router = useRouter();
    const message = ref("");
    const successful = ref(false);

    const firstName = ref("")
    const lastName = ref ("")
    const userEmail = ref ("")
    const password = ref("")
    const confirmPassword = ref("")

    const customRequiredErrorMessage = helpers.withMessage('This field is required',required)
    const customEmailErrorMessage = helpers.withMessage('The email must be valid',email)
    
    const v = useVuelidate({
        firstName:{required: customRequiredErrorMessage},
        lastName:{required: customRequiredErrorMessage},
        userEmail: { required: customRequiredErrorMessage, email:customEmailErrorMessage},
        password: { required, minLength: minLength(8)},
        confirmPassword: { required:customRequiredErrorMessage, sameAs: sameAs(password)}
      },
      { firstName, lastName, userEmail, password, confirmPassword })

    const handleSubmit = async () => {
       const isFormCorrect = await v.value.$validate()
       if (!isFormCorrect) return

      const userData = {
        firstName:firstName.value,
        lastName:lastName.value,
        userEmail:userEmail.value,
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
      firstName, lastName, userEmail, password, confirmPassword,
      v
    };
  },
});
</script>
