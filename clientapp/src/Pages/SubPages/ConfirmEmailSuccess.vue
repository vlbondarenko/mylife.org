<template>
  <div>
    <div>Congratulations!</div>
    <div>
      <button class="floating-button" disabled @click="goHome">
        Password
        <svg v-if="true" viewBox="1 2 40 40" class="spinner circle">
          <circle cx="20" cy="20" r="15"></circle>
        </svg>
      </button>
    </div>
    <Button @click="onClick" :buttonType="'submit'" :loading="loading" :buttonText="'confirm password'"/>
  </div>
</template>

<script lang="ts">
import Button from "@/components/common/Button.vue";
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";

export default defineComponent({
  components: { Button },
  setup() {
    const router = useRouter();
    const loading = ref(false)

    const goHome = () => {
      router.push("/");
    };

    const onClick = () =>{
        loading.value = !loading.value
    }

    return {
        loading,
        onClick,
      goHome,
    };
  },
});
</script>

<style scoped>
svg.spinner {
  width: 16px;
  height: 16px;
  top: 0px;
  left: 10px;
  position: relative;
}

button {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 12;
}
circle {
  fill: transparent;
  stroke: #000000;
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

html,
body {
  height: 100%;
  background-image: linear-gradient(-105deg, #009acc, #363795);
}

.loading {
  background-image: url("https://loading.io/spinners/progress/index.progress-bar-preloader.svg");
  background-repeat: no-repeat;
  background-size: 300px;
  background-position: -40px -134px;
  outline: none;
  border: 1px;
  padding: 4px 8px 4px 8px;
}

.path {
  stroke: hsl(210, 70, 75);
  stroke-linecap: round;
  animation: dash 1.5s ease-in-out infinite;
}

@keyframes rotate {
  100% {
    transform: rotate(360deg);
  }
}

@keyframes dash {
  0% {
    stroke-dasharray: 1, 150;
    stroke-dashoffset: 0;
  }
  50% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -35;
  }
  100% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -124;
  }
}
</style>