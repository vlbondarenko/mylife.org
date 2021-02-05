<template>
  <div v-show="showModal" class="modal-wrapper">
    <div class="modal-body">
      Title <button class="close" @click="closeModal"><i class="fa fa-close"/></button>
      <form ref="formRef" @submit.prevent="handleSubmit">     
        <input
          type="text"
          id="firstname"
          placeholder="firstname"
          v-model="form.firstname"
          required
        />  
        <input
          type="text"
          id="lastname"
          placeholder="lastname"
          v-model="form.lastname"
          required
        />    
        <input
          type="email"
          id="email"
          placeholder="Email"
          v-model="form.email"
          required
        /> 
        <input
          type="password"
          id="password"
          placeholder="Password"
          v-model="form.password"
        />

        <button class="btn btn-primary btn-block w-75 my-4" type="submit">
          Sign up
        </button>
      </form>
      <div
        v-if="message"
        class="alert"
        :class="successful ? 'alert-success' : 'alert-danger'"
      >
        {{ message }}
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, inject, reactive, ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";

export default defineComponent({
  name: "Register",
  setup() {
    const formRef = ref<HTMLFormElement | null>(null);
    const form = reactive({ 
      email: "",
      firstname: "",
      lastname:"",
      password: "",
    });
    const store = useStore();
    const router = useRouter();
    const message = ref("");
    const successful = ref(false);

    const handleSubmit = () => {
      if (!formRef.value?.checkValidity()) return;

      store.dispatch("userModule/Register", form).then(
        (data) => {
          if (data) {
            router.push("user");
          }
        },
        (error) => {
          message.value = error.message;
          successful.value = false;
        }
      );
    };

    const showModal = inject('showRegisterModal')
    const closeModal = inject('closeRegisterModal')

    return {
      formRef,
      form,
      message,
      successful,
      handleSubmit,
      showModal,
      closeModal
    };
  },
});
</script>
