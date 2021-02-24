<template>
    <div>
        <p>{{ message }}</p>
    </div>  
    <div v-show="!!sourceComponentOfModal" >
        <button @click="onBack">Back</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import useEmitter from '@/helpers/emitter'

export default defineComponent({
    props:{
        message:String,
        sourceComponentOfModal:Object,
        sourceTitleOfModal: String
    },
    setup (props){
        const emitter = useEmitter()

        const onBack = () => {
            emitter.emit('onOpenModal', {component:props.sourceComponentOfModal, title:props.sourceTitleOfModal})
        }

        return {
            onBack,
        }
    }
})
</script>