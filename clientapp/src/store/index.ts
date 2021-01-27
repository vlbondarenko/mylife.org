import { InjectionKey } from 'vue'
import { createStore, useStore as baseUseStore, Store } from 'vuex'

export interface State {
}

export const key: InjectionKey<Store<State>> = Symbol('')

export const store = createStore<State>({
  state: {
   
  },

  mutations: {
  },

  actions: {
  }
})

export function useStore () {
  return baseUseStore(key)
}
