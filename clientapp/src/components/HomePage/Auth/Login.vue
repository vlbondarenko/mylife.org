<template>
  <div v-show="show" class="modal-wrapper">
    <div class="modal-body">
      <div class="title">Login</div> <button class="close" @click="close"><i class="fa fa-close"></i></button>
      <form ref="formRef" @submit.prevent="handleSubmit">
        <input
          v-model="form.email"
          class="type-one"
          type="email"
          required
          placeholder="Email"
        />
        <input
          v-model="form.password"
          class="type-one"
          type="password"
          required
          placeholder="Password"
        />
        <button
          class="btn btn-lg"
          :disabled="!form.email || !form.password"
          type="submit"
        >
          Sign in
        </button>

        <div v-if="message" class="alert alert-danger" role="alert">
          {{ message }}
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import router from "@/router";
import { defineComponent, inject, reactive, ref } from "vue";
import { useStore } from "vuex";

export default defineComponent({
  name: "Login",
  setup() {
    const formRef = ref<HTMLFormElement | null>(null);
    const store = useStore();
    const form = reactive({
        email: "",
        password: "",
    });
    const message = ref("");
    const show = inject('showLoginModal')
    const close = inject('closeLoginModal')

    const handleSubmit = () => {
        if (!formRef.value?.checkValidity()) return;

        store.dispatch("userModule/Login", form).then(
            (data) => {
                if (data) router.push("/user");
            },
            (error) => {
                message.value = error;
            }
        );
    };

  
    


    return {
        formRef,
        form,
        message,
        show,
        handleSubmit,
        close
    };
  },
});
</script>

