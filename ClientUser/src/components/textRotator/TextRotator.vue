<script setup>
import {v4 as uuidv4} from 'uuid';
import {onMounted} from "vue";

const words = ["Passion","Sammenhold","Loyalitet"]
const instanceId = uuidv4()

onMounted(function () {
  rotate(words, instanceId)
})

function rotate(toRotate, instanceId) {
  const textRotate = function (el, toRotate, period) {
    this.toRotate = toRotate;
    this.el = el;
    this.loopNum = 0;
    this.period = parseInt(period, 10) || 2000;
    this.txt = '';
    this.tick();
    this.isDeleting = false;
  };

  textRotate.prototype.tick = function () {
    const i = this.loopNum % this.toRotate.length;
    const fullTxt = this.toRotate[i];
    if (this.isDeleting) {
      this.txt = fullTxt.substring(0, this.txt.length - 1);
    } else {
      this.txt = fullTxt.substring(0, this.txt.length + 1);
    }
    this.el.innerHTML = `<span class="wrap">${this.txt}</span>`;
    const that = this;
    let delta = 300 - Math.random() * 100;
    if (this.isDeleting)
      delta /= 2;
    if (!this.isDeleting && this.txt === fullTxt) {
      delta = this.period;
      this.isDeleting = true;
    } else if (this.isDeleting && this.txt === '') {
      this.isDeleting = false;
      this.loopNum++;
      delta = 500;
    }
    setTimeout(function () {
      that.tick();
    }, delta);
  };
  const element = document.getElementById(instanceId);
  if (!element)
    return
  const period = element.getAttribute('data-period');
  if (toRotate)
    new textRotate(element, toRotate, period);
}

</script>
<template>
  <h1 :id="instanceId" class="txt-rotate" data-period="1000"></h1>
</template>
<style lang="css" scoped>
@media (orientation: portrait) or (max-width: 767px){
  .txt-rotate {
    display: none;
  }
}

@media (orientation: landscape) and (min-width: 768px) {
  .txt-rotate {
    display: grid;
    justify-items: center;
    align-self: center;
    width: min-content;
    color: rgba(211, 211, 211, 0.66) !important;
    font-size: 3rem !important;
  }
}
</style>