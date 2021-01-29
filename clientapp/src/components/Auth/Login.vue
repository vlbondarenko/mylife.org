<template>
    <div class="modal-wrapper">
        <div  class="modal-body">
        <slot name="header"></slot>
        <form 
            ref="formRef"
            @submit.prevent="handleSubmit"
            >
            <input
                v-model="form.email"
                class="type-one"
                type="email"
                required
                placeholder="Email"
            >
            <input
                v-model="form.password"
                class="type-one"
                type="password"
                required
                placeholder="Password"
            >
            <button
                class="btn btn-lg "
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

<style>

.modal-wrapper{
    position: fixed;
    overflow: auto;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.5);
}
.modal-body{
   position: absolute;
    left: 50%;
    top: 50%;
    margin-left: -250px;
    margin-top: -250px;
    background-color: #fff;
    width: 500px;
    height: 500px;
    border-radius: 50px;
    padding: 30px;
    box-shadow:0 0 30px 10px rgba(0, 0, 0, 0.2);
  
    
}

input {
  display:block;
  width:80%;
  padding:10px;
  outline: none;
}

.type-one{
    border-radius: 20px;
    border: 0;
    margin:50px 0;
    margin-left: 45px;
    box-shadow:0 0 20px 4px rgba(0, 0, 0, 0.1);
    transition: .5s background-color;
}
.type-one:hover{
    background-color: #86868633;
}
.close{
    border-radius: 50%;
    color: rgb(19, 18, 18);
    background: #2a4cc7;
    display: flex;
    align-items: center;
    justify-content: center;
    position: absolute;
    top: 7px;
    right: 7px;
    width: 30px;
    height: 30px;
    cursor: pointer;
   }

   .title{
       margin-top: 40px;
       text-align: center;
       font-size: 35px;
       text-shadow: 3px 0 10px rgba(0, 0, 0, 0.1);
       font-weight:bolder;
   }
    .btn{
        position: absolute;
        left: 50%;
        border: 1px solid rgb(0, 0, 0);
        width: 100;
    }

</style>