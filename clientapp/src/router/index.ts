import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Views/Home.vue'


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
        component: () => import(/* webpackChunkName: "user" */'../Views/User.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router