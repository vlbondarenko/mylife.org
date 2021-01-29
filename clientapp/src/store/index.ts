import  Vuex  from 'vuex'
import userModule from './modules/user'

export default new Vuex.Store({
  modules:{
    userModule:userModule
  }
})

