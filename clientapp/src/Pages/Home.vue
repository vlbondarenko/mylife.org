<template>
  <div id="home">
    <div id="header">
      <p>This is header</p>
    </div>
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
      <div id="content">
        <div class="content-text">This is content this is content
          This is content this is content
          This is content this is content
          This is content this is content
          This is content this is content
          This is content this is content
          This is content this is content
        </div>
      </div>
    </div>
    <div id="footer">
      <p>Footer</p>
    </div>
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


body{
  font-family: "Poppins";
}

.img{
  height: 500px;
  width: 500px;
}

.content-text{
  height: 200px;
  width: 300px;
  flex:0 0 auto;
 background: linear-gradient(rgba(0,0,0,1), rgba(0,0,0,0));
}
#home{
  width: 100%;
  min-height: 100%;
  display: flex;
  flex-direction: column;
  position: absolute;
  left: 0;
  top: 0;
  flex:1 1 auto;
  justify-content: center;
  align-items: center;
 z-index: 0;
 background: rgb(238, 238, 238);
}
#footer{
  height: 50px;
  width: 100%;
  flex: 0 0 auto;
  justify-content: center;
  align-items: center;
  display: flex;
}

#header{
  height: 50px;
  width: 100%;
  flex: 0 0 auto;
  justify-content: center;
  align-items: center;
  display: flex;
}

#container{
  height: 100%;
  width: 100%;
  flex: 1 0 auto;
  display: flex;
  flex-direction: row-reverse;
  flex-wrap: wrap;
  justify-content: center;
}

#content{
  min-width: 500px;
   min-height: 500px;
  flex: 1 0 auto;
  background-image:  url("../assets/Fon1.png");
  background-size: cover;
  background-position: 50% 50%;
  display: flex;
  justify-content: center;
  align-items: center;
}

#menu{
  width: 500px;
  flex: 0 0 auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  min-height: 500px;
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