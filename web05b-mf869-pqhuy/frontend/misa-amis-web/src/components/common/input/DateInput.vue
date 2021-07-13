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
            :lang="language"
        >
        </date-picker>
    </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import "vue2-datepicker/locale/zh-cn";
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
            language:{
                formatLocale: {
            // MMMM
            months: [
                "Tháng 1",
                "Tháng 2",
                "Tháng 3",
                "Tháng 4",
                "Tháng 5",
                "Tháng 6",
                "Tháng 7",
                "Tháng 8",
                "Tháng 9",
                "Tháng 10",
                "Tháng 11",
                "Tháng 12",
            ],
            // MMM
            monthsShort: [
                "Th1",
                "Th2",
                "Th3",
                "Th4",
                "Th5",
                "Th6",
                "Th7",
                "Th8",
                "Th9",
                "Th10",
                "Th11",
                "Th12",
            ],
            // dddd
            weekdays: [
                "Chủ nhật",
                "Thứ hai",
                "Thứ ba",
                "Thứ tư",
                "Thứ năm",
                "Thứ sáu",
                "Thứ bảy",
            ],
            // ddd
            weekdaysShort: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
            // dd
            weekdaysMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
            // first day of week
            firstDayOfWeek: 1,
            // first week contains January 1st.
            firstWeekContainsDate: 1,
            },
            yearFormat: 'YYYY',
            monthBeforeYear: true,
            }
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

<style >
    .mx-input{
        border-radius: 2px;
        border: 1px solid #babec5 !important;
        box-shadow: none !important;
    }
    .mx-input:hover{
        outline: 1px solid #e2e2e2;
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