<template>
    <div class="toolbar-table">
        <div class="toolbar-table-left">
        </div>
        <div class="toolbar-table-right">
            <div class="toolbar-search">
                <input placeholder="Tìm theo mã, tên nhân viên" type="text" class="search-input"
                v-debounce:1000ms="searchFunc" ref="inputSearch">
                <div class="box-search-icon">
                    <div class="search-icon mi-icon mi-icon-16"></div>
                </div>
            </div>
            <!-- component button action -->
            <div class="box-refresh"
                @click="refreshData"
                @mouseleave="setUnHover('refresh-data')"
                @mouseover="setHover('refresh-data')"
            >
                <div class="mi-icon mi-icon-24" 
                :class="{
                    'refresh-data': hoverRefresh == false,
                    'refresh-data-hover': hoverRefresh == true,
                }"
                v-tooltip.bottom="optionTooltip('Lấy lại dữ liệu')"
                data-toggle="tooltip tool-box" 
                ref="iconRefresh"></div>
            </div>
            <export-excel 
                :data="jsonData"
                :fields="jsonFields"
                :worksheet="worlsheetName"
                :name="fieldName"
            >
                <div class="box-excel"
                    @mouseleave="setUnHover('export-excel')"
                    @mouseover="setHover('export-excel')"
                >
                <div class="mi-icon mi-icon-24"
                    :class="{
                        'export-excel': hoverExcel == false,
                        'export-excel-hover': hoverExcel == true,
                    }"
                    v-tooltip.bottom="optionTooltip('Xuất ra excel')"
                    data-toggle="tooltip tool-box" 
                    ref="exportExcel">
                </div>
            </div>
            </export-excel>
        </div>
    </div>
</template>

<script>
import Resource from "../../js/common/Resource"

export default ({
    setup() {
        
    },
    props: {
        jsonData: Array,
        jsonFields: Object,
        worlsheetName:{
            type: String,
            default: 'worlsheet',
        },
        fieldName:{
            type: String,
            default: 'ExportData.xls',
        }
    },
    data() {
        return {
            hoverRefresh: false,
            hoverExcel: false,
            optionsReload: {
                content: Resource.Tooltip.Refresh,
                autoHide: false,
                show: false,
                classes: 'tooltip-reload'
            },
            optionsExport: {
                content: Resource.Tooltip.Export,
                autoHide: false,
                show: false,
                classes: 'tooltip-export'
            },
            json_meta: [
                [
                    {
                        'key': 'charset',
                        'value': 'utf-8'
                    }
                ]
            ],
        }
    },
    methods: {
        /**
         * Function set tooltip
         * @param {string} title
         * CreatedBy: PQ Huy(09.07.2021)
         */
        optionTooltip(title){
            return{
                content: title,
                autoHide: false,
                show: false,
                classes: 'tooltip-toolbar'
            }
        },
        /**
         * Function set hover for icon
         * @param {indexName} index of class hover
         * CreatedBy: PQ Huy (05.07.2021)
         */
        setHover(indexName){
            switch (indexName) {
                case 'refresh-data':
                    this.hoverRefresh = true;
                    break;
                case 'export-excel':
                    this.hoverExcel = true;
                    break;
            }
        },
        /**
         * Function set hover for icon
         * @param {indexName} index of class hover
         * CreatedBy: PQ Huy (05.07.2021)
         */
        setUnHover(indexName){
           switch (indexName) {
                case 'refresh-data':
                    this.hoverRefresh = false;
                    break;
                case 'export-excel':
                    this.hoverExcel = false;
                    break;
            }
        },
        /**
         * Function refresh data
         * CreatedBy: PQ Huy (05.07.2021)
         */
        refreshData(){
            // Reload state search and input search
            this.$store.state.stateSearch = false;
            this.$store.state.valueSearch = '';

            // Reset data filter
            this.$refs.inputSearch.value = '';

            this.$emit('refreshData');
        },
        /**
         * Function search
         * @param {String} val 
         * CreatedBy: PQ Huy (09.07.2021)
         */
        searchFunc(val){
            // Save value search
            this.$store.state.valueSearch = val;

            if(val){
                this.$store.state.stateSearch = true;
            } else {
                this.$store.state.stateSearch = false;
            }

            // Emit to page
            this.$emit('searchFunc', val);
        }
    }
})
</script>

<style scoped>
    @import url("../../assets/css/employee/table.css");
</style>