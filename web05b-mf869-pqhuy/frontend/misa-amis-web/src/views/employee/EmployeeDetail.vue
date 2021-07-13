<template>
    <div class="dialog" id="dlgEmployee" v-if="isShowDialog">
        <div class="dialog-content">
            <!-- header content -->
            <div class="dialog-header">
                <div class="dialog-title flex items-center">
                    {{this.titleFormClone}}
                    <div class="ms-checkbox flex item-center">
                        <input type="checkbox" name="state" id="" />
                        <span class="tooltip-content mt-2">Là khách hàng</span>
                    </div>
                    <div class="ms-checkbox flex item-center mt-4">
                        <input type="checkbox" name="state"/>
                        <span class="tooltip-content">Là nhà cung cấp</span>
                    </div>
                </div>
                <!-- Button help and close -->
                <div class="btn-header flex">
                    <div class="dialog-help-button mi-icon mi-icon-24"
                        v-tooltip.bottom="optionTooltip('Trợ giúp')"
                        data-toggle="tooltip tool-box"
                        title="Trợ giúp"></div>
                    <div class="dialog-close-button mi-icon mi-icon-24"
                         title="Đóng(Esc)"
                         v-tooltip.bottom="optionTooltip('Đóng')"
                         data-toggle="tooltip tool-box"
                         @click="closeFormDetail"
                    ></div>
                </div>
            </div>
            <!-- body content -->
            <div class="body-content" ref="contentForm">
                <div class="content-first" >
                    <!-- left first content -->
                    <div class="first-left">
                        <!-- row 1-->
                        <div class="row-field">
                            <!-- field 1 -->
                            <BaseInput :isShow='true'
                            class="custom-base-input-1"  
                            :bindingData="itemData.employeeCode"
                            ref="refEmployeeCode"
                            tabindex="1"
                            FieldName="employeeCode"
                            @updateValue="updateValue"
                            @resValidInput="resValidInput"
                            Required="true"
                            isDuplicate="true"
                            :BindingInput="BindingInput.employeeCode"
                            />
                            <!-- field 2 -->
                            <BaseInput :isShow='true' class="custom-base-input-2"
                            ref="employeeName"
                            Required="true"
                            @resValidInput="resValidInput"
                            @updateValue="updateValue"
                            tabindex="2"
                            FieldName="employeeName"
                            :BindingInput="BindingInput.employeeName"
                            :bindingData="itemData.employeeName"/>
                        </div>
                        <!-- row 2-->
                        <div class="row-field full-row">
                            <ComboboxInput :title="'Đơn vị'" :source="'DepartmentsApi'"
                            :bindingData="itemData.departmentId" 
                            ref="cbo_Position"
                            FieldName="departmentId"
                            @updateValue="updateValue"
                            @resValidInput="resValidInput"
                            tabindex="3"
                            RequireCbo="true"
                            :property="'departmentId'"
                            :isShowRequire='true'
                            :showClear='true' 
                            class="custom-cbo" />
                        </div>
                        <!-- row 3-->
                        <div class="row-field full-row">
                            <!-- field 1 -->
                            <BaseInput :isShow='false'
                            @updateValue="updateValue"
                            tabindex="4"
                            :BindingInput="BindingInput.employeePosition"
                            FieldName="employeePosition"
                            :bindingData="itemData.employeePosition" class="" />
                        </div>
                    </div>
                    <!-- right first content -->
                    <div class="first-right">
                        <!-- row 1-->
                        <div class="row-field">
                            <!-- component datetime picker -->
                            <DatetimePicker :title="'Ngày sinh'" :isShow='false'
                            :bindingData="itemData.dateOfBirth"
                            FieldName="dateOfBirth"
                            @updateValue="updateValue"
                            :property="'dateOfBirth'"
                            ref="dateOfBirth"
                            tabindex="5"
                            class="custom-base-input-1"/>
                            <!-- component gender -->
                            <div class="box-input input-gender">
                                <label class="lable-input custom-lable">Giới tính</label>
                                <div class="box-gender" tabindex="6">
                                    <input type="radio" name="gender" value="1"
                                        v-model="itemData.gender"  class="mg-right-10"><span class="pg-right-20">Nam</span>
                                    <input type="radio" name="gender" value="0" 
                                        v-model="itemData.gender" class="mg-right-10"><span class="pg-right-20">Nữ</span>
                                    <input type="radio" name="gender" value="2" 
                                        v-model="itemData.gender" class="mg-right-10"><span class="pg-right-20">Khác</span>
                                </div>
                            </div>
                        </div>
                        <!-- row 2-->
                        <div class="row-field">
                            <!-- field 1 -->
                            <BaseInput
                            ref="identityNumber"
                            @updateValue="updateValue"
                            @resValidInput="resValidInput"
                            tabindex="7"
                            FieldName="identityNumber"
                            :BindingInput="BindingInput.identityNumber"
                            :bindingData="itemData.identityNumber" class="custom-inpit-width" 
                            />
                            <!-- field 2 -->
                            <DatetimePicker :title="'Ngày cấp'" :isShow='false'
                            :bindingData="itemData.identityDate"
                            @updateValue="updateValue"
                            FieldName="identityDate"
                            :property="'identityDate'"
                            tabindex="8"
                            ref="identityDate"
                            v-model="itemData.identityDate" class="custom-base-input-1 input-right"/>
                        </div>
                        <!-- row 3-->
                        <div class="row-field full-row">
                            <!-- field 1 -->
                            <BaseInput :isShow='false'
                            @updateValue="updateValue"
                            tabindex="9"
                            FieldName="identityPlace"
                            :BindingInput="BindingInput.identityPlace"
                            :bindingData="itemData.identityPlace" class="" />
                        </div>
                    </div>
                </div>
                <div class="content-second">
                    <!-- row 1 -->
                    <div class="second-row">
                        <BaseInput :isShow='false'
                        tabindex="10"
                        @updateValue="updateValue"
                        :BindingInput="BindingInput.address"
                        FieldName="address"
                        :bindingData="itemData.address" class="custom-input-full" />
                    </div>
                    <!-- row 2 -->
                    <div class="second-row-custom">
                        <!-- field 1 -->
                        <BaseInput :isShow='false'
                        @updateValue="updateValue"
                        tabindex="11"
                        FieldName="phoneNumber"
                        :BindingInput="BindingInput.phoneNumber"
                        :bindingData="itemData.phoneNumber" class="" />
                        <!-- field 2 -->
                        <BaseInput :isShow='false'
                        @updateValue="updateValue"
                        tabindex="12"
                        FieldName="telephoneNumber"
                        :BindingInput="BindingInput.telephoneNumber"
                        :bindingData="itemData.telephoneNumber" class="" />
                        <!-- field 3 -->
                        <BaseInput :isShow='false'
                        @updateValue="updateValue"
                        tabindex="13"
                        :Duplicate="true"
                        isDuplicate="true"
                        Email="true"
                        FieldName="email"
                        :BindingInput="BindingInput.email"
                        :bindingData="itemData.email" class="" />
                    </div>
                    <!-- row 3 -->
                    <div class="second-row-custom">
                        <!-- field 1 -->
                        <BaseInput :isShow='false'
                        @updateValue="updateValue"
                        :BindingInput="BindingInput.bankAccountNumber"
                        tabindex="14"
                        FieldName="bankAccountNumber"
                        :bindingData="itemData.bankAccountNumber" class="" />
                        <!-- field 2 -->
                        <BaseInput :isShow='false'
                        @updateValue="updateValue"
                        :BindingInput="BindingInput.bankName"
                        tabindex="15"
                        FieldName="bankName"
                        :bindingData="itemData.bankName" class="" />
                        <!-- field 3 -->
                        <BaseInput :isShow='false'
                        @updateValue="updateValue"
                        :BindingInput="BindingInput.bankBranchName"
                        tabindex="16"
                        FieldName="bankBranchName"
                        :bindingData="itemData.bankBranchName" class="" />
                    </div>
                </div>
            </div>
            <!-- footer dialog -->
            <div class="dialog-footer">
                <div class="divide-content"></div>
                <div class="footer-button">
                    <div class="footer-left" tabindex="17" >
                        <button class="m-btn-second" @click="closeFormDetail" v-tooltip.top="tooltipBtn('Hủy')">Huỷ</button>
                    </div>
                    <div class="footer-right">
                        
                    </div>
                    <div class="m-btn-first" tabindex="18">
                        <button 
                            class="m-btn-second mr-rem-10" 
                            @click="save('save')"
                            v-tooltip.top="tooltipBtn('Cất')"
                            data-toggle="tooltip tool-box"
                        >
                            Cất
                        </button>
                    </div>
                    <div class="m-btn-secound" tabindex="19">
                        <button 
                            class="m-btn btn-save"
                            @click="save('saveContinue')"
                            v-tooltip.top="tooltipBtn('Cất và thêm')"
                            data-toggle="tooltip tool-box"
                        >
                            Cất và thêm
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Popup warning -->
        <DialogConfirm
            :formIcon="formIconPopup" 
            ref="dialogConfirm"
            :titlePopup="titlePopup" 
            :isBtnLeft="isBtnLeft"
            :isBtnRightFirst="isBtnRightFirst"
            :isBtnRightSecound="isBtnRightSecound"
            :titleLeft="titleLeft"
            :tleRightFirst="tleRightFirst"
            :tleRightSecound="tleRightSecound"
            :configClass="configClass"
            :statusForm="statusForm"
            @closeAll="closeAll"
            @save='save'
        />
        <LoadingIcon/>
    </div>
</template>
<script>
// init common components
import BaseInput from "../../components/common/input/BaseInput"
import ComboboxInput from "../../components/common/input/ComboboxInput.vue"
import DatetimePicker from "../../components/common/input/DateInput.vue"
import DialogConfirm from "../../components/common/DialogConfirm.vue"
import LoadingIcon from "../../components/common/LoadingIcon.vue"
// init js enum, resource
import Enumeration from '../../js/common/Enumeration'
import EmployeesApi from "../../helper/api/page/EmployeesApi"
import Resource from "../../js/common/Resource"
// other library 
import moment from "moment"
const Swal = require('sweetalert2')


export default {
    created() {
        this.InitInputBase();
    },
    props: {
        titleForm: String
    },
    data() {
        return {
            dateOfBirth: null,
            isShowDialog: false,
            itemData: {},
            titlePopup: "Notification",
            formIconPopup: "icon-notification",
            isBtnLeft: true,
            isBtnRightFirst: true,
            isBtnRightSecound: true,
            titleLeft: "Hủy",
            tleRightFirst: "Không",
            tleRightSecound: "Có",
            configClass: "",
            statusForm: Enumeration.PopupMode.Warning,
            isShow: false,
            titleFormClone: "",
            cloneCode: "",
            BindingInput: {},
            cloneDataOrigin: {}
        }
        
    },
    components: {
        BaseInput,
        ComboboxInput,
        DatetimePicker,
        DialogConfirm,
        LoadingIcon
    },
    methods: {
        /**
         * Function init base input
         * CreatedBy: PQ Huy (10.07.2021)
         */
        InitInputBase(){
            this.BindingInput = {
                employeeCode: {
                    inputFieldName: Resource.FormatLable.EmployeeCode,
                    label: Resource.LableInput.EmployeeCode,
                    Required: true,
                    Duplicate: true,
                },
                employeeName: {
                    inputFieldName: Resource.FormatLable.EmployeeName,
                    label: Resource.LableInput.EmployeeName,
                    Required: true,
                    inputType: "text",
                },
                dateOfBirth: {
                    inputFieldName: Resource.FormatLable.DateOfBirth,
                    label: Resource.LableInput.DateOfBirth,
                    inputType: "date",
                    dataType: Resource.DataTypeColumn.Date,
                },
                employeePosition: {
                    inputFieldName: Resource.FormatLable.EmployeePosition,
                    label: Resource.LableInput.EmployeePosition,
                    inputType: "text",
                },
                identityNumber: {
                    inputFieldName: Resource.FormatLable.IdentityNumber,
                    label: Resource.LableInput.IdentityNumber,
                    inputType: "text",
                    Number: true
                },
                identityDate: {
                    inputFieldName: Resource.FormatLable.IdentityDate,
                    label: Resource.LableInput.IdentityDate,
                    inputType: "date",
                    dataType: Resource.DataTypeColumn.Date,
                },
                identityPlace: {
                    inputFieldName: Resource.FormatLable.IdentityPlace,
                    label: Resource.LableInput.IdentityPlace,
                    inputType: "text",
                },
                address: {
                    inputFieldName: Resource.FormatLable.Address,
                    label: Resource.LableInput.Address,
                    inputType: "text",
                },
                email: {
                    inputFieldName: Resource.FormatLable.Email,
                    label: Resource.LableInput.Email,
                    inputType: "text",
                    dataType: "Email",
                    Duplicate: true,
                },
                phoneNumber: {
                    inputFieldName: Resource.FormatLable.PhoneNumber,
                    label: Resource.LableInput.PhoneNumber,
                    inputType: "text",
                    Number: true
                },
                telephoneNumber: {
                    inputFieldName: Resource.FormatLable.TelephoneNumber,
                    label: Resource.LableInput.TelephoneNumber,
                    inputType: "text",
                    Number: true
                },
                bankAccountNumber: {
                    inputFieldName: Resource.FormatLable.BankAccountNumber,
                    label: Resource.LableInput.BankAccountNumber,
                    inputType: "text",
                    Number: true
                },
                bankName: {
                    inputFieldName: Resource.FormatLable.BankName,
                    label: Resource.LableInput.BankName,
                    inputType: "text",
                },
                bankBranchName: {
                    inputFieldName: Resource.FormatLable.BankBranchName,
                    label: Resource.LableInput.BankBranchName,
                    inputType: "text",
                },
                department: {
                    inputFieldName: Resource.FormatLable.DepartmentId,
                    label: Resource.LableInput.DepartmentId,
                    Required: true,
                },
            }
        },
        /**
         * function open form detail
         * @param {Object} item
         * @param {String} titleForm
         * CreatedBy: PQ Huy (05.07.2021)
         */
        async OpenDialog(item = null, titleForm){
            // check state and set item data
            if(item != null && this.$store.state.formMode == Enumeration.FormMode.Edit){
                this.itemData = item;
                this.cloneCode = item.employeeCode;
            }

            // check state is add
            if(this.$store.state.formMode == Enumeration.FormMode.Add) {

                // call api get new code
                await EmployeesApi.getNewEmployeeCode().then((response) => {
                    this.cloneCode = response.data;

                    //rest form
                    this.RefreshForm();
                }).catch((error) => {
                    console.error(error);
                });

                //restart item data
                this.itemData = {};

                // assign code
                if(this.cloneCode){
                    this.itemData.employeeCode = this.cloneCode;
                }
            }
            
            //Check state is copy
            if(this.$store.state.formMode == Enumeration.FormMode.Copy){
                this.itemData = item;
                this.cloneCode = item.employeeCode;
            }

            // clone origin data
            this.cloneData = JSON.parse(JSON.stringify(this.itemData));

            // set title form dialog
            this.titleFormClone = titleForm;
            this.isShowDialog = true;
        },
        /**
         * function close form detail
         * CreatedBy: PQ Huy (05.07.2021)
         */
        closeFormDetail(){
            // Compare object change
            let isCompare = true;

            // Compare object
            isCompare = this.compareObject(this.cloneData, this.itemData);

            if(!isCompare) {
                // Opent dialog warining change data
                let title = Resource.TitleForm.Notification;

                // Open dialog confirm
                this.openFormInfo(title);
            } else {
                this.isShowDialog = false;
            }
        },
        /**
         * Function access close form from dialog warning
         * CreatedBy: PQ Huy (10.07.2021)
         */
        closeAll(){
            this.isShowDialog = false;
        },
        /**
         * Function compare 2 object changes
         * @param {Object} obj1
         * @param {Object} obj1
         * @returns a true/false
         * CreatedBy: PQ Huy (10.07.2021)
         */
        compareObject(obj1, obj2){
            let isCompare = true;

            // compare 2 object by json
            if(JSON.stringify(obj1) === JSON.stringify(obj2)) {
                isCompare = true;
            } else {
                isCompare = false;
            }
            
            // return result
            return isCompare;
        },
        /**
         * Set change value data from component
         * @param {string} key
         * @param {string} value
         * CreatedBy: PQ Huy (06.07.2021)
         */
        updateValue(key, value) {
            if(value != null) {
                // check is datetime
                if(key == Resource.FormatLable.DateOfBirth || key == Resource.FormatLable.IdentityDate){
                    if(value != null) {
                        value = value.getFullYear() + "-" + value.getMonth() + "-" + value.getDate();
                    }
                } else {
                    // assign key value
                    this.$set(this.itemData, key, value);
                }
            }
        },
        /**
         * Function reset form
         * CreatedBy: PQ Huy (07.07.2021)
         */
        RefreshForm(){
            try {
                this.$refs.dateOfBirth.formatDate();
                this.$refs.identityDate.formatDate();
            } catch (e) {
                console.log(e);
            }
        },
        /**
         * Function save data
         * CreatedBy : PQ Huy (06.07.2021)
         */
        async save(saveMode){
            // get all data input for validate
            var isValid = true,
                resultSave = true;

            // validate inside components input
            let me = this,
                inputValid = me.$refs.contentForm.querySelectorAll("[Required]");
                me.isValid = true;
            
            // call function inside component 
            await inputValid.forEach((input) => {
                input.querySelector(".refInput").focus();
                input.querySelector(".refInput").blur();
            });

            // validate data
            isValid = await this.validateData();
            // isValid = true;

            // check validate result
            if(isValid) {
                // fotmat data before save
                this.formatDataSave();
        
                // check state is copy
                if(this.$store.state.formMode == Enumeration.FormMode.Add || this.$store.state.formMode == Enumeration.FormMode.Copy) {
                    // api save
                    resultSave = await this.Insert();

                    // check state result
                    if(resultSave == true){
                        this.popupNotification('success', Resource.TitleForm.AddSuccess);
                        this.closeAction(saveMode);
                    }
                } else {
                    // api edit
                    resultSave = await this.Edit();

                    // check state result
                    if(resultSave == true){
                        this.popupNotification('success', Resource.TitleForm.EditSuccess);
                        this.closeAction(saveMode);
                    }
                }
            }
        },
        /**
         * function close form detail
         * @param {string} saveMode
         * CreatedBy: PQ Huy (07.07.2021)
         */
        closeAction(saveMode){
            let me = this;

            // reload data
            this.$emit('reloadingData');

            // set close with save model
            if(saveMode == 'save'){
                this.isShowDialog = false;
            } else {                
                // Opent loading icon
                this.$store.state.isLoading = true;

                //Bing new code and reset data
                EmployeesApi.getNewEmployeeCode().then((response) => {
                    
                    //Rest data form
                    this.itemData = {};
                    me.itemData.employeeCode = response.data;
                    
                    // Reset clone data
                    this.cloneData = this.itemData;
                }).catch((error) => {
                    console.error(error);

                    // Reset clone data
                    this.cloneData = this.itemData;
                });

                // Set time out for loading icon
                setTimeout(() => { clearInterval(this.$store.state.isLoading = false)}, 1000);
                
            }
        },
        /**
         * Function inset data
         * CreatedBy: PQ Huy (07.07.2021)
         */
        async Insert(){
            // Call api get data
            // format data before save
            var data = this.itemData,
                isResult = true,
                temp = {};

            // Call api instet data
            await EmployeesApi.insert(data).then((response) => {
                temp = response.data;
                if(response.status == 200 || response.status == 201 || response.status == 204){
                    if(temp)
                        isResult = true;
                } else {
                    isResult = false;
                }
            }).catch((error) => {
                // open dialog form notification
                this.openFormRequire(Resource.TitleForm.AddErrPopup);

                // console log error
                console.error(error);
                isResult = false;
            });

            // Return result
            return isResult;
        },

        /**
         * Function edit data
         * CreatedBy: PQ Huy (07.07.2021)
         */
        async Edit(){
            let data = this.itemData,
            resultEdit = true,
            rowAffect = {};

            // Call api edit data
            await EmployeesApi.update(data.employeeId, data).then((response) => {
                rowAffect = response.data;
                
                if(response.status == 200 || response.status == 201 || response.status == 204){
                    if(rowAffect)
                        resultEdit = true;
                } else {
                    resultEdit = false;
                }
            }).catch((error) => {
                // open dialog form notification
                this.openFormRequire(Resource.TitleForm.EditErrPopup);

                // console log error
                console.error(error);
                resultEdit = false;
            });

            // Return result
            return resultEdit;
        },
        /**
         * Function format data before save
         * CreatedBy: PQ Huy (07.07.2021)
         */
        formatDataSave(){
            // Format date
            let dateOfBirth = this.$refs.dateOfBirth.dataReceive;
            let identityDate = this.$refs.identityDate.dataReceive;
            
            // Update date time before save
            if(dateOfBirth && dateOfBirth != Resource.StateDate.InValid){
                this.itemData.dateOfBirth = moment(String(dateOfBirth)).format("YYYY-MM-DD");
            }
            if(identityDate && identityDate != Resource.StateDate.InValid){
                this.itemData.identityDate = moment(String(identityDate)).format("YYYY-MM-DD");
            }
        },
        /**
         * Function validate data
         * CreatedBy: PQ Huy (06.07.2021)
         */
        async validateData(){
            // get all componet have property required
            var inputValidateRequire = this.$refs.contentForm.querySelectorAll("[Required]"),
                isValid = true;

            // validate require
            for (let i = 0; i < inputValidateRequire.length; i++) {
                isValid =  this.vaidateRequire(inputValidateRequire[i]);
                if(!isValid)
                    return isValid;
            }

            // Validate format number
            let inputNumber = this.$refs.contentForm.querySelectorAll("[isNumber]");
            for (let i = 0; i < inputNumber.length; i++) {
                isValid =  this.validateFormatNumber(inputNumber[i]);
                if(!isValid)
                    return isValid;
            }

            // Validate format email
            let inputEmail = this.$refs.contentForm.querySelectorAll("[Email]");
            for (let i = 0; i < inputEmail.length; i++) {
                isValid =  this.validateFormat(inputEmail[i]);
                if(!isValid)
                    return isValid;
            }

            // require for combobox
            inputValidateRequire = this.$refs.contentForm.querySelectorAll("[RequireCbo]");
            for (let i = 0; i < inputValidateRequire.length; i++) {
                isValid =  this.vaidateRequire(inputValidateRequire[i]);
                if(!isValid){
                    this.$refs.cbo_Position.onFocusOut();
                    return isValid;
                }
            }

            //validate duplicate
            var inputDuplicate = this.$refs.contentForm.querySelectorAll("[isDuplicate]");
            for (let i = 0; i < inputDuplicate.length; i++) {
                isValid =  await this.vaidateDuplicate(inputDuplicate[i]);
                if(!isValid)
                    return isValid;
            }

            // Return value
            return isValid;
        },
        /**
         * Function validate format email
         * @param {Object} inputValid
         * CreatedBy: PQ Huy (09.07.2021)
         */
        validateFormat(inputValid){
            /* eslint-disable */
            const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; // eslint-disable-next-line
            
            let isValid = true,
                title = inputValid.querySelector('.lable-input').querySelector('.lable').textContent,
                inputValue = inputValid.querySelector('.m-input').value;
            
            if(inputValue) {
                // Test with regex
                isValid = re.test(inputValue);
                
                // Opent form warning
                if(!isValid)
                    this.openFormWarning(`${title} <${inputValue}> ${Resource.TitleForm.FormatError}`);
            }

            // Return value
            return isValid; 
        },
        /**
         * Function validate format number
         * @param {Object} inputValid
         * CreatedBy: PQ Huy (11.07.2021)
         */
        validateFormatNumber(inputValid){
            try{
                let isValid = true,
                title = inputValid.querySelector('.lable-input').querySelector('.lable').textContent,
                inputValue = inputValid.querySelector('.m-input').value;

                const regex = /^\d+$/;

                if(inputValue) {
                    // Test with regex
                    isValid = regex.test(inputValue.trim());

                    // Opent form warning
                    if(!isValid)
                        this.openFormWarning(`${title} <${inputValue}> ${Resource.TitleForm.FormatError}`);
                }
                
                // Return value
                return isValid;
            } catch(error) {
                throw error;
            }
        },
        /**
         * Function validate duplicate code
         * @param {object} inputValidate
         * CreatedBy: PQ Huy (07.07.2021)
         */
        async vaidateDuplicate(inputValidate){
            try{
                let isValid = true,
                me = this,
                title = inputValidate.querySelector('.lable-input').querySelector('.lable').textContent,
                valueInput = inputValidate.querySelector('.m-input'),
                fieldName = inputValidate.getAttribute("fieldName");

                // check if select box
                if(valueInput == null){
                    valueInput = inputValidate.querySelector('.dx-texteditor-input').value;
                } else {
                    valueInput = valueInput.value;
                }

                // validate employee code
                if(fieldName.toLowerCase() == Resource.DuplicateType.EmployeeCode.toLowerCase()) {
                    let checkState = "";
                    // Check state form mode
                    if(me.$store.state.formMode == Enumeration.FormMode.Edit) {
                        // Query by code
                        await EmployeesApi.getById(me.itemData.employeeId).then((response) => {
                            checkState = response.data[0];
                        }).catch((error) => {
                            // open dialog form notification
                            console.error(error);
                        })
                    }

                    // Validate when add new
                    if(me.$store.state.formMode == Enumeration.FormMode.Add || me.$store.state.formMode == Enumeration.FormMode.Copy) {
                        // Query by code
                        await EmployeesApi.getCheckEmployeeCodeExist(valueInput).then((response) => {
                            isValid = response.data;
                        }).catch((error) => {
                            // open dialog form notification
                            console.error(error);
                        })
                    }

                    // Check value duplicate with employee code
                    if(checkState != ""){
                        if(checkState.employeeCode.toLowerCase() != valueInput.toLowerCase()) {
                            // Query by code
                            await EmployeesApi.getCheckEmployeeCodeExist(valueInput).then((response) => {
                                isValid = response.data;
                            }).catch((error) => {
                                // open dialog form notification
                                console.error(error);
                            })
                        } else {
                            isValid = false;
                        }
                    }
                    if(isValid) {
                        //show dialog error
                        title = `Mã nhân viên <${valueInput}> ${Resource.TitleForm.Duplicate}`;

                        //open dialog
                        this.openFormWarning(title);
                        return false;
                    } else {
                        return true;
                    }
                }

                // validate employee email
                if(fieldName.toLowerCase() == Resource.DuplicateType.Email.toLowerCase()) {
                    let checkedEmail = {};

                    // check state edit
                    if(me.$store.state.formMode == Enumeration.FormMode.Edit) {
                        if(valueInput) {
                            // get origin Email
                            let originEmail = {};

                            await EmployeesApi.getById(this.itemData.employeeId).then((response) => {
                                originEmail = response.data[0].email;
                            }).catch((error) => {
                                // open dialog form notification
                                console.error(error);
                                return false;
                                
                            });

                            // compare with current Email
                            if(originEmail != valueInput) {
                                // Validate duplicate if compare fail
                                await EmployeesApi.getEmployeeByEmail(valueInput).then((response) => {
                                    checkedEmail = response.data;
                                }).catch((error) => {
                                    // open dialog form notification
                                    let title = Resource.TitleForm .ErrorMISA;
                                    this.openFormWarning(title);
                                    console.error(error);
                                    
                                });
                            } else {
                                return true;
                            }
                        } else {
                            return true;
                        }
                    }

                    // check state add new and copy
                    if(me.$store.state.formMode == Enumeration.FormMode.Add || me.$store.state.formMode == Enumeration.FormMode.Copy) {
                        await EmployeesApi.getEmployeeByEmail(valueInput).then((response) => {
                            checkedEmail = response.data;
                        }).catch((error) => {
                            // open dialog form notification
                            console.error(error);
                        });

                        if(checkedEmail.length > 0) {
                            //show dialog error
                            title = `Email <${valueInput}> ${Resource.TitleForm.Duplicate}`;

                            this.openFormWarning(title);

                            //return value
                            return false;
                        } else {
                            return true;
                        }
                    }

                    // return value
                    return isValid;
                }
            } catch(error) {
                throw error;
            }
        },
        /**
         * Function validate required
         * @param {object} inputValidate
         * @return a boolen check validate
         * CreatedBy: PQ Huy (07.07.2021)
         */
        vaidateRequire(inputValidate){
            try{
                let title = inputValidate.querySelector('.lable-input').querySelector('.lable').textContent,
                valueInput = inputValidate.querySelector('.m-input'),
                isValid = true;

                if(valueInput == null){
                    valueInput = inputValidate.querySelector('.dx-texteditor-input').value;
                } else {
                    valueInput = valueInput.value;
                }

                if(valueInput == null || valueInput == ''){
                    isValid = false;

                    //config dialog detail
                    let val =`${title} ${Resource.TitleForm.NotNull}`;

                    //open form
                    this.openFormRequire(val);
                }
                
                // return value
                return isValid;
            } catch(error) {
                throw error;
            }
        },
        /**
         * Result validate input
         * @param {string} state
         * @param {string} message
         * CreatedBy: PQ Huy (07.07.2021)
         */
        resValidInput(state, message){
            try{
                if(state == Enumeration.StateValid.Required){
                    this.openFormRequire(message);
                }
            } catch(error) {
                throw error;
            }
            
        },
        /**
         * @param {string} title
         * CreatedBy: PQ Huy (07.07.2021)
         */
        openFormRequire(title){
            try{
                //config dialog detail
                this.titlePopup=`${title}`,
                this.formIconPopup="icon-error",
                this.isBtnLeft=false,
                this.isBtnRightFirst=false,
                this.isBtnRightSecound=true,
                this.titleLeft="",
                this.tleRightFirst="",
                this.tleRightSecound=Resource.TitleBtn.Close,
                this.configClass="jtf-ct"
                this.statusForm=Enumeration.PopupMode.Error

                // call popup waning
                this.$refs.dialogConfirm.openForm();
            } catch(error) {
                throw error;
            }
            
        },
        /** Function call popup info
         * @param {string} title
         * CreatedBy: PQ Huy (07.07.2021)
         */
        openFormInfo(title){
            try{
                //config dialog detail
                this.titlePopup=`${title}`,
                this.formIconPopup="icon-info",
                this.isBtnLeft=true,
                this.isBtnRightFirst=true,
                this.isBtnRightSecound=true,
                this.titleLeft=Resource.TitleBtn.Cancel,
                this.tleRightFirst=Resource.TitleBtn.No,
                this.tleRightSecound=Resource.TitleBtn.Yes,
                this.configClass=""
                this.statusForm=Enumeration.PopupMode.Info
                // call popup waning
                this.$refs.dialogConfirm.openForm();
            } catch(error) {
                throw error;
            }
            
        },
        /** Function call popup warning
         * @param {string} title
         * CreatedBy: PQ Huy (07.07.2021)
         */
        openFormWarning(title){
            try{
                //config dialog detail
                this.titlePopup=`${title}`,
                this.formIconPopup="icon-notification",
                this.isBtnLeft=false,
                this.isBtnRightFirst=false,
                this.isBtnRightSecound=true,
                this.titleLeft="",
                this.tleRightFirst="",
                this.tleRightSecound=Resource.TitleBtn.Confirm,
                this.configClass="jtf-fd"
                this.statusForm=Enumeration.PopupMode.WarningValidate

                // call popup waning
                this.$refs.dialogConfirm.openForm();
            } catch(error) {
                throw error;
            }
        },
        /**
         * Function call popup notification
         * @param {string} icon
         * @param {string} title
         * CreatedBy: PQ Huy (07.07.2021)
         */
        popupNotification(icon, title){
            const Toast = Swal.mixin({
                    toast: true,
                    position: 'bottom-end',
                    showConfirmButton: false,
                    timer: 3000,
                    customClass: "popup-success",
                    timerProgressBar: true,
                    didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })
            Toast.fire({
                icon: `${icon}`,
                title: `${title}`
            });
        },
        /**
         * Function set tooltip for icon
         * @param {string} title
         * @return {object} list object setup tooltip
         * CreatedBy: PQ Huy (10.07.2021)
         */
        optionTooltip(title){
            try{
                return {
                    content: title,
                    autoHide: false,
                    show: false,
                    classes: 'tooltip-detail'
                }
            } catch(error) {
                throw error;
            }
            
        },
        /**
         * Function set tooltip for icon
         * @param {string} title
         * @return {object} list object setup tooltip
         * CreatedBy: PQ Huy (10.07.2021)
         */
        tooltipBtn(title){
            return {
                content: title,
                autoHide: false,
                show: false,
                classes: 'tooltip-btn'
            }
        },
    }
}
</script>
<style scoped>
    @import url("../../assets/css/common/dialog.css");
</style>
