import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './styles/styles.css'
import 'font-awesome/css/font-awesome.min.css'
import mitt from 'mitt'
import { createI18n } from 'vue-i18n'
import enUS from './locales/en-US.json'
import ruRU from './locales/ru-RU.json'

const i18n = createI18n({
    legacy: false,
    locale: 'en',
    fallbackLocale: 'ru',
    messages:{
        'en': enUS,
        'ru': ruRU
    }
})

const emitter = mitt()

let app = createApp(App)
app.config.globalProperties.emitter = emitter
app.use(router).use(store).use(i18n).mount('#app')

