<template>
  <div class="f-column jc-ai-center menu">
    This is Home Component
    <button class="floating-button" @click="openSignInModal()">Sign In</button>
    <button class="floating-button" @click="openSignUpModal()">Sign Up</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SignUpForm from "../auth/SignUpForm.vue";
import SignInForm from "../auth/SignInForm.vue";
import useEmitter from "@/helpers/emitter";

export default defineComponent({
  setup() {
    const emitter = useEmitter();

    const openSignInModal = () => {
      emitter.emit("onOpenModal", { component: SignInForm, title: "Sign In" });
    };

    const openSignUpModal = () => {
      emitter.emit("onOpenModal", { component: SignUpForm, title: "Sign Up" });
    };

    return {
      openSignUpModal,
      openSignInModal,
    };
  },
});
</script>
<style scoped>
.menu {
  width: 500px;
  min-height: 500px;
}
</style>