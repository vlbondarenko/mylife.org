<template>
  <div id="home">
    This is Home Component
    <button @click="openSignInModal()">Login</button>
    <button @click="openSignUpModal()">Register</button>

    <modal-root />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import ModalRoot from "../components/common/ModalRoot.vue";
import SignUpForm from "../components/auth/SignUpForm.vue";
import SignInForm from "../components/auth/SignInForm.vue";
import useEmitter from "../helpers/emitter";
export default defineComponent({
  name: "HomePage",
  components: {
    ModalRoot,
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const emitter = useEmitter();

    if (store.state.user.loggedIn) {
      router.push("/user");
    }

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