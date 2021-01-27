import { createRouter, createWebHistory, RouteRecordRaw} from 'vue-router'
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
        component: User,
        meta:{
            requiresAuth: true
        }
        
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

router.beforeEach((to, from, next) => {
    if (to.name === from.name) {
      return next();
    }
    else if (to.matched.some(record => record.meta.requiresAuth)) {
        next({path: "/"});
    } else {
        next();
    }
  });

export default router