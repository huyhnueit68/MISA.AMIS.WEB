<template>
    <div class="main">
        <Header/>
        <Content/>
    </div>
</template>

<script>
import Header from './TheHeader.vue'
import Content from './TheContent.vue'

export default {
  components: {
      Header,
      Content
  },
}
</script>