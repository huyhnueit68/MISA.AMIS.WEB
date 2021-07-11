<template >
    <div class="box-input">
        <div class="lable-input">
            {{title}}<span class="required" v-show="isShow"> * </span>
        </div>
        <!-- date time picker -->
        <date-picker
            class="custom-date-picker" 
            v-model="dataReceive"
            :formatter="momentFormat"
        >
        </date-picker>
    </div>
</template>

<script>
import 'vue2-datepicker/index.css';
import DatePicker from 'vue2-datepicker';
import moment from "moment";

export default {
    components: {
        DatePicker,
    },
    props: {
        title: String,
        isShow: Boolean,
        bindingData: {
            default: null
        },
        property: String
    },
    created() {
        // set data binding for component
        this.dataReceive = this.formatDate(this.bindingData);
    },
    data() {
        return {
            momentFormat: {
                stringify: (date) => {
                    return date ? moment(String(date)).format("DD/MM/YYYY") : ''
                },
                parse: (value) => {
                    return value ? moment(value, 'LL').toDate() : null
                },
            },
            dataReceive: "",
        }
    },
    watch: {
        dataReceive: function (val) {
            
            this.$emit("updateValue", this.property, val);
        },
        bindingData: function (val) {
            // format datetime to vietnamese
            
            val = new Date(moment(String(val)).format("YYYY-MM-DD"));
            this.dataReceive = val;
        },
    },
    methods: {
        /**
         * function format data datetime
         * @param {String} val
         * CreatedBy: PQ Huy (07.07.2021)
         */
        formatDate(val){
            
            if(val) {
                this.dataReceive = new Date(moment(String(val)).format("YYYY-MM-DD"));
                return new Date(moment(String(val)).format("YYYY-MM-DD"));
            } else {
                
                this.dataReceive = new Date(moment(String("0000-00-00")).format("YYYY-MM-DD"));
            }
        }
    }
}
</script>

<style>
    .mx-input{
        border-radius: 2px;
        border: 1px solid #babec5 !important;
        box-shadow: none !important;
    }
    .lable-input{
        font-size: 12px;
        font-family: "NotoSans-bold";
        padding-bottom: 4px;
    }
    /* custom datetime picker */
    .custom-date-picker {
        width: 100%;
        height: 32px;
        border-radius: 2px;
    }

    .custom-date-picker .mx-input-wrapper{
        width: 100% !important;
        height: 100% !important;
        border-radius: 2px !important;
    }

    .custom-date-picker .mx-input-wrapper .mx-input{
        height: 100%;
        color: #000 !important;
    }

    .mx-datepicker-popup .mx-datepicker-content .active{
        background-color: #2ca01c !important;
    }

    .custom-date-picker .mx-input-wrapper .mx-input:hover,
    .custom-date-picker .mx-input-wrapper .mx-input:focus {
        border: 1px solid #2ca01c;
    }
</style>