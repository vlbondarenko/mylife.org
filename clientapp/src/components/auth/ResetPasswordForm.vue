<template>
  <div>
    <div v-show="!message">
      <form @submit.prevent="handleSubmit">
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
          :buttonText="'Reset Password'"
          :loading="loading"
          :optionalClass="'btn'"
        />
      </form>
    </div>
    <message
      v-show="message"
      :showBackButton="showBackButton"
      @closeMessage="handleCloseMessage"
    >
      {{ message }}
    </message>
  </div>
</template>

<script>
import { defineComponent, ref } from "vue";
import { required, minLength, sameAs, helpers } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import Message from "../common/Message.vue";
import authService from "@/services/authService";
import Input from '../common/Input.vue'
import Button from '../common/Button.vue'

export default defineComponent({
  name: "ResetPasswordForm",
  components: {
    Message,
    Input,
    Button
  },
  setup() {
    const password = ref("");
    const confirmPassword = ref("");

    const requiredWithCustomErrorMessage = helpers.withMessage(
      "This field is required",
      required
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
        password: {
          required: requiredWithCustomErrorMessage,
          minLength: minLenthWithCustomErrorMessage,
        },
        confirmPassword: {
          required: requiredWithCustomErrorMessage,
          sameAs: sameAsWithCustomErrorMessage,
        },
      },
      { password, confirmPassword }
    );

    const message = ref("");
    const loading = ref(false)
    const showBackButton = ref(false);
    const handleCloseMessage = () => {
      message.value = "";
    };

    function handleSubmit() {
      v.value.$touch();
      if (v.value.$error) return;

      loading.value = true

      authService.resetPassword(password.value).then(
        (msg) => {
          message.value = msg;
          showBackButton.value = false;
          loading.value = false
        },
        (errorMessage) => {
          message.value = errorMessage;
          showBackButton.value = true;
          loading.value = false
        }
      );
    }

    return {
      password,
      confirmPassword,
      v,

      message,
      loading,
      showBackButton,
      handleCloseMessage,

      handleSubmit
    };
  },
});
</script>
<style scoped>
.btn{
  min-width: 250px;
}
</style>

