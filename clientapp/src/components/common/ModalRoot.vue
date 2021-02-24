<template>
  <Modal
    :isOpen="isOpen"
    :title="currentTitle"
    :showCloseButton="showCloseButton"
    @onCloseModal="handleClose"
    @onEndOfTransition="clearData"
  >
    <component :is="currentComponent" />
  </Modal>
</template>

<script lang="ts">
import Modal from "../common/Modal.vue";
import { defineComponent, ref, shallowRef } from "vue";
import useEmitter from "@/helpers/emitter";

export default defineComponent({
  components: {
    Modal,
  },
  setup() {
    const emitter = useEmitter();

    const currentComponent = shallowRef(null);
    const currentTitle = ref("");
    const isOpen = ref(false);
    const showCloseButton = ref(true);

    emitter.on(
      "onOpenModal",
      ({ component = null, title = "", closeButton = true }) => {
        currentComponent.value = component;
        currentTitle.value = title;
        isOpen.value = true;
        showCloseButton.value = closeButton;
      }
    );

    const handleClose = () => {
      isOpen.value = false;
    };

    //There is a problem: if you assign 'null' to the 'currentComponent' immediately after catching the 'onClose' event,
    //the content of the modal window will be cleared before the transition ends, which looks ugly.
    //Therefore, we wait for the transition to end, intercept the 'onEndOfTransition' event and only then assign the 'null' value
    const clearData = () => {
      currentComponent.value = null;
      currentTitle.value = "";
    };

    return {
      currentComponent,
      currentTitle,
      handleClose,
      isOpen,
      clearData,
      showCloseButton,
    };
  },
});
</script>