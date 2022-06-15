<template>
  <div class="flex jc-ai-center footer">
    <a class="footer-link" href="">{{ t('homePage.footer.about') }}</a>
    <a class="footer-link" href=""> {{ t('homePage.footer.privacy') }}</a>
    <a class="footer-link" href="">{{ t('homePage.footer.contacts') }} </a>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import useLocalizer from "@/helpers/localizer";

export default defineComponent({
  setup() {
    const { t } = useLocalizer()

    return { t }
  }
});
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