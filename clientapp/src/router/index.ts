import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
import Home from '../Pages/Home.vue'
import Slider from '../components/HomePage/Slider.vue'
import SignUp from '../components/HomePage/Auth/SignUp.vue'
import SignIn from '../components/HomePage/Auth/SignIn.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path:'/',
        name:'Home',
        component: Home,
        children:[
            {
                path:'sign-up',
                name:'SignUp',
                component:SignUp
            },
            {
                path:'sign-in',
                name:'SignIn',
                component: SignIn
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
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router