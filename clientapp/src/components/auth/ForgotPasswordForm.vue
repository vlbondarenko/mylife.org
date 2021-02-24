<template>
  <div v-show="!message">
    <form @submit.prevent="handleSubmit">
      <input
        type="text"
        id="email"
        placeholder="Email"
        v-model="userEmail"
        @blur="v.userEmail.$touch()"
      />
      <span v-if="v.userEmail.$invalid && v.userEmail.$dirty">{{
        v.userEmail.$errors[0].$message
      }}</span>
      <button @click="handleSubmit">Restore Password</button>
    </form>
  </div>
  <Message
    :showBackButton="showBackButton"
    v-show="message"
    @closeMessage="handleCloseMessage"
  >
    {{ message }}
  </Message>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { required, email } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import authService from "@/services/authService";
import Message from "../common/Message.vue";

export default defineComponent({
  components: {
    Message,
  },
  setup() {
    const userEmail = ref("");

    const v = useVuelidate({ userEmail: { required, email } }, { userEmail });

    const message = ref("");
    const showBackButton = ref(false);

    const handleCloseMessage = () => {
      message.value = "";
    };
    const handleSubmit = () => {
      v.value.$touch();
      if (v.value.$invalid) return;

      authService.forgotPassword(userEmail.value).then(
        (msg) => {
          message.value = msg;
        },
        (errorMsg) => {
          message.value = errorMsg;
          showBackButton.value = true;
        }
      );
    };

    return {
      userEmail,

      message,
      showBackButton,
      handleCloseMessage,

      handleSubmit,
      v,
    };
  },
});
</script>