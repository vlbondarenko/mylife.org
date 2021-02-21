<template>
  <Modal :isOpen="isOpen" :title="currentTitle" @onClose="handleClose" @onEndOfTransition="clearData">
      <keep-alive :max="1">
            <component :is="currentComponent"/>
      </keep-alive>    
  </Modal>
</template>

<script lang="ts">
import Modal from '../common/Modal.vue'
import { defineComponent, ref, shallowRef} from 'vue'
import useEmitter from '@/helpers/emitter'
import { useRouter } from 'vue-router'

export default defineComponent({
    components:{
        Modal
    },
    setup(){
        const currentComponent = shallowRef(null)
        const currentTitle = ref('')
        const isOpen = ref(false)

        const router = useRouter()
        const emitter = useEmitter()


        emitter.on('open',({component = null, title = ''}) => {
           currentComponent.value = component
           currentTitle.value = title
           isOpen.value = true
        })

        const handleClose = () => {
            isOpen.value = false
            router.go(-1)
        }

        //There is a problem: if you assign 'null' to the 'currentComponent' immediately after catching the 'onClose' event, 
        //the content of the modal window will be cleared before the transition ends, which looks ugly. 
        //Therefore, we wait for the transition to end, intercept the 'onEndOfTransition' event and only then assign the 'null' value
        const clearData = () => {
            currentComponent.value = null
            currentTitle.value = ''
        }

        return {
            currentComponent,
            currentTitle,
            handleClose,
            isOpen,
            clearData
        }
    }
})
</script>