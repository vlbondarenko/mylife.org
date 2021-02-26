<template>
   <div class="flex jc-ai-center slider-content">
    <transition @after-leave="onAfterLeave" @after-enter="onAfterEnter" name="fade">
      <span v-if="showSlide">
          {{ slides[index] }}
      </span>    
    </transition>
  </div>
</template>

<script lang="ts">
import { defineComponent,  onMounted,  ref } from "vue";

export default defineComponent({
    setup(){
        const slides =[
            'First slide First slide First slide First slide First slide First slide First slide First slide First slide First slide',
            'Second Slide Second Slide Second Slide Second Slide Second Slide Second Slide Second Slide Second Slide Second Slide Second Slide',
            'Third slide Third slide Third slide Third slide Third slide Third slide Third slide Third slide Third slide Third slide Third slide'
        ]

        const index = ref(2)
        const showSlide = ref(false)

        const nextSlide = () => {
          showSlide.value = true
          if(index.value==slides.length-1)
            index.value=0
          else 
            index.value++
          //setTimeout(()=>{closeSlide()}, 5000)
        }

        const closeSlide = () => {showSlide.value = false}

        
        const onAfterLeave = () => {
            setTimeout(()=>{nextSlide()},1000)
        }

        const onAfterEnter = () =>{
              setTimeout(()=>{closeSlide()},2000)
        }

        onMounted(()=>{showSlide.value=true})

        return {slides, index, showSlide, onAfterLeave, onAfterEnter}
    }
})
</script>
<style scoped>
 .fade-enter-active,
.fade-leave-active {
  transition: opacity 2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

 span {
	display: inline;
	background-color: rgba(238, 238, 238, 0.6);
	box-shadow: 0px 0px 30px 30px rgba(238, 238, 238, 0.6);
	padding:0px;
	color: #000000;
	line-height: 60px;
	font-size: 20px;
  font-weight: 550;
}
</style>