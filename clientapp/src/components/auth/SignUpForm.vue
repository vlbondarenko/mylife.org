<template>
  <div>
    <div v-show="!message">
      <form @submit.prevent="handleSubmit">
        <Input
          v-model:modelValue="firstName"
          :inputType="'text'"
          :inputLabel="'First Name'"
          :validator="v.firstName"
        />
        <Input
          v-model:modelValue="lastName"
          :inputType="'text'"
          :inputLabel="'Last Name'"
          :validator="v.lastName"
        />
        <Input
          v-model:modelValue="userEmail"
          :inputType="'text'"
          :inputLabel="'Email'"
          :validator="v.userEmail"
        />
        <Input
          v-model:modelValue="password"
          :inputType="'password'"
          :inputLabel="'Password'"
          :validator="v.password"
        />
         <Input
          v-model:modelValue="confirmPassword"
          :inputType="'password'"
          :inputLabel="'Confirm Password'"
          :validator="v.confirmPassword"
        />
        <Button
          :buttonType="'submit'"
          :buttonText="'Sign Up'"
          :loading="loading"
        />
      </form>
    </div>
    <Message
      v-show="message"
      :showBackButton="showBackButton"
      @closeMessage="handleCloseMessage"
    >
      {{ message }}
    </Message>
  </div>
</template>

<script>
import { defineComponent, ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import {
  required,
  email,
  minLength,
  sameAs,
  helpers,
} from "@vuelidate/validators";
import Message from "../common/Message.vue";
import authService from '@/services/authService'
import Input from '../common/Input.vue'
import Button from '../common/Button.vue'

export default defineComponent({
  name: "SignUpForm",
  components: {
    Message,
    Input,
    Button
  },

  setup() {
    const firstName = ref("");
    const lastName = ref("");
    const userEmail = ref("");
    const password = ref("");
    const confirmPassword = ref("");

    const requiredWithCustomErrorMessage = helpers.withMessage(
      "This field is required",
      required
    );
    const emailWithCustomErrorMessage = helpers.withMessage(
      "The email must be valid",
      email
    );
    const minLenthWithCustomErrorMessage = helpers.withMessage(
      "The password must contain at least eight characters",
      minLength(8)
    );
    const sameAsWithCustomErrorMessage = helpers.withMessage(
      "Passwords don't match",
      sameAs(password)
    );

    const v = useVuelidate(
      {
        firstName: { required: requiredWithCustomErrorMessage },
        lastName: { required: requiredWithCustomErrorMessage },
        userEmail: {
          required: requiredWithCustomErrorMessage,
          email: emailWithCustomErrorMessage,
        },
        password: {
          required: requiredWithCustomErrorMessage,
          minLength: minLenthWithCustomErrorMessage,
        },
        confirmPassword: {
          required: requiredWithCustomErrorMessage,
          sameAs: sameAsWithCustomErrorMessage,
        },
      },
      { firstName, lastName, userEmail, password, confirmPassword }
    );

    const message = ref("");
    const loading = ref(false)
    const showBackButton = ref(false);

    const handleCloseMessage = () => {
      message.value = "";
    };

    const handleSubmit = async () => {
      const isFormCorrect = await v.value.$validate();
      if (!isFormCorrect) return;

      loading.value = true

      const userData = {
        userEmail: userEmail.value,
        firstName: firstName.value,
        lastName: lastName.value,
        password: password.value,
      };

      authService.signUp(userData).then(
        (msg) => {
          message.value = msg;
          showBackButton.value = false;
          loading.value = false
        },
        (errorMsg) => {
          message.value = errorMsg;
          showBackButton.value = true;
          loading.value = false
        }
      );
    };

    return {
      firstName,
      lastName,
      userEmail,
      password,
      confirmPassword,
      v,

      message,
      loading,
      showBackButton,
      handleCloseMessage,

      handleSubmit,
    };
  },
});
</script>
