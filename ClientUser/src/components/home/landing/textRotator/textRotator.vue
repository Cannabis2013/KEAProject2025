<script>
import rotator from './rotator';
import {v4 as uuidv4} from 'uuid';

export default {
  props: ["words"],
  setup(props) {
    return {
      props,
      instanceId: uuidv4()
    }
  },
  mounted() {
    rotator(this.props.words, this.instanceId)
  }
}

</script>
<template>
  <h1 :id="instanceId" class="txt-rotate" data-period="1000"></h1>
</template>
<style lang="css" scoped>
.txt-rotate {
  color: rgba(211, 211, 211, 0.66) !important;
  font-size: 3rem !important;
}
</style>