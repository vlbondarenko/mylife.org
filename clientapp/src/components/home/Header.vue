<template>
  <div class="flex j-c-start a-i-center header">
    <a class="header-link" href="http://localhost:8080/"> MyLife.org </a>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
export default defineComponent({});
</script>
<style scoped>
.header {
  height: 50px;
  width: 100%;
}

.header-link {
  margin-left: 60px;
  text-decoration: none;
  color: rgb(0, 0, 0);
  font-size: 20px;
  font-weight: 500;
}
</style>