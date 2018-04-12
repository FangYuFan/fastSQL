import Vue from 'vue';
import axios from 'axios';

import App from './App';
import router from './router';
import store from './store';
import storage from 'vue-localstorage';
import VueCodemirror from 'vue-codemirror'
import vClickOutside from 'v-click-outside'

if (!process.env.IS_WEB) Vue.use(require('vue-electron'));
Vue.use(vClickOutside)
Vue.use(storage)
Vue.use(VueCodemirror)
Vue.http = Vue.prototype.$http = axios;
Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  components: { App },
  router,
  store,
  template: '<App/>'
}).$mount('#app')
