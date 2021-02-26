<template>
  <transition  name="fade" v-on:after-leave="$emit('onEndOfTransition')">
    <div class="flex jc-ai-center modal-backdrop blur" v-show="isOpen" >
      <div class="f-column jc-ai-center modal-dialog" :class="{ open: isOpen }" @click.stop>
        <button
          v-show="showCloseButton"
          class="modal-close"
          @click="$emit('onCloseModal')"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="20"
            height="20"
            viewBox="0 0 20 20"
          >
            <path
              d="M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z"
            />
            <path d="M0 0h24v24H0z" fill="none" />
          </svg>
        </button>
        <div class="modal-title" v-if="title">{{ title }}</div>
        <div class="flex jc-ai-center modal-bodys">
          <slot />
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    isOpen: Boolean,
    title: String,
    showCloseButton: Boolean,
  },
});
</script>

<style scoped>
.fade-enter-active{
  transition: all 0.3s ease-out;
}
.fade-leave-active {
  transition: all 0.7s;
  
}

.fade-enter{
  opacity: 1;
}
.fade-leave-to .modal-dialog {
  opacity: 0;
  animation: bounce 0.7s;
}

@keyframes bounce {
  0% {
    transform: scale(1);
  }
  50% {
    transform: scale(0.75);
  }
  100% {
    transform: scale(0.5);
  }
}



.modal-backdrop {
  background: rgba(255, 255, 255, 0.4);
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  
}

.blur {
  backdrop-filter: blur(4px);
}

.modal-dialog {
  background: rgb(238, 238, 238);
  padding: 2rem 2rem;
  box-shadow: 0 4px 7px rgba(0, 0, 0, 0.322);
  border-radius: 20px;
  transition: 0.7s;
  position: relative;
}

.modal-close {
  background: none;
  border: none;
  position: absolute;
  top: 5px;
  right: 10px;
  outline: none;
  height: 2rem;
  width: 2rem;
  display: flex;
  align-items: center;
  justify-content: center;
  
}

.modal-close svg {
  fill: rgb(0, 0, 0);
}

.modal-close svg:hover {
  fill: rgb(148, 148, 148);
}

.modal-title {
  font-weight: bold;
  font-size: 20px;
  color: rgb(0, 0, 0);
}

.modal-bodys {
  color: rgb(0, 0, 0);
  position: relative;
}
</style>
