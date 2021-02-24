<template>
  <div id="home">
    <div id="header">This is header</div>
    <div id="container">
      <div id="menu">
        This is Home Component
        <button class="floating-button" @click="openSignInModal()">
          Sign In
        </button>
        <button class="floating-button" @click="openSignUpModal()">
          Sign Up
        </button>
      </div>
      <div id="content"></div>
    </div>
    <div id="footer">Footer</div>
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
<style>
#home{
  width: 100%;
  min-height: 100%;
  background: rgb(187, 219, 155);
  display: flex;
  flex-direction: column;
  position: absolute;
  left: 0;
  top: 0;
}
#footer{
  background: rgb(226, 226, 222);
  height: 70px;
  width: 100%;
  flex: 0 0 auto;
}

#header{
  background: lavender;
  height: 70px;
  width: 100%;
}

#container{
  height: 100%;
  width: 100%;
  background: lightcoral;
  flex: 1 0 auto;
  display: flex;
  flex-direction: row-reverse;
  min-height: 500px;
}

#content{
  min-width: 300px;
  flex: 1 0 auto;
  background: turquoise;
   
}

#menu{
  min-width: 200px;
  flex: 0 0 auto;
  display: flex;
  flex-direction: column;
  background: rgba(34, 36, 36, 0.295);
}
.floating-button {
  display: inline-block;
  width: 140px;
  border-radius: 50px;
  margin: 20px 20px;
  font-family: "Poppins";
  font-size: 11px;
  text-transform: uppercase;
  text-align: center;
  letter-spacing: 3px;
  font-weight: 500;
  color: #000000;
  background: rgb(255, 255, 255);
  box-shadow: 0 4px 7px rgba(0, 0, 0, 0.322);
  transition: 0.3s ease-in-out;
  outline: none;
  padding: 8px;
  border: 1px solid rgba(0, 0, 0, 0.075);
  position: relative;
}
.floating-button:hover {
  background: rgb(94, 94, 94);
  box-shadow: 0 5px 10px rgba(75, 75, 75, 0.774);
  color: #ffffff;
  border: 1px solid rgba(0, 0, 0, 0.075);
}
</style>