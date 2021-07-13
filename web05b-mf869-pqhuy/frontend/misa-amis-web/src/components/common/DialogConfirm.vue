<template >
    <div class="dialog-confirm" v-if="isShow">
        <div class="confirm-form">
            <!-- title dialog confirm -->
            <div class="form-title">
                <div class="box-icon">
                    <div :class="[`${formIcon}`, 'mi-icon mi-icon-48']"></div>
                </div>
                <div class="title">
                    <span class="msg-title">{{titlePopup}}</span>
                </div>
            </div>
            <!-- button action dialog -->
            <div class="form-line"></div>
            <div :class="[`${configClass}`, 'form-button']">
                <div class="form-left" v-show="isBtnLeft">
                    <button class="m-btn-second" @click="closeForm"><span>{{titleLeft}}</span></button>
                </div>
                <div class="form-right">
                    <button class="m-btn-second mr-rem-10" v-show="isBtnRightFirst" @click="closeAll"><span>{{tleRightFirst}}</span></button>
                    <button class="m-btn" v-show="isBtnRightSecound" @click="save(statusForm)"><span>{{tleRightSecound}}</span></button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Enumeration from '../../js/common/Enumeration'

export default {
    props: {
        formIcon: {
            type: String,
            default: 'icon-notification',
        },
        titlePopup: String,
        titleLeft: String,
        tleRightFirst: String,
        tleRightSecound: String,
        isBtnLeft: Boolean,
        isBtnRightFirst: Boolean,
        isBtnRightSecound: Boolean,
        configClass: String,
        statusForm: Number,
    },
    data() {
        return{
            isShow: false
        }
    },
    methods: {
        /**
         * Function dialog form
         * CreatedBy: PQ Huy (07.07.2021)
         */
        openForm(){
            this.isShow = true;
        },
        /**
         * Function close form
         * CreatedBy: PQ Huy (07.07.2021)
         */
        closeForm(){
            this.isShow = false;
        },
        /**
         * Function set action form
         * @param {string} status
         * CreatedBy: PQ Huy (07.07.2021)
         */
        save(status){
            switch (status) {
                case Enumeration.PopupMode.Error:
                    this.closeForm();
                    break;
                case Enumeration.PopupMode.WarningValidate:
                    this.closeForm();
                    break;
                case Enumeration.PopupMode.WarningDelete:
                    this.delete();
                    break;
                case Enumeration.PopupMode.Info:
                    this.confirmInfo();
                    break;
            }
        },
        /**
         * Function confirm delete function
         * CreatedBy: PQ Huy (07.07.2021)
         */
        delete(){
            this.$emit('confirmDelete');
        },
        /**
         * Function close all form
         * CreatedBy: PQ Huy (07.07.2021)
         */
        closeAll(){
            this.$emit('closeAll');
            this.isShow = false;
        },
        /**
         * Function confirm save data
         * CreatedBy: PQ Huy (10.07.2021)
         */
        confirmInfo(){
            let saveMode = 'save';
            this.$emit('save', saveMode);
        }
    }
}
</script>
<style scoped>
    /* css for dialog box */
    .dialog-confirm{
        display: flex;
        justify-content: center;
        align-items: center;
        width: 100%;
        background: rgba(0,0,0,.4);
        position: absolute;
        left: 0;
        top: 0;
        height: 100%;
        z-index: 10;
        transition: all .25s ease;
        opacity: 1;
    }

    .dialog-confirm .confirm-form{
        padding: 32px;
        background-color: #fff;
        border-radius: 3px;
    }

    .dialog-confirm .confirm-form .form-title{
        width: 380px;
        height: 62px;
        display: flex;
        justify-content: end;
        align-items: center;
        padding-bottom: 16px;
    }

    .form-title .box-icon{
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .form-title .title{
        padding-left: 16px;
    }

    .form-line {
        height: 1px;
        background: #b8bcc3;
        margin-bottom: 20px;
    }

    /* icon dialog notification */
    .icon-notification{
        background-position: -591px -458px !important;
    }
    .icon-error{
        background-position: -24px -954px !important;
    }
    .icon-info{
        background-position: -666px -456px !important;
    }
    /* footer dialog notification */
    .footer-right{
        display: flex;
    }

    .form-button{
        display: flex;
        justify-content: space-between;
    }

    .jtf-ct{
        justify-content: center;
    }

    .jtf-fd{
        justify-content: flex-end;
    }
</style>