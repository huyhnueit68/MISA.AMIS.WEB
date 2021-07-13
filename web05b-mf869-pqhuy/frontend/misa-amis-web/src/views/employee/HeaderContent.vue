<template>
    <div class="header-content">
        <!-- header content left -->
        <div class="content-left">
            <div class="left-title">
                <span>Nhân viên</span>
            </div>
        </div>
        <!-- header content right -->
        <div class="content-right">
            <!-- show list button toolbar -->
            <div class="right-toolbar">
                <!-- init button add -->
                <button class="toolbar-btn-add" @click="openDetail">
                    <div class="btn-add-title">
                        <span>Thêm mới nhân viên</span>
                    </div>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import Enumeration from '../../js/common/Enumeration'

export default {
    methods: {
        async openDetail(){
            this.$store.state.formMode = Enumeration.FormMode.Add;
            await this.$emit('openDialogDetail');
        }
    }
}
</script>