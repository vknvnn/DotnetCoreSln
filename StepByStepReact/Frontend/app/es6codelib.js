export default class ES6Lib {
    constructor() {
        this.user = {name: 'Nghiep', age: 29}
        this.text = "Data from ES6 class";
     }
    getData() {
        var copyUser = {...this.user, age : 100};
        console.log(this.user, copyUser)
        return this.text;
    }
}