<template>
  <div>
    <div v-show="!message">
      <form @submit.prevent="handleSubmit">
        <Input 
          v-model:modelValue="firstName" 
          :inputType="'text'" 
          :inputLabel="t('homePage.signUpModal.firstName')"
          :validator="v.firstName" />
        <Input 
          v-model:modelValue="lastName" 
          :inputType="'text'" 
          :inputLabel="t('homePage.signUpModal.lastName')"
          :validator="v.lastName" />
        <Input 
          v-model:modelValue="userEmail" 
          :inputType="'text'" 
          :inputLabel="t('homePage.modalCommon.email')"
          :validator="v.userEmail" />
        <Input 
          v-model:modelValue="password" 
          :inputType="'password'" 
          :inputLabel="t('homePage.modalCommon.password')"
          :validator="v.password" />
        <Input 
          v-model:modelValue="confirmPassword" 
          :inputType="'password'"
          :inputLabel="t('homePage.modalCommon.confirmPassword')" 
          :validator="v.confirmPassword" />
        <Button 
          :buttonType="'submit'" 
          :buttonText="t('homePage.menu.signUp')" 
          :loading="loading" />
      </form>
    </div>
    <Message v-show="message" :showBackButton="showBackButton" @closeMessage="handleCloseMessage">
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
import useLocalizer from "@/helpers/localizer";

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

    const { t } = useLocalizer()

    const requiredWithCustomErrorMessage = helpers.withMessage(
      t('validation.required'),
      required
    );
    const emailWithCustomErrorMessage = helpers.withMessage(
      t('validation.email'),
      email
    );
    const minLenthWithCustomErrorMessage = helpers.withMessage(
      t('validation.password', { count: 8 }),
      minLength(8)
    );
    const sameAsWithCustomErrorMessage = helpers.withMessage(
      t('validation.confirmedPassword'),
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
      t
    };
  },
});
</script>
