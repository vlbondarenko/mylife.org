import {Module} from 'vuex'
import authService from '@/services/authService'

const storedUser = localStorage.getItem('user')

const state = {
    loggedIn: storedUser?true:false,
    user:storedUser
}


const userModule: Module <typeof state,any> = {
    namespaced:true,
    state,
    mutations:{
        SET_LOGGEDIN:(state, loggedIn) => {
            state.loggedIn=loggedIn
        },
        SET_USER: (state, user) =>{
            state.user=user
        }
    },

    actions:{
        Login({commit},authData): Promise<any> {
            return authService.login(authData)
            .then( user => {
                commit('SET_LOGGEDIN',true)
                commit('SET_USER',user)
                return Promise.resolve(user)
            },error=>{
                commit('SET_LOGGEDIN',false)
                const message = (error.response&&error.response.data&&error.response.data.message)||
                error.message||
                error.toString()
                return Promise.reject(message);
            })

            // let userAsSting = JSON.stringify(authData)
            // localStorage.setItem('user',userAsSting)
            // commit('SET_LOGGEDIN',true)
            // commit('SET_USER',userAsSting)
            // return Promise.resolve(userAsSting)
        },

        Register ({commit},userData: any): Promise<any>{
            return authService.register(userData).
            then(response=>{
                commit('SET_LOGGEDIN',true)
                commit('SET_USER',response.data)
                localStorage.setItem('user',response.data.token)
                return Promise.resolve(response.data)
            },
            error=>{
                commit('SET_LOGGEDIN',false)
                const message = (error.response&&error.response.data&&error.response.data.message)||
                error.message||
                error.toString()
                return Promise.reject(message);
            })
            // let userAsSting = JSON.stringify(userData)
            // localStorage.setItem('user',userAsSting)
            // commit('SET_LOGGEDIN',true)
            // commit('SET_USER',userAsSting)
            // return Promise.resolve(userAsSting)
        },

        Logout({commit}):void{
            authService.logout()
            commit('SET_LOGGEDIN',false)
        }
    }
}

export default userModule


