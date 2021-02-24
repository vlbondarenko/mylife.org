import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Pages/Home.vue'
import Slider from '../components/HomePage/Slider.vue'
import SignUpForm from '../components/auth/SignUpForm.vue'
import SignInForm from '../components/auth/SignInForm.vue'
import ConfirmEmailSuccess from '../components/auth/ConfirmEmailSuccess.vue'
import ConfirmEmailFailure from '../components/auth/ConfirmEmailFailure.vue'
import ForgotPasswordForm from '@/components/auth/ForgotPasswordForm.vue'
import ResetPasswordForm from '@/components/auth/ForgotPasswordForm.vue'
import ConfirmResultRoot from '@/components/common/ConfirmResultRoot.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path:'/',
        name:'Home',
        component: Home,
        children:[
            {
                path:'/sign-up',
                name:'SignUp',
                component:SignUpForm
            },
            {
                path:'/sign-in',
                name:'SignIn',
                component: SignInForm
            },
            {
                path:'/forgot-password',
                name:'ForgotPassword',
                component: ForgotPasswordForm
            }
        ]
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