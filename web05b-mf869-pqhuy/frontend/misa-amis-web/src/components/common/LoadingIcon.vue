<template>
    <div class="box-loading" v-show="this.$store.state.isLoading">
        <div class="loading-icon">
            <img src="../../assets/icon/loading.svg" alt="" class="loading-svg">   
        </div>
    </div>
</template>
<script>
export default {
    setup() {
        
    },
    data() {
        return {
            isShow: false
        }
    },
    methods: {
        /**
         * Function set active loading icon
         * CreatedBy: PQ Huy (05.07.2021)
         */
        activeShowIcon(){
            this.isShow = !this.isShow;
        }
    }
}
</script>
<style scoped>
    .box-loading{
        width: 100%;
        height: 100%;
        position: absolute;
        top: 0;
        left: 0;
        z-index: 15;
        justify-content: center;
        align-items: center;
        background-color: rgb(248 248 248 / 50%);
    }
    .loading-icon{
        position: relative;
        justify-content: center;
        display: flex;
        top: 50%;
        left: 0;
        align-self: center;
    }
    .loading-svg {
        position: absolute;
        width: 48px;
        height: 48px;
        -webkit-animation: spin 4s linear infinite;
        -moz-animation: spin 4s linear infinite;
        animation: spin 1s linear infinite;
    }
    /* set moz webkit rotate image */
    @-moz-keyframes spin {
        100% {
            -moz-transform: rotate(360deg);
        }
    }
    @-webkit-keyframes spin {
        100% {
            -webkit-transform: rotate(360deg);
        }
    }
    @keyframes spin {
        100% {
            -webkit-transform: rotate(360deg);
            transform: rotate(360deg);
        }
    }
</style>