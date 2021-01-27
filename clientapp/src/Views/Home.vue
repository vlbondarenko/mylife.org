<template>
    <div id="home">
      <div>
        This is Home Component
      </div>
      <login />
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import Login from '../components/Auth/Login.vue'


@Options({
  components:{
    Login
  }
})
export default class Home extends Vue{}

</script>
