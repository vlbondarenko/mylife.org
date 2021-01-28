<template>
    <div>
        <form
            ref="formRef"
            @submit.prevent="handleSubmit"
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
         <div v-if="message" class="alert alert-danger" role="alert">
                {{ message }}
        </div>
          </div>  
</template>

<script lang="ts">
import router from "@/router";
import { defineComponent, reactive, ref } from "vue";
import { useStore } from "vuex";

export default defineComponent({
    name: 'Login',

    setup(){
        const formRef = ref<HTMLFormElement|null>(null)
        const store = useStore()
        const form = reactive({
            email:'',
            password:'',
        })
        const message = ref('')

        const handleSubmit = () => {
            if (!formRef.value?.checkValidity()) return

            store.dispatch('userModule/Login',form).then(
                (data)=>{
                    if(data)
                        router.push('/user')
                },
                error=>{
                    message.value = error
                }
            )
           
        }

        return {
            formRef,
            form,
            message,
            handleSubmit
        }
    }
}) 
</script>