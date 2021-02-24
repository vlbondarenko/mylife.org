<template>
  <div>
    <div v-show="!message">
      <form @submit.prevent="handleSubmit">
        <input
          v-model="password"
          class="type-one"
          type="password"
          placeholder="Password"
          @blur="v.password.$touch()"
        />
        <span v-if="v.password.$invalid && v.password.$dirty">{{
          v.password.$errors[0].$message
        }}</span>
        <input
          v-model="confirmPassword"
          class="type-one"
          type="password"
          placeholder="Confirm Password"
          @blur="v.confirmPassword.$touch()"
        />
        <span v-if="v.confirmPassword.$invalid && v.confirmPassword.$dirty">{{
          v.confirmPassword.$errors[0].$message
        }}</span>
        <button type="submit">Reset Password</button>
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

export default defineComponent({
  name: "ResetPasswordForm",
  components: {
    Message,
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
    const showBackButton = ref(true);

    function handleSubmit() {
      v.value.$touch();
      if (v.value.$error) return;

      authService.resetPassword(password.value).then(
        (msg) => {
          message.value = msg;
          showBackButton.value = false;
        },
        (errorMessage) => {
          message.value = errorMessage;
          showBackButton.value = true;
        }
      );
    }

    return {
      password,
      confirmPassword,
      v,

      message,
      showBackButton,

      handleSubmit,
    };
  },
});
</script>

