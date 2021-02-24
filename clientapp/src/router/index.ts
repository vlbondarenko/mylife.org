import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Pages/Home.vue'
import ConfirmEmailSuccess from '@/Pages/SubPages/ConfirmEmailSuccess.vue'
import ConfirmEmailFailure from '@/Pages/SubPages/ConfirmEmailFailure.vue'
import ResetPasswordFailure from '@/Pages/SubPages/ResetPasswordFailure.vue'
import ResetPassword from '@/Pages/SubPages/ResetPassword.vue'

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
        path:'/reset-password-failure',
        name:'ResetPasswordFailure',
        component:ResetPasswordFailure,
    },
    {
        path:'/reset-password',
        name:'ResetPassword',
        component:ResetPassword,
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router