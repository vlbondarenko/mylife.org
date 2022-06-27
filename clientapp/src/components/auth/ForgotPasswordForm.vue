<template>
  <div v-show="!message">
    <form @submit.prevent="handleSubmit">
      <Input 
        v-model:modelValue="userEmail" 
        :inputType="'text'" 
        :inputLabel="t('homePage.modalCommon.email')"
        :validator="v.userEmail" />
      <Button 
        :buttonType="'submit'" 
        :buttonText="t('homePage.forgotPasswordModal.title')" 
        :loading="loading"
        :optionalClass="'forgot-password-btn'" />
    </form>
  </div>
  <Message 
    :showBackButton="showBackButton" 
    v-show="message" 
    @closeMessage="handleCloseMessage">
      {{ message }}
  </Message>
</template>

<script>
import { defineComponent, ref } from "vue";
import { required, email, helpers } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import authService from "@/services/authService";
import useLocalizer from "@/helpers/localizer";
import Message from "../common/Message.vue";
import Input from "../common/Input.vue";
import Button from "../common/Button.vue";

export default defineComponent({
  components: {
    Message,
    Input,
    Button
  },
  setup() {
    const userEmail = ref("");

    const { t } = useLocalizer()
    const requiredWithCustomErrorMessage = helpers.withMessage(
      t('validation.required'),
      required
    );
    const emailWithCustomErrorMessage = helpers.withMessage(
      t('validation.email'),
      email
    );
    const v = useVuelidate({ userEmail: { required: requiredWithCustomErrorMessage, email: emailWithCustomErrorMessage } }, { userEmail });

    const message = ref("");
    const loading = ref(false)
    const showBackButton = ref(false);

    const handleCloseMessage = () => {
      message.value = "";
    };
    const handleSubmit = () => {
      v.value.$touch();
      if (v.value.$invalid) return;

      loading.value = true

      authService.forgotPassword(userEmail.value).then(
        (msg) => {
          message.value = msg;
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
      userEmail,

      message,
      loading,
      showBackButton,
      handleCloseMessage,

      handleSubmit,
      v,
      t
    };
  },
});
</script>
<style>
.forgot-password-btn {
  width: 200px;
}
</style>