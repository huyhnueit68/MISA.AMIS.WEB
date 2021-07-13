<template>
    <div class="box-input">
        <div class="lable-input">
            <span class="lable">{{title}}</span><span class="required" v-show="isShowRequire"> * </span>
        </div>
        <DxSelectBox
            ref="reInput"
            :search-enabled="true"
            :data-source="dataShow"
            noDataText="Không có dữ liệu hợp lệ"
            v-model="dataReceive"
            display-expr="departmentName"
            value-expr="departmentId"
            :onFocusOut="onFocusOut"
            :searchTimeout="0"
            @value-changed="changeValue"
            :placeholder="'Chọn vị trí phòng ban'"
            class="dxCombobox field-input"
            :class="{
                'err-cbo-input': isValid == false,
                'succ-cbo-input' :isValid == true
            }"
        />
    </div>
</template>
 
<script>
import DxSelectBox from 'devextreme-vue/select-box';
import DepartmentsApi from "../../../helper/api/page/DepartmentsApi"
import Resource from "../../../js/common/Resource"

export default {
    components: {
        DxSelectBox
    },
    props: {
        title: String,
        isShowRequire: Boolean,
        showClear: Boolean,
        source: String,
        // init prop for binding data
        bindingData: {
            default: null
        },
        property: String
    },
    data() {
        return {
            dataShow: {},
            dataReceive: "",
            isValid: true
        }
    },
    created() {
        //get data api befor show data
        this.getDataSource(this.source);
        // set data binding for component
        this.dataReceive = this.bindingData;
    },
    watch: {
        /**
         * set update input value for parent
         * @param {string} val
         * CreatedBy: PQ Huy (06.07.2021)
         */
        dataReceive: function (val) {
            this.$emit("updateValue", this.property, val);
        },
        /**
         * set change input for parent
         * @param {string} val
         * CreatedBy: PQ Huy (06.07.2021)
         */
        bindingData: function(val) {
            this.dataReceive = JSON.parse(JSON.stringify(val));
        }
    },
    methods: {
        /**
         * function get data source api
         * @Param {string} source
         * CreatedBy: PQ Huy (10.07.2021)
         */
        getDataSource(source) {
            switch (source) {
                case 'DepartmentsApi':
                    this.getDbDepartments();
                    break;
                case 'EmployeesApi':
                    // this.getDbEmployees();
                    break;
                default:
                    this.dataShow = {};
                    break;
            }
        },
        /**
         * Get api department
         */
        async getDbDepartments(){
            await DepartmentsApi.getAll().then((response) => {
                this.dataShow = response.data;
            }).catch((error) => {
                console.error(error);
            }); 
        },
        /**
         * function check length of input 
         * @param {string} targetInput
         * CreatedBy: PQ Huy (06.07.2021)
         */
        applyInputStyle(targetInput) { 
            if(this.isShowRequire) {
                if(targetInput && targetInput.length > 0){
                    return ['mi-success-input']
                }else{
                    return ['mi-error-input']
                }
            }
        },
        changeValue(e){
            if(e) {
                this.isValid = true;
            } else {
                this.isValid = false;
            }
        },
        onFocusOut() {
            if(!this.dataReceive) {
                this.isValid = false;
                this.$refs.reInput.title = Resource.TitleForm .FullField;
            }
            else {
                this.isValid = true;
            }
        },
    }
}
</script>

<style >
    /* css for require class */
    .required{
        color: red;
    }

    .lable-input{
        font-size: 12px;
        font-family: "NotoSans-bold";
        padding-bottom: 4px;
    }

    /* custom color for combobox */
    .dx-dropdowneditor-icon {
        border: 0px;
        width: 34px;
        border-top-left-radius: 0px;
        border-bottom-left-radius: 0px;
    }

    .err-cbo-input {
        border: 1px solid red;
    }

    .err-cbo-input .dx-texteditor-input-container{
        height: 30px !important;
    }

    .succ-cbo-input .dx-texteditor-input-container{
        height: 30px !important;
    }

    .succ-cbo-input,
    .dx-texteditor.dx-editor-outlined{
        border: 1px solid #babec5;
    }

    .dx-button-normal {
        border: 0px;
        padding: 0;
    }

    .dx-texteditor.dx-state-active.dx-editor-filled,
    .dx-texteditor.dx-state-active.dx-editor-outlined,
    .dx-texteditor.dx-state-active.dx-editor-underlined,
    .dx-texteditor.dx-state-focused.dx-editor-filled,
    .dx-texteditor.dx-state-focused.dx-editor-outlined,
    .dx-texteditor.dx-state-focused.dx-editor-underlined{
        border-color: #2ca01c;
    }
    .dx-texteditor.dx-state-hover {
        outline: 1px solid #e2e2e2;
    }

    .dx-texteditor.dx-editor-outlined{
        border-radius: 2px;
    }

    /* Set css for each item */
    .mi-error-input {
        border-color: #f65454;
    }

    .mi-success-input {
        border-color: #2ca01c;
    }

    .dxCombobox input {
        font-family: 'Notosans-regular' !important;
        font-size: 13px !important;
        color: #000 !important;
    }

    .dx-selectbox-popup-wrapper
    .dx-list:not(.dx-list-select-decorator-enabled)
    .dx-list-item.dx-state-focused {
        color: #000;
        background-color: #ebedf0;
    }
</style>