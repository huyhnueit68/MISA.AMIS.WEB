<template>
    <div class="box-action-row" @blur="closeDrop" tabindex="0">
        <button class="btn-edit" @click="editItem"> <span>Sửa</span> </button>
        <div class="button-arrow flex justify-center">
            <div class="icon-arrow-up mi-icon mi-icon-16"
            :class="{'arrow-up-border': isActive == true}"
            @click="openDrop"
            ></div>
        </div>
        <div class="box-function" v-show="isShow">
            <ul>
                <li @click="copyRecord">Nhân bản</li>
                <li @click="deleteRecord">Xoá</li>
                <li>Ngưng sử dụng</li>
            </ul>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
            isShow: false,
            isActive: false
        }
    },
    methods: {
        /**
         * Function opent drop down 
         * CreatedBy: PQ Huy (08.07.2021)
         */
        openDrop(){
            this.isShow = !this.isShow;
            this.$emit("setIndexImportant");
            this.isActive = !this.isActive;
        },
        /**
         * Function close drop down
         * CreatedBy: PQ Huy (06.05.2021)
         */
        closeDrop(){
            this.$emit("setIndexImportant");
            this.isShow = false;
            this.isActive = false;
        },
        /**
         * Function edit item
         * CreatedBy: PQ Huy (08.07.2021)
         */
        editItem(){
            this.$emit("editItem");
        },
        /**
         * Function copy record
         * CreatedBy: PQ Huy (08.07.2021)
         */
        copyRecord(){
            this.$emit("copyRecord");
        },
        /**
         * Function delete recore
         * CreatedBy: PQ Huy (08.07.2021)
         */
        deleteRecord(){
            this.$emit("deleteRecord");
        },
        /**
         * Function set active border
         * CreatedBy: PQ Huy (08.07.2021)
         */
        setActiveBorder(){
            this.isActive = true;    
        }
    }
}
</script>
<style scoped>
    /* main component function */
    .btn-edit {
        padding-right: 10px;
        padding: 5px 0px 5px 5px;
        font-family: "NotoSans-bold";
    }

    .btn-edit span {
        border: 1px solid #fff0;
    }

    .btn-edit span:active {
        border: 1px solid #0075c0;
    }

    .box-action-row {
        color: #0075c0;
        display: flex;
        justify-content: center;
        height: 36px;
    }

    .button-arrow.flex.justify-center {
        padding-top: 10px;
    }

    .icon-arrow-up {
        background-position: -891px -359px !important;
        border: 1px solid #0075c000;
        padding: 8px 15px 8px 12px !important;

    }

    .arrow-up-border{
        border: 1px solid #0075c0;
    }

    .active-arrow-border:active{
         border: 1px solid #0075c0;
    }

    .bold-text {
        font-weight: bold;
    }

    /* box function edit */
    .box-function{
        position: absolute;
        background: white;
        border: 1px solid #babec5 ;
        z-index: 50;
        top: 77%;
        right: 34%;
        outline: none;
        cursor: pointer;
    }
    /* custom ul li for drop list */
    ul {
        width: 125px;
        height: 90px;
        text-align: left;
        padding: 2px 1px 2px 1px;
        cursor: pointer;
    }

    ul li {
        list-style: none;
        height: 28px;
        padding: 5px 10px;
        color: #111;
        cursor: pointer;
    }

    ul li:hover {
        background: #e8e9ec;
        color: #08bf1e;
        cursor: pointer;
    }
</style>