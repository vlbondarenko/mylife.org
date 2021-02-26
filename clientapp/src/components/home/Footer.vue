<template>
  <div class="flex jc-ai-center footer">
    <a class="footer-link" href=""> About </a>
    <a class="footer-link" href=""> Privacy Policy </a>
    <a class="footer-link" href=""> Contacts </a>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
export default defineComponent({});
</script>
<style scoped>
.footer {
  height: 70px;
  width: 100%;
}

.footer-link {
  margin: 60px 30px;
  color: rgb(0, 0, 0);
  font-size: 14px;
  font-weight: 500;
}
</style>