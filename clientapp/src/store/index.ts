import { InjectionKey } from 'vue'
import { createStore, useStore as baseUseStore, Store } from 'vuex'

export interface State {
  loggedIn: boolean;
}

export const key: InjectionKey<Store<State>> = Symbol('')

export const store = createStore<State>({
  state: {
    loggedIn: false
  },

  mutations: {
    SET_LOGGEDIN (state, loggedIn) {
      state.loggedIn = loggedIn
    }
  },

  actions: {
    setLoggedIn ({ commit }, loggedIn) {
      this.commit('SET_LOGGEDIN', loggedIn)
    }
  }
})

export function useStore () {
  return baseUseStore(key)
}
