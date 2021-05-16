import Vue from 'vue'

export function removeObjProperty(obj =[],propertyName=''){

    const tempObj=deepCopy(obj);
Vue.delete(tempObj,propertyName);//vue方法
return tempObj;
}

//深拷贝，引用类型直接浅拷贝，会影响全局
export function deepCopy(obj) {
    // 只拷贝对象
    if (typeof obj !== 'object') return;
    // 根据obj的类型判断是新建一个数组还是一个对象
    var newObj = obj instanceof Array ? [] : {};
    for (var key in obj) {
      // 遍历obj,并且判断是obj的属性才拷贝
      if (obj.hasOwnProperty(key)) {
        // 判断属性值的类型，如果是对象递归调用深拷贝
        newObj[key] = typeof obj[key] === 'object' ? deepCopy(obj[key]) : obj[key];
      }
    }
    return newObj;
  }
