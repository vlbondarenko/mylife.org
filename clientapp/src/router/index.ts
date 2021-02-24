import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Pages/Home.vue'
import Slider from '../components/HomePage/Slider.vue'
import ConfirmEmailSuccess from '@/Pages/ConfirmEmailSuccess.vue'
import ConfirmEmailFailure from '@/Pages/ConfirmEmailFailure.vue'
import ConfirmResultRoot from '@/components/common/ConfirmResultRoot.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path:'/',
        name:'Home',
        component: Home,
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
        //component: () => import(/* webpackChunkName: "user" */'../components/HomePage/Slider.vue')
        component: Slider
    },
    {
        path:'/confirm-email-success',
        name:'ConfirmEmailSuccess',
        component:ConfirmEmailSuccess
    },
    {
        path:'/confirm-email-failure',
        name:'ConfirmEmailFailure',
        component:ConfirmEmailFailure
    },
    {
        path:'/reset-password',
        name:'ConfirmResultRoot',
        component:ConfirmResultRoot,
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router