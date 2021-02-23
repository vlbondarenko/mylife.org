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
            .then( response => {
                commit('SET_LOGGEDIN',true)
                commit('SET_USER',response.data)
                return Promise.resolve(response.data)
            },error=>{
                commit('SET_LOGGEDIN',false)
                const message = (error.response&&error.response.data&&error.response.data.error.Message)||
                error.message||
                error.toString()
                return Promise.reject(message);
            })
        },

        Register ({commit},userData: any): Promise<any>{
            return authService.register(userData).
            then(response=>{
                const message = 'Registration was successful! A message was sent to you with a link to confirm your email address. Follow the link and log in to start using the service!'
                return Promise.resolve(message)
            },
            error=>{
                const message = (error.response&&error.response.data&&error.response.data.error.Message)||
                error.message||
                error.toString()
                return Promise.reject(message);
            })
        },

        Logout({commit}):void{
            authService.logout()
            commit('SET_LOGGEDIN',false)
        }
    }
}

export default userModule


