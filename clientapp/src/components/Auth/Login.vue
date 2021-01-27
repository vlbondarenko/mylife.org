<template>
    <div>
        <form
            ref="formRef"
            @submit.prevent="login"
            >
              <input
                v-model="form.email"
                class="form-control form-control-lg"
                type="email"
                required
                placeholder="Email"
              >
              <input
                v-model="form.password"
                class="form-control form-control-lg"
                type="password"
                required
                placeholder="Password"
              >
            <button
              class="btn btn-lg btn-primary pull-xs-right"
              :disabled="!form.email || !form.password"
              type="submit"
            >
              Sign in
            </button>
        </form>
          </div>  
</template>

<script lang="ts">
import router from "@/router";
import { defineComponent, reactive, ref } from "vue";

export default defineComponent({
    name: 'Login',

    setup(){
        const formRef = ref<HTMLFormElement|null>(null)
        const form = reactive({
            email:'',
            password:'',
        })

        const login = () => {
            if (!formRef.value?.checkValidity()) return

            const result = true; //здесь должна содержаться логика для проверки аутентификации пользователя
            if(result){
                router.push('/user')
            } else return
        }

        return {
            formRef,
            form,
            login
        }
    }
}) 
</script>