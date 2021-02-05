import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Pages/Home.vue'


const routes: Array<RouteRecordRaw> = [
    {
        path:'/',
        name:'Home',
        component: Home
    },
    {
        path:'/user',
        name:'User',
        //lazy load
        component: () => import(/* webpackChunkName: "user" */'../Pages/User.vue')
    },
    {
        path:'/confirm-email',
        name:'Slider',
        //lazy load
        component: () => import(/* webpackChunkName: "user" */'../components/HomePage/Slider.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router