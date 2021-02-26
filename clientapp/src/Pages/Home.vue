<template>
  <div class="page f-column">
    <Header />
    <div class="f-inline j-c-center f-w-reverse f-g-1">
      <div class="f-inline f-g-1 j-c-center slider-bg">
        <div class="flex jc-ai-center f-g-1 f-s-1">
          <Slider />
        </div>
      </div>
      <Menu />
    </div>
    <Footer />
    <ModalRoot />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import ModalRoot from "../components/common/ModalRoot.vue";

import Slider from "@/components/home/Slider.vue";
import Header from "@/components/home/Header.vue";
import Footer from "@/components/home/Footer.vue";
import Menu from "@/components/home/Menu.vue";

export default defineComponent({
  name: "HomePage",
  components: {
    ModalRoot,
    Slider,
    Header,
    Footer,
    Menu,
  },
  setup() {
    const store = useStore();
    const router = useRouter();

    if (store.state.user.loggedIn) {
      router.push("/user");
    }
  },
});
</script>
<style scoped>
.page {
  width: 100%;
  min-height: 100%;
  position: absolute;
  left: 0;
  top: 0;
  background: rgb(238, 238, 238);
}

.slider-bg {
  background-image: url("../assets/Fon1.png");
  background-size: cover;
  background-position: 50% 50%;
}
</style>