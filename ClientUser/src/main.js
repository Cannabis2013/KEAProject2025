import './styling/main.css'
import './styling/al-boxmodel-utils.css'
import './styling/al-layout-utils.css'
import {createApp} from 'vue'
import App from './app/app.vue'
import router from './router'

const app = createApp(App)

app.use(router)
app.mount('#app')