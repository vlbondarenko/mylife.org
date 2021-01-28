import {Module} from 'vuex'
import authService from '@/services/authService'

const storedUser = localStorage.getItem('user')

const state = {
    loggedIn: storedUser?true:false,
    user:storedUser
}

type UserStateType = typeof state

const userModule: Module <UserStateType,any> = {
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
        Login({commit},authData){
            // return authService.login(authData)
            // .then( user => {
            //     commit('SET_LOGGEDIN',true)
            //     commit('SET_USER',user)
            //     return Promise.resolve(user)
            // },error=>{
            //     commit('SET_LOGGEDIN',false)
            //     const message = (error.response&&error.response.data&&error.response.data.message)||
            //     error.message||
            //     error.toString()
            //     return Promise.reject(message);
            // })

            let userAsSting = JSON.stringify(authData)
            localStorage.setItem('user',userAsSting)
            commit('SET_LOGGEDIN',true)
            commit('SET_USER',userAsSting)
            return Promise.resolve(userAsSting)
        }
    }
}

export default userModule


