<template>
  <div class="page f-column">
    <Header/>
    <div class="f-inline j-c-center f-w-reverse f-g-1">
      <div class="f-inline f-g-1 j-c-center slider-bg">
        <div class="flex jc-ai-center f-g-1 f-s-1">
            <Slider/>
        </div>
      </div>
      <div class="f-column jc-ai-center menu" >
        This is Home Component
        <button class="floating-button" @click="openSignInModal()">
          Sign In
        </button>
        <button class="floating-button" @click="openSignUpModal()">
          Sign Up
        </button>
      </div>
      
    </div>
    <Footer/>
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
import Slider from "@/components/home/Slider.vue";
import Header from "@/components/home/Header.vue"
import Footer from "@/components/home/Footer.vue"


export default defineComponent({
  name: "HomePage",
  components: {
    ModalRoot,
    Slider,
    Header,
    Footer
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