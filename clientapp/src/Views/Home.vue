<template>
    <div id="home">
      <div class="home">
        This is Home Component
      </div>
      <login />
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import Login from '../components/Login/Login.vue'


@Options({
  components:{
    Login
  }
})
export default class Home extends Vue{}

</script>

<style>
.home{
  background: url("../assets/bg.jpg");
  background-position: 50% 50%;
  background-repeat: no-repeat;
  -webkit-background-size: cover;
  -moz-background-size: cover;
  -o-background-size: cover;
  background-size: cover;
  height: 99%;
  width: 50%;
  z-index: -1;
  position: absolute;
}
</style>