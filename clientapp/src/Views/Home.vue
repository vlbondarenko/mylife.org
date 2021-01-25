<template>
    <div id="home">
      <div>
        This is Home Component
      </div>
        
    </div>
</template>

<script lang="ts">
import {Vue} from 'vue-class-component'

export default class Home extends Vue{}
</script>

<style>
#home{
  background: url("../assets/bg.jpg");
  background-position: 50% 50%;
  background-repeat: no-repeat;
  -webkit-background-size: cover;
  -moz-background-size: cover;
  -o-background-size: cover;
  background-size: cover;
  height: 99%;
  width: 99%;
  z-index: -1;
  position: absolute;
}
</style>