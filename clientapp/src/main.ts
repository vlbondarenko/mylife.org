import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './styles/styles.css'
import 'font-awesome/css/font-awesome.min.css'
import mitt from 'mitt'

const emitter = mitt()

let app = createApp(App)
app.config.globalProperties.emitter = emitter
app.use(router).use(store).mount('#app')

