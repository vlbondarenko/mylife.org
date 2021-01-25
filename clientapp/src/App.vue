<template>
  <home/>
</template>

<script lang="ts">

import Home from './Views/Home.vue';
import {Options, Vue}  from 'vue-class-component';


 @Options({
   components: {Home
   }
 })
export default class App extends Vue {}
</script>

