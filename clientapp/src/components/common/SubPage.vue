<template>
  <div class="page f-column">
    <Header />
      <slot/>
    <Footer />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { useRouter } from "vue-router";
import Header from "@/components/home/Header.vue";
import Footer from "@/components/home/Footer.vue";

export default defineComponent({
  components: { Header, Footer },
  setup() {
    const router = useRouter();
    const loading = ref(false);

    const goHome = () => {
      router.push("/");
    };


    return {
      loading,
      goHome,
    };
  },
});
</script>