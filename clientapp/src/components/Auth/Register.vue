<template>
  <div>
    <form
        @submit.prevent="registerUser"
        >
        <input
            type="text"
            id="firstName"
            placeholder="firstName"
            v-model="register.firstName"
            required
        />
        <input
            type="text"
            id="lastName"
            placeholder="lastName"
            v-model="register.lastName"
            required
        />
        <input
            type="email"
            id="email"
            placeholder="Email"
            v-model="register.email"
            required
        />
        <input
            type="password"
            id="password"
            placeholder="Password"
            v-model="register.password"
        />
       
        <button
            class="btn btn-primary btn-block w-75 my-4"
            type="submit"
            >
            Sign up
        </button>
           
           
        </form>
    </div>
</template>
<script>
import { defineComponent } from "vue";

export default defineComponent({
    setup(){
        const register = {
            firstName: "",
            lastName: "",
            email: "",
            password: ""
        }

       const registerUser = () => {}

       return {
           register,
           registerUser
       }
    }
})
</script>
