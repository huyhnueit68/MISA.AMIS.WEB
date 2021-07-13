<template>
    <div class="content-container">
        <!-- header content -->
        <HeaderContent @openDialogDetail = 'openDialogDetail'/>

        <!-- main content -->
        <div class="content-main">
            <!-- toolbar table -->
            <ToolbarContent @refreshData='refreshData'
            @searchFunc='getDataServer'
            :jsonData="jsonData"
            :jsonFields="jsonFields"
            :worlsheetName="worlsheetName"
            :fieldName="fieldName"
            />
            <!-- content table  -->
            <Table
                @openDialogEdit="openDialogDetail"
                @openDialogCopy="openDialogCopy"
                @deleteRecord="deleteRecord"
                @changePageIndex="getDataServer"
                ref="tableContent"
                :employeeData="employeeData"
            />
            <!-- Loading icon -->
            <LoadingIcon/>
        </div>
        <!-- Dialog detail -->
        <DialogDetail ref="dialogDetail" @reloadingData="refreshData"/>
        <!-- Popup warning -->
        <DialogConfirm
            ref="dialogConfirm"
            :formIcon="formIconPopup" 
            :titlePopup="titlePopup" 
            :isBtnLeft="isBtnLeft"
            :isBtnRightFirst="isBtnRightFirst"
            :isBtnRightSecound="isBtnRightSecound"
            :titleLeft="titleLeft"
            :tleRightFirst="tleRightFirst"
            :tleRightSecound="tleRightSecound"
            :configClass="configClass"
            :statusForm="statusForm"
            @confirmDelete="confirmDelete"
        />
    </div>
</template>

<script>
// common components
import DialogDetail from './EmployeeDetail.vue'
import LoadingIcon from '../../components/common/LoadingIcon'
import DialogConfirm from "../../components/common/DialogConfirm"
// child component
import HeaderContent from './HeaderContent'
import ToolbarContent from './ToolbarContent'
import Table from './EmployeeList.vue'
// init js file enum
import Enumeration from "../../js/common/Enumeration"
import Resource from "../../js/common/Resource"
import EmployeesApi from "../../helper/api/page/EmployeesApi"
import CommonFn from "../../js/common/CommonFn"
const Swal = require('sweetalert2')
import moment from "moment"

export default ({
    components: {
        HeaderContent,
        ToolbarContent,
        Table,
        DialogDetail,
        LoadingIcon,
        DialogConfirm 
    },
    data(){
        return {
            employeeData: {
                column: [
                    {
                        columnName: "MÃ NHÂN VIÊN",
                        fieldName: "employeeCode",
                    },
                    {
                        columnName: "TÊN NHÂN VIÊN",
                        fieldName: "employeeName",
                    },
                    {
                        columnName: "GIỚI TÍNH",
                        fieldName: "gender",
                        dataType: "Enum",
                        enumName: "Gender",
                    },
                    {
                        columnName: "NGÀY SINH",
                        fieldName: "dateOfBirth",
                        dataType: "Date",
                    },
                    {
                        columnName: "SỐ CMND",
                        fieldName: "identityNumber",
                    },
                    {
                        columnName: "CHỨC DANH",
                        fieldName: "employeePosition",
                    },
                    {
                        columnName: "TÊN ĐƠN VỊ",
                        fieldName: "departmentName",
                    },
                    {
                        columnName: "SỐ TÀI KHOẢN",
                        fieldName: "bankAccountNumber",
                    },
                    {
                        columnName: "TÊN NGÂN HÀNG",
                        fieldName: "bankName",
                    },
                    {
                        columnName: "CHI NHÁNH TK NH",
                        fieldName: "bankBranchName",
                    }
                ],
                gridData: [],
                idFieldName: "EmployeeId",
            },
            cloneId: "",
            formIconPopup: "icon-notification",
            titlePopup: "Notification",
            isBtnLeft: true,
            isBtnRightFirst: true,
            isBtnRightSecound: true,
            titleLeft: Resource.TitleBtn.Cancel,
            tleRightFirst: Resource.TitleBtn.No,
            tleRightSecound: Resource.TitleBtn.Yes,
            configClass: "",
            statusForm: Enumeration.PopupMode.Warning,
            jsonFields: {},
            worlsheetName:"",
            fieldName:"",
            jsonData: []
        }
    },
    async created() {
        await this.getDataServer();
    },
    methods: {
        /**
         * Function get all data servers
         * @param {int} val
         * @param {int} pgIndex
         * CreatedBy: PQ Huy (05.07.2021)
         */
        async getDataServer(val = null, pgIndex = null) {
            //Get current page
            let currentpage = this.$store.state.currentPage,
                pageSize = this.$store.state.pageSize,
                totalRecord = 0,
                me = this,
                dataPaging = {};

            if(this.$store.state.stateSearch == false) {
                // Get data paging
                await EmployeesApi.getEmployeePaging(currentpage, pageSize).then((response) => {
                    dataPaging = response.data;
                    totalRecord = dataPaging.total;
                    me.employeeData.gridData = dataPaging.data

                    // opent icon loading
                    this.$store.state.isLoading = true;
                }).catch((error) => {
                    if(error){
                        // opent popup error
                        this.openFormValidate(Resource.TitleForm.ErrorMISA);

                        console.log(error);
                    }
                });

                // Get data all
                await EmployeesApi.getAll().then((response) => {
                    // pass data to toolbar
                    me.setPropsToolbar(response.data);
                }).catch((error) => {
                    if(error){
                        console.log(error);
                    }
                });
            } else {
                // Rest page index
                if(pgIndex == null)
                    this.$store.state.currentPage = 1;

                // Get data paging filter
                await EmployeesApi.getEmployeeFilter(this.$store.state.currentPage, pageSize, val).then((response) => {
                    dataPaging = response.data;
                    totalRecord = dataPaging.total;
                    me.employeeData.gridData = dataPaging.data

                    // opent icon loading
                    this.$store.state.isLoading = true;
                }).catch((error) => {
                    if(error){
                        // opent popup error
                        console.log(error);
                    }
                });

                // Get all data paging filter
                await EmployeesApi.getEmployeeFilterAll(val).then((response) => {
                    me.setPropsToolbar(response.data.data);

                }).catch((error) => {
                    if(error){
                        // opent popup error
                        console.log(error);
                    }
                });
            }

            //Get total page ( totalPage = totalRecord/pageSize - rounding and handling plus one)
            this.$store.state.totalPage = this.CalculatorTotaPage(totalRecord, pageSize);
            this.$store.state.totalRecord = totalRecord;

            // Set selected item null
            me.$refs.tableContent.resetSelectedAll();

            // Set time out for loading icon
            setTimeout(() => { clearInterval(this.$store.state.isLoading = false)}, 500);
            
            //set display icon no data
            if(me.employeeData.gridData.length > 0){
                this.$refs.tableContent.setIsNoData(false);
            } else {
                this.$refs.tableContent.setIsNoData(true);
            }
        },
        /**
         * Function set props data to the toolbar
         * @param {Object} data
         * CreatedBy: PQ Huy (11.07.2021)
         */
        setPropsToolbar(data){
            let me = this;

            let clone = JSON.parse(JSON.stringify(data))
            

            // Set lable
            /* eslint-disable */
            for (const [key, value] of Object.entries(clone[0])) {
                let format = CommonFn.formatLable(key);

                // assign lable
                if(key != Resource.FormatLable.EmployeeId && key != Resource.FormatLable.DepartmentId)
                    if(format)
                    me.jsonFields[format] = key;
                
            }

            //set data export
            this.jsonData = this.formatDataExport(data);

            // set worlsheet name
            this.worlsheetName = Resource.FormatExcel.WorlSheetName;

            // set field name
            this.fieldName = Resource.FormatExcel.FileName;
        },
        /**
         * Function format data export
         * @param {object} data
         * CreatedBy: PQ Huy (12.07.2021)
         */
        formatDataExport(data) {
            var cloneNotRef = JSON.parse(JSON.stringify(data));
            
            cloneNotRef.forEach(item => {
                if(item.dateOfBirth) {
                    item.dateOfBirth = moment(String(item.dateOfBirth)).format("DD/MM/YYYY");
                }

                if(item.identityDate) {
                    item.identityDate = moment(String(item.identityDate)).format("DD/MM/YYYY");
                }

                if(item.createdDate) {
                    item.createdDate = moment(String(item.createdDate)).format("DD/MM/YYYY");
                }

                if(item.modifiedDate) {
                    item.modifiedDate = moment(String(item.modifiedDate)).format("DD/MM/YYYY");
                }
                
                if(item.gender != null) {
                    item.gender = CommonFn.getEnumValue(item.gender, 'Gender');
                }
            })

            return cloneNotRef;
            
        },
        /**
         * function calculator total page
         * @param {int} totalRecord
         * @param {int} pageSize
         * Return a total page
         * CreatedBy: PQ Huy (08.07.2021)
         */
        CalculatorTotaPage(totalRecord, pageSize){
            let totalPage = 0;

            if(totalRecord < pageSize) {
                totalPage = 1;
            } else {
                // Calculator
                totalPage = totalRecord / pageSize;

                if(this.hasDecimal(totalPage)) {
                    totalPage++;
                }
            }

            // Return value
            return parseInt(totalPage);
        },
        /**
         * check decimal number
         * @param {int} num
         * CreatedBy: PQ Huy (08.07.2021)
         */
        hasDecimal (num) {
            return !!(num % 1);
        },
        /**
         * Function opent dialog detail
         * @param {object} item
         * CreatedBy: PQ Huy (05.07.2021)
         */
        async openDialogDetail(item = null){
            try {
                let titleForm = "";
                var cloneData = {};

                if(item != null) {
                    let idItem = item.employeeId;

                    // Call api get data
                    await EmployeesApi.getById(idItem).then((response) => {
                        cloneData = response.data[0];
                    }).catch((error) => {
                        // open dialog form notification
                        console.error(error);
                    });
                        
                    titleForm = Resource.TitleForm.EditForm
                } else {
                    titleForm = Resource.TitleForm.AddForm
                    cloneData = null;
                }

                // Open dialog
                this.$refs.dialogDetail.OpenDialog(cloneData, titleForm);

            } catch (error) {
                // opent popup error
                this.openFormValidate(Resource.TitleForm.ErrorMISA);

                throw error;
            }
        },
        /**
         * Function refresh data table
         * CreatedBy: PQ Huy (05.07.2021)
         */
        refreshData(){
            this.getDataServer();
        },
        /**
         * function open dialog for copy record
         * @param {string} code
         * @param {object} item
         * CreatedBy: PQ Huy (08.07.2021)
         */
        async openDialogCopy(code, item){
            try {
                let titleForm = "",
                cloneData = {},
                idItem = item.employeeId;

                // Call api get data
                await EmployeesApi.getById(idItem).then((response) => {
                    cloneData = response.data[0];
                    cloneData.employeeCode = code;
                }).catch((error) => {
                    // open dialog form notification
                    console.error(error);
                });
                
                // set title and state
                titleForm = Resource.TitleForm.AddForm
                this.$store.state.formMode = Enumeration.FormMode.Copy;

                // Opent dialog Detail
                this.$refs.dialogDetail.OpenDialog(cloneData, titleForm);
            } catch (error) {
                // opent popup error
                this.openFormValidate(Resource.TitleForm.ErrorMISA);

                throw error;
            }
            
        },
        /**
         * Function delete recored
         * @param {Object} item
         * CreatedBy: PQ Huy (08.07.2021)
         */
        async deleteRecord(item){
            try {
                // Open dialog confirm
                this.cloneId = item.employeeId;

                // Validate id is not null in data base
                let isVaild = false;

                await EmployeesApi.getById(this.cloneId).then((response) => {
                    if(response.status == '200' || response.status == '201' || response.status == '204') {
                        if(response.data.length > 0){
                            isVaild = true;
                        }
                    } else {
                        isVaild = false;
                    }
                }).catch((error) => {
                    // open dialog form notification
                    this.openFormValidate(Resource.TitleForm.ErrorMISA);

                    console.error(error);
                    isVaild = false;
                });

                // check result validate
                if(isVaild) {
                    let titleConfirm = `${Resource.TitleForm.DeleteEmployee} <${item.employeeCode}> ${Resource.TitleForm.NoRequire}`
                    this.openFormConfirm(titleConfirm);
                } else {
                    let titleWaring = `${Resource.TitleForm.Employee} <${item.employeeCode}> ${Resource.TitleForm.NoDuplicate}`
                    this.openFormValidate(titleWaring);
                }
            } catch (error) {
                // opent popup error
                this.openFormValidate(Resource.TitleForm.ErrorMISA);

                throw error;
            }
        },
        /**
         * @param {string} title
         * CreatedBy: PQ Huy (08.07.2021)
         */
        openFormValidate(title){
            //config dialog detail
            this.titlePopup=title,
            this.formIconPopup="icon-error",
            this.isBtnLeft=false,
            this.isBtnRightFirst=false,
            this.isBtnRightSecound=true,
            this.titleLeft="",
            this.tleRightFirst="",
            this.tleRightSecound="Đóng",
            this.configClass="jtf-ct"
            this.statusForm=Enumeration.PopupMode.Error

            // call popup waning
            this.$refs.dialogConfirm.openForm();
        },
        /**
         * @param {string} title
         * CreatedBy: PQ Huy (08.07.2021)
         */
        openFormConfirm(title){
            //config dialog detail
            this.titlePopup=title,
            this.formIconPopup="icon-notification",
            this.isBtnLeft=true,
            this.titleLeft="Không",
            this.isBtnRightFirst=false,
            this.isBtnRightSecound=true,
            this.tleRightFirst="",
            this.tleRightSecound="Có",
            this.configClass=""
            this.statusForm=Enumeration.PopupMode.WarningDelete

            // call popup waning
            this.$refs.dialogConfirm.openForm();
        },
        /**
         * Function action delete
         * CreatedBy: PQ Huy (08.07.2021)
         */
        async confirmDelete(){
            let isResult = false;
            // Call api delete data
            await EmployeesApi.delete(this.cloneId).then((response) => {
                if(response.data.misaCode == '200'){
                    isResult = true;
                }
            }).catch((error) => {
                isResult = false;
                console.error(error);
            });

            this.$refs.dialogConfirm.closeForm();
            // Get popup info
            if(isResult) {
                // Delete success
                this.popupNotification('success', Resource.TitleForm.DelSuccess);
                this.getDataServer();
            } else {
                // Delete error
                this.popupNotification('error', Resource.TitleForm.DelError);
            }
        },
        /**
         * Show popup notification
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
            })
        },
    }
})

</script>

<style scoped>
    @import url("../../assets/css/employee/employee.css");
</style>
