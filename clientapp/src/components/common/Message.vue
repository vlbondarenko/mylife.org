<template>
  <div>
    <div>
      <slot />
    </div>
    <button v-show="showBackButton" @click="$emit('closeMessage')">Back</button>
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