<template>
  <div>
    <div v-show="!message">
      <form @submit.prevent="handleSubmit">
         <Input
          v-model:modelValue="password"
          :inputType="'password'"
          :inputLabel="'New Password'"
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
          :optionalClass="'reset-password-btn'"
        />
      </form>
    </div>
    <message
    :showBackButton="false"
      v-show="message"
    >
      {{ message }}
       <Button
          :buttonText="'Home'"
          @onClick="goHome"
          :optionalClass="'go-home-btn'"
        />
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
import router from "@/router";

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

    function handleSubmit() {
      v.value.$touch();
      if (v.value.$error) return;

      loading.value = true

      authService.resetPassword(password.value).then(
        (msg) => {
          message.value = msg;
          loading.value = false
        },
        (errorMessage) => {
          message.value = errorMessage;
          loading.value = false
        }
      );
    }

    const goHome = () =>{
      router.push('/')
    }

    return {
      password,
      confirmPassword,
      v,

      message,
      loading,
      goHome,

      handleSubmit
    };
  },
});
</script>
<style>
.reset-password-btn{
  width: 200px;
}
.go-home-btn{
  position: absolute;
  left: -5px;
  margin-bottom: 0px;
  bottom: -20px;
}
</style>

