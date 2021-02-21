<template> 
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
        <span v-if="v.confirmPassword.$invalid&&v.confirmPassword.$dirty">{{v.confirmPassword.$errors[0].$message}}</span>
        <button type="submit">
          Sign up
        </button>
      </form>
</template>
<script>
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { useVuelidate } from "@vuelidate/core"
import { required, email, minLength, sameAs, helpers } from "@vuelidate/validators"
import useEmitter from '@/helpers/emitter'

export default defineComponent({
  name: "SignUpForm",
  setup() {
    const store = useStore();
    const router = useRouter();
    const emitter = useEmitter()

    const message = ref("");

    const firstName = ref("")
    const lastName = ref ("")
    const userEmail = ref ("")
    const password = ref("")
    const confirmPassword = ref("")

    const requiredWithCustomErrorMessage = helpers.withMessage('This field is required',required)
    const emailWithCustomErrorMessage = helpers.withMessage('The email must be valid',email)
    const minLenthWithCustomErrorMessage = helpers.withMessage('The password must contain at least eight characters',minLength(8))
    const sameAsWithCustomErrorMessage = helpers.withMessage('Passwords don\'t match',sameAs(password))
    
    
    const v = useVuelidate({
        firstName:{required: requiredWithCustomErrorMessage},
        lastName:{required: requiredWithCustomErrorMessage},
        userEmail: { required: requiredWithCustomErrorMessage, email:emailWithCustomErrorMessage},
        password: { required:requiredWithCustomErrorMessage, minLength: minLenthWithCustomErrorMessage},
        confirmPassword: { required:requiredWithCustomErrorMessage, sameAs: sameAsWithCustomErrorMessage}},
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
            }
        );
    };

    return {
      message,
      handleSubmit,
      firstName, lastName, userEmail, password, confirmPassword,
      v, emitter
    };
  },
});
</script>
