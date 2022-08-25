<template>
    <el-card class="box-card" :style="{'left':x+'px','top':y+'px'}" :stop="11" @click.stop="" style="padding: 0;">
        <div class="text item" @click.stop="callBack('source')" v-if="item.type !=='edge'">
            配置数据源
        </div>
        <div class="text item" @click.stop="callBack('remove')">
            删除
        </div>
    </el-card>
</template>

<script>


  export default {
    name: "contentMemu",
    data() {
      return {
        x: '',
        y: '',
        item: {}
      };
    },
    mounted() {

    },
    methods: {
      initFn(x, y, item) {
        this.x = parseInt(x) + ''
        this.y = y + ""
        if (item) {
          this.item = item
        }

      },
      setItem(item) {
        this.item = item
      },
      callBack(type) {
        this.$emit('callBack', type, this.item)
      }
    }
  };
</script>
<style scoped>
    .box-card {
        position: absolute;
        z-index: 112;
    }

    .box-card ::v-deep .el-card__body {
        padding: 0;
    }

    .text {
        font-size: 14px;
    }

    .item {
        padding: 10px 0;
        text-align: center;
    }

    .item:hover {
        color: #ffffff;
        background-color: #409eff;
    }

    .box-card {
        width: 150px;
    }
</style>
