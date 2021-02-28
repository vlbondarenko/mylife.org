<template>
  <SubPage>
    <div class="f-column jc-ai-center f-g-1">
      <div class="f-column jc-ai-center">
        <p class="title">An error occurred</p>
        <div class="f-s-1 content">
          An error occurred while confirming the email address. Please repeat
          the procedure through your profile settings
        </div>
        <Button @onClick="goHome" :buttonText="'Home'" />
      </div>
    </div>
  </SubPage>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import SubPage from "@/components/common/SubPage.vue";
import Button from "@/components/common/Button.vue";

export default defineComponent({
  components: { SubPage, Button },
  setup() {
    const router = useRouter();

    const goHome = () => {
      router.push("/");
    };

    return {
      goHome,
    };
  },
});
</script>