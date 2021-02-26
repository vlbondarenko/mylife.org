<template>
  <div>
    <div class="f-column jc-ai-center" v-show="!message">
      <form @submit.prevent="handleSubmit">
        <div class="conteiner f-column">
          <input
          v-model="emailAdress"
          type="text"
          placeholder=" "
          @blur="v.emailAdress.$touch()"
        />
         <label>Email</label>
        <span v-if="v.emailAdress.$invalid && v.emailAdress.$dirty">{{
          v.emailAdress.$errors[0].$message
        }}</span>
        </div>
        <div class="conteiner">
<input
          v-model="password"
          type="password"
          placeholder=" "
          @blur="v.password.$touch()"
        />
        <label>Password</label>
        <span v-if="v.password.$invalid && v.password.$dirty">{{
          v.password.$errors[0].$message
        }}</span>
        </div>
        
        <button class="floating-button" type="submit">Sign in</button>
      </form>
      <a @click="openForgotPasswordForm">Forgot Password?</a>
    </div>

    <message v-show="message" @closeMessage="handleCloseMessage">
      {{ message }}
    </message>
  </div>
</template>

<script lang = "ts">
import router from "@/router";
import { defineComponent, ref } from "vue";
import { useStore } from "vuex";
import { required } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import useEmitter from "@/helpers/emitter";
import Message from "../common/Message.vue";
import ForgotPasswordForm from "./ForgotPasswordForm.vue";

export default defineComponent({
  name: "SignInForm",
  components: {
    Message,
  },
  setup() {
    const store = useStore();
    const emitter = useEmitter();

    const emailAdress = ref("");
    const password = ref("");

    const v = useVuelidate(
      {
        emailAdress: { required },
        password: { required },
      },
      { emailAdress, password }
    );

    const message = ref("");

    const handleCloseMessage = () => {
      message.value = "";
    };

    function handleSubmit() {
      v.value.$touch();
      if (v.value.$error) return;
      const loginData = {
        email: emailAdress.value,
        password: password.value,
      };
      store.dispatch("user/SignIn", loginData).then(
        (data) => {
          if (data) router.push("/user");
        },
        (errorMessage) => {
          message.value = errorMessage;
        }
      );
    }

    const openForgotPasswordForm = () => {
      emitter.emit("onOpenModal", {
        component: ForgotPasswordForm,
        title: "Forgot Password",
      });
    };

    return {
      emailAdress,
      password,
      v,

      message,
      handleCloseMessage,

      handleSubmit,
      openForgotPasswordForm,
    };
  },
});
</script>

<style scoped>
a:hover {
  border-bottom: 1px solid rgb(0, 0, 0);
  cursor: pointer;
  color: rgb(0, 0, 0)
}

a{
  border-bottom: 1px solid rgb(100, 100, 100);
  font-size: 14px;
  color: rgb(100, 100, 100);
}

input{
  margin: 40px 20px 5px 20px;
  width: 300px;
  border-radius: 10px;
  border: 0;
  transition: .5s background-color;
  font-family: "Poppins";
}

input:hover {
    background-color: rgb(245, 245, 245);
}

form{
  display: flex;
  flex-direction: column;
}

button{
  left:90px;
}

label{
  color: rgb(131, 131, 131);
    font-size: 14px;
    line-height: 16px;
    padding: 5px 20px;
    pointer-events: none;
    position: absolute;
    transition: all 200ms;
    top: 47px;
    left: 20px;
}

span{
  position: relative;
  color: rgb(255, 0, 0);
  font-size: 12px;
  margin-left: 20px;
}

input:focus + label,
input:not(:placeholder-shown) + label{
    top: 10px;
    left: 0px;
    font-size: 14px; 
    color: rgb(0, 0, 0);
}

.conteiner{
  position: relative;
}


input:focus + span,
input:not(:placeholder-shown) + span{
    animation: light 0.6s;
}

@keyframes light {
  0% {
    color: rgba(0, 0, 0, 1);
  }
  50% {
    color: rgba(0, 0, 0, 0.5);
  }
  100% {
   color: rgba(0, 0, 0, 1);
  }
}
</style>

