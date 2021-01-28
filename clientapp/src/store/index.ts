import { createStore } from 'vuex'
import userModule from './modules/user'

const store = createStore({
  modules:{
    userModule
  }
})

export default store
