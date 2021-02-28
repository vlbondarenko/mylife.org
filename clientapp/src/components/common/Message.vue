<template>
  <div class="flex jc-ai-center">
     <button v-show="showBackButton" @click="$emit('closeMessage')">
      <i class="fa fa-angle-left fa-2x" aria-hidden="true"></i>
    </button>
    <div class="message-body">
      <slot />
    </div>
   
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    showBackButton: {
      type: Boolean,
      default: true,
    },
  },
});
</script>
<style scoped>
.message-body{
  padding: 40px 20px 20px 20px ;
  min-width: 200px;
  max-width: 300px;
  position: relative;
  left: 5px;
}
button{
  border: 0;
  position: absolute;
  outline: none;
  top:-53px;
  left: -27px;
  background: none;
  cursor: pointer;
}
i{
  width: 20px;
  height: 20px;
  color: rgb(0, 0, 0);
}
button i:hover{
  color: rgb(150, 150, 150);
}
</style>