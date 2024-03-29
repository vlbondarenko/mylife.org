<template>
  <div>
    <div class="f-column jc-ai-center" v-show="!message">
      <form @submit.prevent="handleSubmit">
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
        <Button 
          :buttonType="'submit'" 
          :buttonText="t('homePage.menu.signIn')" 
          :loading="loading" />
      </form>
      <a @click="openForgotPasswordForm">{{ t('homePage.signInModal.forgotPasswordQuestion') }}</a>
    </div>

    <Message v-show="message" @closeMessage="handleCloseMessage">
      {{ message }}
    </Message>
  </div>
</template>

<script>
import router from "@/router";
import { defineComponent, ref } from "vue";
import { useStore } from "vuex";
import { required, helpers } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import useEmitter from "@/helpers/emitter";
import useLocalizer from "@/helpers/localizer";
import Message from "../common/Message.vue";
import ForgotPasswordForm from "./ForgotPasswordForm.vue";
import Input from "../common/Input.vue";
import Button from "../common/Button.vue";

export default defineComponent({
  name: "SignInForm",
  components: {
    Message,
    Input,
    Button,
  },
  setup() {
    const store = useStore();
    const emitter = useEmitter();

    const userEmail = ref("");
    const password = ref("");
    const loading = ref(false);

    const { t } = useLocalizer();

    const requiredWithCustomErrorMessage = helpers.withMessage(
      t('validation.required'),
      required
    );
    const v = useVuelidate(
      {
        userEmail: { required: requiredWithCustomErrorMessage },
        password: { required: requiredWithCustomErrorMessage },
      },
      { userEmail, password }
    );

    const message = ref("");

    const handleCloseMessage = () => {
      message.value = "";
    };

    function handleSubmit() {
      v.value.$touch();
      if (v.value.$error) return;

      loading.value = true;

      const loginData = {
        email: userEmail.value,
        password: password.value,
      };
      store.dispatch("user/SignIn", loginData).then(
        (data) => {
          if (data) router.push("/user");
          loading.value = false;
        },
        (errorMessage) => {
          message.value = errorMessage;
          loading.value = false;
        }
      );
    }

    const openForgotPasswordForm = () => {
      emitter.emit("onOpenModal", {
        component: ForgotPasswordForm,
        title: t('homePage.forgotPasswordModal.title'),
      });
    };

    return {
      userEmail,
      password,
      v,

      loading,
      message,
      handleCloseMessage,

      handleSubmit,
      openForgotPasswordForm,
      t
    };
  },
});
</script>

<style scoped>
a {
  border-bottom: 1px solid rgb(160, 160, 160);
  font-size: 11px;
  color: rgb(160, 160, 160);
}

a:hover {
  border-bottom: 1px solid rgb(0, 0, 0);
  cursor: pointer;
  color: rgb(0, 0, 0);
}
</style>

