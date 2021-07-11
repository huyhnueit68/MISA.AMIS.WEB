import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        /**
         * set form model for action api
         * false: add, true: edit
         */
        formMode: 1,
        /**
         * set loading UI
         */
        isLoading: false,
        /**
         * Current page in paging
         */
        currentPage: 1,
        /**
         * Page size
         */
        pageSize: 20,
        /**
         * Total page
         */
        totalPage: 1,
        /**
         * Total record
         */
        totalRecord: 1,
        /**
         * State search
         */
        stateSearch: false,
        /**
         * Value search
         */
        valueSearch: '',
    },
    mutations: {
        /**
         * Function commit disable loading page
         * @param {boolen} state 
         * CreatedBy: PQ Huy (08.07.2021)
         */
        DisableLoading(state) {
            state.isLoading = false;
        },
        /**
         * Function commit enable loading page
         * @param {boolen} state 
         * CreatedBy: PQ Huy (08.07.2021)
         */
        EnableLoading(state) {
            state.isLoading = true;
        }
    }
})