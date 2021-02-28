<template>
  <div class="container">
    <input
      :type="inputType"
      placeholder=" "
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
      @blur="validator.$touch()"
    />
    <label>{{ inputLabel }}</label>
    <div class="error-notif">
      <p></p>
      <transition name="error">
        <span v-if="validator.$invalid && validator.$dirty">{{
          validator.$errors[0].$message
        }}</span>
      </transition>
    </div>
  </div>
</template>

<script>
import { defineComponent } from "vue";

export default defineComponent({
  name: "Input",
  props: {
    inputType: String,
    inputLabel: String,
    validator: Object,
    modelValue: String,
  },
  emits: ["update:modelValue"],
  setup() {},
});
</script>

<style scoped>
.error-enter-active,
.error-leave-active {
  transition: opacity 0.5s;
}

.error-enter-from,
.error-leave-to {
  opacity: 0;
}

.error-leave-from {
  opacity: 1;
}

.error-notif {
  display: inline-flex;
}

.container {
  position: relative;
}

label {
  color: rgb(131, 131, 131);
  font-size: 14px;
  line-height: 16px;
  padding: 5px 20px;
  pointer-events: none;
  position: absolute;
  transition: all 200ms;
  top: 7px;
  left: 20px;
}
p {
  margin-bottom: 0px;
}

input {
  margin: 40px 20px 5px 20px;
  width: 300px;
  border-radius: 10px;
  border: 0;
  transition: 0.5s background-color;
  font-family: "Poppins";
}

input:hover {
  background-color: rgb(245, 245, 245);
}

span {
  position: relative;
  color: rgb(255, 68, 68);
  font-size: 12px;
  margin-left: 20px;
  transition: all 0.5s;
}

input:focus + label,
input:not(:placeholder-shown) + label {
  top: -27px;
  left: 0px;
  font-size: 14px;
  color: rgb(0, 0, 0);
}

input:focus + span,
input:not(:placeholder-shown) + span {
  animation: light 0.6s;
}

@keyframes light {
  0% {
    color: rgba(0, 0, 0, 1);
  }
  50% {
    color: rgba(0, 0, 0, 0.5);
  }
  100% {
    color: rgba(0, 0, 0, 1);
  }
}
</style>

