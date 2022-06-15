<template>
  <div class="flex f-w-reverse a-i-center header">
    <a class="header-link" href="http://localhost:8080/"> MyLife.org </a>
    <Select :value="selectedLanguage" :options="availableLocales" @on-select-updated="onLocaleUpdated" class="select" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import Select from "../common/Select.vue";
import { getLocale, setLocale, getAvailableLocales } from "@/helpers/localizer"

export default defineComponent({
  name: "Header",
  components: {
    Select
  },
  setup() {

    const router = useRouter()

    const onLocaleUpdated = (locale: string) => {
      setLocale(locale);
      router.go(0)
    }

    let availableLocales: Array<any> = new Array<any>();
    getAvailableLocales().forEach(locale => availableLocales.push({ text: locale, value: locale }))

    const selectedLanguage = getLocale()

    return {
      availableLocales,
      selectedLanguage,
      onLocaleUpdated
    }
  },
});
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

.select{
   margin-right: 60px;
  border: none;
  position: absolute;
  top: 5px;
  right: 10px;
  outline: none;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
</style>