import { getCurrentInstance } from 'vue'

function useEmitter() {
    const internalInstance = getCurrentInstance()
    return internalInstance?.appContext.config.globalProperties.emitter
}

export default useEmitter