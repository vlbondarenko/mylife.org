<template>
  <div>
    <button class="floating-button" :disabled="loading" type="{{buttonType}}">
      {{ buttonText }}
      <svg
        v-if="loading"
        viewBox="1 2 40 40"
        class="spinner circle"
        @click.stop
      >
        <circle cx="20" cy="20" r="15"></circle>
      </svg>
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    loading: Boolean,
    buttonText: String,
    buttonType: String,
  },
});
</script>

<style scoped>
.floating-button {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 12;
  border-radius: 50px;
  margin: 20px 20px;
  font-family: "Poppins";
  text-transform: uppercase;
  text-align: center;
  letter-spacing: 3px;
  font-weight: 500;
  color: #000000;
  background: rgb(255, 255, 255);
  box-shadow: 0 4px 7px rgba(0, 0, 0, 0.322);
  transition: 0.3s ease-in-out;
  outline: none;
  padding: 6px 25px 6px 25px;
  border: 1px solid rgba(0, 0, 0, 0.075);
  position: relative;
}

.floating-button:hover {
  background: rgb(94, 94, 94);
  box-shadow: 0 5px 10px rgba(75, 75, 75, 0.774);
  color: #ffffff;
  border: 1px solid rgba(0, 0, 0, 0.075);
}

button:disabled {
  color: rgb(100, 100, 100, 0.75);
  background: rgb(190, 190, 190, 0.3);
}
button:disabled:hover {
  color: rgb(100, 100, 100, 0.75);
  background: rgb(190, 190, 190, 0.3);
}
svg.spinner {
  width: 16px;
  height: 16px;
  top: 0px;
  left: 10px;
  position: relative;
}

circle {
  fill: transparent;
  stroke: rgb(100, 100, 100);
  stroke-width: 4;
  stroke-linecap: round;
  stroke-dasharray: 125.6;
  transform-origin: 20px 20px 0;
  animation: spinner 3s linear infinite;
}

@keyframes spinner {
  0% {
    stroke-dashoffset: 26.4;
    transform: rotate(0deg);
  }
  50% {
    stroke-dashoffset: 125.6;
    transform: rotate(720deg);
  }
  100% {
    stroke-dashoffset: 26.4;
    transform: rotate(1080deg);
  }
}
</style>