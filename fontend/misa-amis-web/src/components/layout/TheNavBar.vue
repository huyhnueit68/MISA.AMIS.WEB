 <template>
  <div class="m-navbar">
      <div class="box-navbar">
          <!-- main nav bar with icon toggle and icon logo amis -->
          <div class="navbar-header">
            <div class="navbar-header-icon"></div>
            <div class="navbar-header-logo"><img class="header-logo" src="../../assets/img/Logo_Module.svg" alt=""></div>
          </div>
          <!-- for list icon menu -->
          <div class="navbar-items">
              <router-link
                v-for="(item, index) in navbarItems" :key="index" :to="getRouter(item.m_router)"
                class="items-box-content m-tooltip"
                @mouseenter.native="setHover(index)"
                @mouseleave.native="setUnHover"
                :class="{
                    'navbar-item-selected': item.m_router == $route.path,
                    'navbar-item-hover': item.m_router != $route.path && index == crtHover
                }"
                v-tooltip.right="optionTooltip(item.m_title)"
                data-toggle="tooltip tool-box" 
              > 
                <!-- render icon for menu -->
                <div class="navbar-items-icon">
                    <div class="icon-child mi-icon mi-icon-24"  :class="setActiveIcon(item.c_icon, item.m_router, $route.path)"></div>
                </div>
                <!-- render title -->
                <div class="navbar-items-title">{{ item.m_title }}</div>
              </router-link>
          </div>
      </div>
  </div>
</template>

<script>

export default ({
    props: {
        // get props nav bar item
        navbarItems: {
            type: Array,
            required: true,
        },
    },
    data(){
        return {
            crtHover: null,
        }
    },
    methods: {
        /**
         * Function set tooltip for icon
         * @param {string} title
         * CreatedBy: PQ Huy (10.07.2021)
         */
        optionTooltip(title){
            return {
                content: title,
                autoHide: false,
                show: false,
                classes: 'tooltip-navbar'
            }
        },
        /**
         * function get link router
         * @param {link}
         * CreatedBy: PQ Huy (05.07.2021)
         */
        getRouter(link) {
            return link + "";
        },

        /**
         * function set hover menu in list menu
         * @param {index} index of hover menu
         * CreatedBy: PQ Huy (05.07.2021)
         */
        setHover(index) {
            this.crtHover = index;
        },

        /**
         * function set unhover for list items
         * @param {e} index mouse hover
         * CreatedBy: PQ Huy (05.07.2021)
         */
        setUnHover(e) {
            e.target.classList.remove("navbar-item-hover");
        },

        /**
         * function set active icon in menu
         * @param {c_icon} class icon active
         * @param {c_icon} class icon setActiveIcon
         * @param {c_icon} class icon active
         * CreatedBy: PQ Huy (05.07.2021)
         */
        setActiveIcon(c_icon, m_router, path){
            // check current is rounte path match with element path set class active icon
            if(m_router == path)
                return [c_icon+'-active'];
            else
                return [c_icon];
        }
    }
})

</script>


<style scoped>
    /* custom css router link */
    a {
        text-decoration: none
    }

    /* import header css */
    @import url("../../assets/css/common/navbar.css");
</style>