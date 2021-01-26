import Vue from 'vue'
import VueRouter, { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Views/Home.vue'
import User from '../Views/User.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path:'/',
        name:'Home',
        component: Home
    },
    {
        path:'/user',
        name:'User',
        component: User
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router