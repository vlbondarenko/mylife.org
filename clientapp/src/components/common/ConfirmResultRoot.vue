<template>
    <div>Confirm Result Root</div>
    <modal-root />
</template>

<script>
import useEmitter from "@/helpers/emitter";
import { defineComponent, onMounted} from "vue";
import {  useRouter } from "vue-router";
import ResetPasswordForm from "../auth/ResetPasswordForm.vue";
import ModalRoot from "./ModalRoot.vue";
export default defineComponent({
    components:{
        ModalRoot
    },
    setup(){    
        const router = useRouter()
        const emitter = useEmitter()

        const openResetPasswordForm = () => {
            emitter.emit('onOpenModal', { component:ResetPasswordForm, title: 'Reset Password', closeButton: false })
            router.push('/reset-password')
        }

        onMounted(()=>{
            openResetPasswordForm()
        })
    }})
</script>