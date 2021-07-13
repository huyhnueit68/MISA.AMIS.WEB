import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'
import axios from 'axios'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import store from './store/index.js'
import VueAxios from 'vue-axios'
import 'devextreme/dist/css/dx.light.css';
import vueDebounce from 'vue-debounce'
import excel from 'vue-excel-export'
 
Vue.use(excel)
Vue.use(vueDebounce)
Vue.use(VueSweetalert2);
Vue.use(VueAxios, axios)
Vue.config.productionTip = false

new Vue({
    vuetify,
    router,
    store,
    render: h => h(App)
}).$mount('#app')