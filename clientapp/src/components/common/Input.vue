<template>
  <div class="conteiner">
    <input
    :value="modelValue"
   @input="$emit('update:modelValue', $event.target.value)"
      :type="inputType"
      placeholder=" "
      @blur="validator.$touch()"
      
    />
    <label>{{inputLabel}}</label>
    <div class="notif">
      <p></p>
      <transition name="form">
        <span v-if="validator.$invalid && validator.$dirty">{{
          validator.$errors[0].$message
        }}</span>
      </transition>
    </div>
  </div>
</template>

<script>
import { defineComponent} from "vue";


export default defineComponent({
  name: "Input",
  props:{
     inputType:String,
     inputLabel:String,
     validator:Object,
     modelValue:String
  },
  emits:['update:modelValue'],
  setup(){
  }
  })
</script>

<style scoped>
.form-enter-active {
  transition: opacity 0.4s;
}
.form-leave-active {
  transition: opacity 0.5s;
}

.form-enter-from {
  opacity: 0;
}

.form-leave-from {
  opacity: 1;
}
.form-leave-to {
  opacity: 0;
}

.fader-enter-active,
.fader-leave-active {
  transition: height 0.5s;
}
.fader-enter-from,
.fader-leave-to {
  height: 0;
  margin: 0;
  border: 0;
}

a:hover {
  border-bottom: 1px solid rgb(0, 0, 0);
  cursor: pointer;
  color: rgb(0, 0, 0);
}

.notif {
  display: inline-flex;
}

a {
  border-bottom: 1px solid rgb(160, 160, 160);
  font-size: 11px;
  color: rgb(160, 160, 160);
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

form {
  display: flex;
  flex-direction: column;
}

button {
  max-width: 150px;
  margin-top: 10px;
  left: 90px;
}

label {
  color: rgb(131, 131, 131);
  font-size: 14px;
  line-height: 16px;
  padding: 5px 20px;
  pointer-events: none;
  position: absolute;
  transition: all 200ms;
  top: 47px;
  left: 20px;
}
p {
  margin-bottom: 0px;
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
  top: 13px;
  left: 0px;
  font-size: 14px;
  color: rgb(0, 0, 0);
}

.conteiner {
  position: relative;
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

