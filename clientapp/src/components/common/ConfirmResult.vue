<template>
    <component :is="currentComponent"/>
</template>

<script lang="ts">
import useEmitter from "@/helpers/emitter";
import { defineComponent, shallowRef } from "vue";
export default defineComponent({
    setup(){    
        const emitter = useEmitter()
        
        const currentComponent = shallowRef(null)
        
        emitter.on('onConfirmResult',({component = null}) => {
           currentComponent.value = component
        })

        return {currentComponent}
    }
})
</script>