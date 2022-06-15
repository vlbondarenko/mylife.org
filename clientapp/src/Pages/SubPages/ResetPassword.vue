<template>
    <modal-root />
</template>

<script>
import useEmitter from "@/helpers/emitter";
import { defineComponent, onMounted } from "vue";
import ResetPasswordForm from "@/components/auth/ResetPasswordForm.vue";
import ModalRoot from "@/components/common/ModalRoot.vue";
export default defineComponent({
    components: {
        ModalRoot
    },
    setup() {
        const emitter = useEmitter()

        const openResetPasswordForm = () => {
            emitter.emit('onOpenModal', { component: ResetPasswordForm, title: 'Reset Password', closeButton: false })
        }

        onMounted(() => {
            openResetPasswordForm()
        })
    }
})
</script>