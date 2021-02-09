import  Vuex  from 'vuex'
import user from './modules/user.module'

export default new Vuex.Store({
  modules:{
    user:user
  }
})

