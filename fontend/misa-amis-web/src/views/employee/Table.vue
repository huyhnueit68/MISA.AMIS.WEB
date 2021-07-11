<template>
<div class="table">
    <div class="tableData">
        <!-- table content -->
        <table class="table-data">
            <thead>
                <tr>
                    <th class="custom-checkbox"><input type="checkbox" v-model="selectedAll" ref="selectedAll"/></th>
                    <th v-for="(item, index) in employeeData.column"
                        :class="{ 'text-right': item.dataType == 'Number', 'text-center': item.dataType == 'Date'}"
                        :key="index"
                        :title="item.columnName"
                    >
                        {{ item.columnName }}
                    </th>
                    <th class="text-center">
                        CHỨC NĂNG
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr
                    v-for="(item, index) in employeeData.gridData"
                    :itemId="item[employeeData.idFieldName]"
                    :key="index"
                    @dblclick="selectedItem(item)"
                    class="tr-body"
                >
                    <td>
                        <input type="checkbox" v-model="selected" :value="item.employeeCode" number>
                    </td>
                    <td
                        v-for="(col, subIndex) in employeeData.column"
                        :class="{ 'text-right': col.dataType == 'Number', 'text-center': col.dataType == 'Date'}"
                        :key="subIndex"
                        :title="formatData(item[col.fieldName], col.dataType, col.enumName)"
                    >
                        <span>{{ formatData(item[col.fieldName],col.dataType, col.enumName) }}</span>
                    </td>
                    <td class="td-body sticky-right set-drop-box">
                        <DropFunction :itemData="item"
                            @editItem="selectedItem(item)"
                            @copyRecord="copyRecord(item)"
                            @deleteRecord="deleteRecord(item)"
                            @setIndexImportant="setIndexImportant(index)"
                        />
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="box-nodata" v-show="isNoData">
            <img src="../../assets/img/eport_nodata.svg" class="nodata-img">
            <br>
            <span class="nodata-text">Không có dữ liệu</span>
        </div>
    </div>
    <!-- paging data -->
    <div class="pagination">
      <span>Tổng số: <span class="bold-text">{{this.$store.state.totalRecord}}</span> bản ghi</span>
      <div class="paging">
        <DxSelectBox
            :data-source="setPageSize"
            :show-clear-button="false"
            display-expr="Title"
            value-expr="Size"
            v-model="currentPageSize"
            class="comboboxPaging"
            @value-changed="changePageSize"
        />
        <div class="pageMovement">
          <div class="movePage"
          :class="setActiveChagePage('Previous')"
          @click="nextPageIndex('Previous')"
          >Trước</div>
          <div class="pages">
              <div v-if="isPrevious" @click="changePageByIndex(1)" class="jump-page">1 ... </div>
            <div v-for="index in countPageDisplay" 
                :key="index"
                :class="{ 'active-page': isActive(index) }" 
                @click="changePageByIndex(index)"
                class="page-item"
            >
                {{index}}
            </div>
            <div v-if="isBehind"
            class="jump-page"
            @click="changePageByIndex($store.state.totalPage)"> 
            ... {{this.$store.state.totalPage}}
            </div>
          </div>
          <div class="movePage" 
          :class="setActiveChagePage('Behind')"
          @click="nextPageIndex('Behind')"
          >Sau</div>
        </div>
      </div>
    </div> 
</div>
</template>

<script>
import DropFunction from "../../components/common/DropFunction";
// init resource, enum, commonfn
import EmployeesApi from "../../helper/api/page/EmployeesApi";
import Resource from "../../js/common/Resource"
import Enumeration from "../../js/common/Enumeration"
import CommonFn from "../../js/common/CommonFn";
import DxSelectBox from 'devextreme-vue/select-box';

export default {
    components: {
        DxSelectBox,
        DropFunction
    },
    props: {
        employeeData: {
            type: Object,
            required: true,
        }
    },
    data() {
        return {
            setPageSize: [
                {
                    'Size': 10,
                    'Title': '10 bản ghi trên 1 trang',
                },
                {
                    'Size': 20,
                    'Title': '20 bản ghi trên 1 trang',
                },
                {
                    'Size': 30,
                    'Title': '30 bản ghi trên 1 trang',
                },
                {
                    'Size': 50,
                    'Title': '50 bản ghi trên 1 trang',
                },
                {
                    'Size': 100,
                    'Title': '100 bản ghi trên 1 trang',
                }
            ],
            isDrop: false,
            cloneCode: "",
            currentPageSize: 0,
            selected: [],
            isPrevious: false,
            isBehind: false,
            isNoData: false
        }
    },
    created() {
        //Set page size
        this.currentPageSize = this.$store.state.pageSize;
    },
    computed: {
        /**
         * Set selected row
         * CreatedBy: PQ Huy (10.07.2021)
         */
        selectedAll: {
            get() {
                return this.employeeData.gridData ? this.selected.length == this.employeeData.gridData.length : false;
            },
            set(value) {
                var selected = [];
                //check value
                if (value) {
                    this.employeeData.gridData.forEach(function (item) {
                        selected.push(item.employeeCode);
                    });
                }
                this.selected = selected;
            },
        },
        /**
         * Function count display page
         * CreatedBy: PQ Huy(10.07.2021)
         */
        countPageDisplay(){
            /* eslint-disable */
            let pageDisplay = [],
                currentPage = this.$store.state.currentPage,
                totalPage = this.$store.state.totalPage;

            // set number page display
            if(totalPage < 3) {
                pageDisplay = this.displAllPage(totalPage);
                this.isBehind = false;
                this.isPrevious = false;
            } else {
                if(currentPage <= 3) {
                    pageDisplay = [1, 2, 3];
                    this.isBehind = true;
                    this.isPrevious = false;
                } else {
                    this.isPrevious = true;
                    this.isBehind = false;
                }

                if(currentPage > 2 && currentPage < totalPage) {
                    pageDisplay = [currentPage - 1 , currentPage, currentPage + 1];
                    this.isBehind = true;
                    this.isPrevious = true;
                }

                if(currentPage >= totalPage - 1) {
                    pageDisplay = [totalPage - 2 , totalPage - 1, totalPage];
                    this.isBehind = false;
                    this.isPrevious = true;
                }
            }

            return pageDisplay;
        },
    },
    methods: {
        /**
         * function format data
         * @param {data need format} data 
         * @param {data type of data} dataType 
         * @param {enum name for data} enumName 
         * @returns data
         * CreatedBy: PQ Huy (06.07.2021)
         */
        formatData(data, dataType, enumName) {
            if(data != null){
                return CommonFn.formatData(data, dataType, enumName);
            }
            else {
                return data;
            }
        },
        /**
         * @param {item selected} item 
         * @returns data
         * CreatedBy: PQ Huy (06.07.2021)
         */
        selectedItem(item) {
            // set state edit form
            this.$store.state.formMode = Enumeration.FormMode.Edit;

            // emit data to employee content
            this.$emit('openDialogEdit', item);
        },
        /**
         * Function reset selected all
         * CreatedBy: PQ Huy (09.07.2021)
         */
        resetSelectedAll(){
            this.$refs.selectedAll.value = "";
            this.selected = [];
        },
        /**
         * Function copy record
         * @param {object} item
         * CreatedBy: PQ Huy(06.07.2021)
         * UpdateBy: PQ Huy(07.07.2021)
         */
        async copyRecord(item){
            // Set new code for data
            await EmployeesApi.getNewEmployeeCode().then((response) => {
                this.cloneCode = response.data;
            }).catch((error) => {
                // open dialog form notification
                console.error(error);
            });

            // set state edit form
            this.$store.state.formMode = Enumeration.FormMode.Add;

            // emit data to employee content
            this.$emit('openDialogCopy',this.cloneCode, item);
        },
        /**
         * Function set index important for drop icon 
         * @param {bool} isShow
         * CreatedBy: PQ Huy (06.07.2021)
         */
        unIndexImportant(){
            this.isDrop = false;
        },
        /**
         * Function set class drop box
         * @param {Object} index
         * CreatedBy: PQ Huy (06.07.2021)
         */
        setIndexImportant(index){
            let drop = document.getElementsByClassName('set-drop-box');

            drop.forEach((item) => {
                item.classList.remove('index-important');
            })
            
            document.getElementsByClassName('set-drop-box')[index].classList.add('index-important');
        },
        /**
         * Function delete reocord
         * @param {Object} index
         * CreatedBy: PQ Huy (08.07.2021)
         */
        deleteRecord(item) {
            // Opent dialog form confirm delete
            this.$emit('deleteRecord', item);
        },
        /**
         * check active menu
         * CreatedBy: PQ Huy(08.07.2021)
         */
        isActive: function (pageNumber) {
            return this.$store.state.currentPage === pageNumber;
        },
        /**
         * Change page index
         * CreatedBy: PQ Huy(08.07.2021)
         */
        changePageByIndex(pageIndex){
            if(pageIndex != this.$store.state.currentPage) {
                this.changePage(pageIndex);
            }
        },
        /**
         * Function change page by index
         * CreatedBy: PQ Huy (08.07.2021)
         */
        changePage(pageIndex){
            this.$store.state.currentPage = pageIndex;

            // Emit to parent and call api paging
            this.$emit('changePageIndex', this.$store.state.valueSearch, pageIndex);
        },
        /**
         * Function set active chaged page
         * @param {string} option
         * CreatedBy: PQ Huy (09.07.2021)
         */
        setActiveChagePage(option){
            if(option == Resource.pageChange.Previous){
                if(this.$store.state.currentPage > 1) {
                    return ['enable-change-index'];
                } else {
                    return ['disable-change-index'];
                }
            } else {
                if(this.$store.state.currentPage < this.$store.state.totalPage) {
                    return ['enable-change-index'];
                } else {
                    return ['disable-change-index'];
                }
            }
        },
        /**
         * Function set page next
         * @param {string} option
         * CreatedBy: PQ Huy (09.07.2021)
         */
        nextPageIndex(option){
            if(option == Resource.pageChange.Previous){
                this.changePage(this.$store.state.currentPage - 1);
            } else {
                this.changePage(this.$store.state.currentPage + 1);
            }
        },
        /**
         * Function check change page size
         * @param {Object} event
         * CreatedBy: PQ Huy (09.0.2021)
         */
        changePageSize(e){
            this.$store.state.pageSize = e.value;
            this.$store.state.currentPage = 1;
            // Emit to parent and call api paging
            this.$emit('changePageIndex', this.$store.state.valueSearch, 1);
        },
        /**
         * Function display all page
         */
        displAllPage(totalPage){
            let listPage = [];
            for(var i = 1; i <= totalPage; i++) {
                listPage.push(i);
            }

            return listPage
        },
        /**
         * Function set display icon no data
         * @param {boolen} state
         * CreatedBy: PQ Huy (10.07.2021)
         */
        setIsNoData(state){
            this.isNoData = state;
        }
    }
}
</script>

<style scoped>
    @import url("../../assets/css/common/grid.css");
</style>
