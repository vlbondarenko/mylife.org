<template>
    <select :value="selected" @change="$emit('onSelectUpdated', $event.target.value)" class="opt">
        <option v-for="option in options" :key="option.value">
            {{ option.text }}
        </option>
  </select>
</template>

<script>
import { defineComponent } from "vue";

export default defineComponent({
  name: "Select",
  props: {
    options: Array
  },
  emits: ['onSelectUpdated']
});
</script>

<style>
.opt{
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  border-radius: 10px;
  margin: 10px 10px;
  text-align: center;
  letter-spacing: 2px;
  font-family: 'Caveat', cursive;
  font-weight: 500;
  color: #000000;
  background: rgb(255, 255, 255);
  transition: 0.3s ease-in-out;
  outline: none;
  padding: 2px 2px 2px 2px;
  border: 1px solid rgba(0, 0, 0, 0.075);
  position: relative;
  min-width: 20px;
}
</style>