<template>
  <div class="f-column jc-ai-center menu">
  <div class="f-column jc-ai-center title">{{t('homePage.menu.title')}}</div>
    <Button
      :buttonText="t('homePage.menu.signIn')"
      @onClick="openSignInModal()"
    />
    <Button
      :buttonText="t('homePage.menu.signUp')"
      @onClick="openSignUpModal()"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SignUpForm from "../auth/SignUpForm.vue";
import SignInForm from "../auth/SignInForm.vue";
import useEmitter from "@/helpers/emitter";
import Button from "../common/Button.vue";
import useLocalizer  from "@/helpers/localizer";

export default defineComponent({
  components: { Button },
  setup() {
    const emitter = useEmitter();

    const {t} = useLocalizer();

    const openSignInModal = () => {
      emitter.emit("onOpenModal", { component: SignInForm, title: t('homePage.menu.signIn') });
    };

    const openSignUpModal = () => {
      emitter.emit("onOpenModal", { component: SignUpForm, title: t('homePage.menu.signUp') });
    };

    return {
      openSignUpModal,
      openSignInModal,
      t
    };
  },
});
</script>

<style scoped>
.menu {
  width: 500px;
  min-height: 500px;
}

.title{
  text-align: center;
  margin-left: 40px;
  margin-right: 40px;
  font-size: 18px;
}
</style>