<template>
    <div class="box-input">
        <div class="lable-input">
            <span class="lable">{{BindingInput.label}}</span><span class="required" v-show="isShow"> * </span>
        </div>
        <input type="text" tabindex="1"
        ref="reInput"
        class="m-input refInput" title=""
        @keyup="capitalize"
        @focus="focus"
        @blur="blur"
        :class="{
            'error-input': isValid == false,
            'success-input': isValid == true,
        }"
        v-model="dataReceive">
    </div>
</template>

<script>

export default ({ 
    props: {
        title: String,
        isShow: Boolean,
        bindingData: {
            default: null
        },
        Require: Boolean,
        Duplicate: Boolean,
        Number: Boolean,
        BindingInput: {
            type: Object,
            require: true,
        },
    },
    data() {
        return {
            dataReceive: "",
            isValid: true,
        }
    },
    created() {
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
            this.$emit("updateValue", this.BindingInput.inputFieldName, val);
        },
        /**
         * set change input for parent
         * @param {string} val
         * CreatedBy: PQ Huy (06.07.2021)
         */
        bindingData: function (val) {
            this.dataReceive = JSON.parse(JSON.stringify(val));
        },
    },
    methods: {
        /**
         * function check length of input 
         * @param {string} targetInput
         * CreatedBy: PQ Huy (06.07.2021)
         */
        applyInputStyle(targetInput) { 
            let className = "";
            if(this.isShow) {
                if(targetInput && targetInput.length > 0){
                    className = 'success-input';
                }else{
                    className = 'error-input';
                }
            }

            return className;
        },
        /**
         * function set action when focused
         * CreatedBy: PQ Huy (07.07.2021)
         */
        focus() {
            this.isValid = true;
        },
        /**
         * Function set validate when blur
         * CreatedBy: PQ Huy (07.07.2021)
         */
        blur() {
            this.isFocus = false;
            this.validate();
        },
        /**
         * Function validate data
         * CreatedBy: PQ Huy (07.07.2021)
         */
        validate(){
            let value = this.dataReceive,
                isValid = true;

            // validate required
            isValid = this.validateRequired(value);

            // validate type input numbers
            isValid = this.validateFormatNumber(value);

            // // validate type input numbers
            // isValid = this.validateFormatEmail(value);

            return isValid;
        },
        /**
         * Function validate required
         * @param {string} value
         * CreatedBy: PQ Huy (08.07.2021)
         */
        validateRequired(value) {
            if (this.BindingInput.Required) {
                if (!value || value.length <= 0) {
                    this.isValid = false;
                    this.$refs.reInput.title = "Vui lòng điền đầy đủ thông tin";
                    return false;
                } else {
                    this.isValid = true;
                }
            }
            return true;
        },
        /**
         * Function validate required
         * @param {string} value
         * CreatedBy: PQ Huy (08.07.2021)
         */
        validateFormatNumber(value){
            if (this.BindingInput.Number) {
                if(value) {
                    if(!value.match(/^-{0,1}\d+$/)) {
                        this.isValid = false;
                        this.$refs.reInput.title = "Vui lòng nhập đúng định dạng chỉ gồm số";
                        return false;
                    } else {
                        this.isvalid = true;
                    }
                }
            }
            return true;
        },
        /**
         * Ppper case full name
         * CreatedBy : PQ Huy (09.07.2021)
         */
        capitalize() {
            if(this.BindingInput.inputFieldName == 'employeeName') {
                let words = this.dataReceive.split(' '),
                    CapitalizedWords = [];

                words.forEach(element => {
                    if(element) {
                        CapitalizedWords.push(element[0].toUpperCase() + element.slice(1, element.length));
                    } else {
                        CapitalizedWords.push("");
                    }
                });
                this.dataReceive = CapitalizedWords.join(' ');
            }
        },
    }
})
</script>


<style scoped>
    /* css for require class */
    .required{
        color: red;
    }
    .m-input{
        border: 1px solid #babec5;
        border-radius: 2px;
        height: 32px;
        padding-left: 10px;
    }
    .lable-input{
        font-size: 12px;
        font-family: "NotoSans-bold";
        padding-bottom: 4px;
    }
    /* Set css for each item */
    .error-input {
        outline: none;
        border: 2px solid #f65454;
    }
    .success-input:focus {
        outline: none;
        border: 1px solid #019160;
    }
</style>