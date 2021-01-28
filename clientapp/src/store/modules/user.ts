import {Module} from 'vuex'

const ACCES_TOKEN='Acces_Token'

const storage = localStorage

const state = {
    token: storage.getItem(ACCES_TOKEN),
}

type UserStateType = typeof state

const user:Module<UserStateType,any> = {
    namespaced:true,
    state,
    mutations:{
        SET_TOKEN: (state,token) => {
            state.token=token
        }
    },
    actions:{
        Login({commit}, loginForm){
          
                storage.setItem(ACCES_TOKEN,loginForm.email)
                commit('SET_TOKEN',loginForm.email)
            
        }
    }
}

export default user