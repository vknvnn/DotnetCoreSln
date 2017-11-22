//$ = require('jquery');
//import { getText } from './lib';
require('./lib');
import 'bootstrap/dist/css/bootstrap.css';
import '../css/site.css';


import ES6Lib from './es6codelib';
import React from 'react';
import ReactDOM from 'react-dom';



//function getText() {
//    return "Data from getText function in dep.js";
//}
//console.log(getText);
//document.getElementById("fillthis").innerHTML = lib.getText();
$('#fillthiswithjquery').html('Filled by Jquery!!');
let myES6Object = new ES6Lib();
$('#fillthiswithes6lib').html(myES6Object.getData());

//Lazy loading
function component() {
    import(/* webpackChunkName: "fetchdata" */'./fetchdata').then(module => {
        const FetchData = module.default
        ReactDOM.render(<FetchData />, document.getElementById('reactcomponentwithapidata'));
    });
    import(/* webpackChunkName: "reactcomponent" */'./reactcomponent').then(module => {
        const Counter = module.default
        ReactDOM.render(<Counter />, document.getElementById('basicreactcomponent'));
    });
    import(/* webpackChunkName: "amd" */'./amd').then(module => {
    });
    import(/* webpackChunkName: "lodash" */'lodash').then(module => {
        var _ = module;   
        var element = document.createElement('div');
        var button = document.createElement('button');
        var br = document.createElement('br');
        button.innerHTML = 'Click me and look at the console!';
        element.innerHTML = _.join(['Hello', 'webpack'], ' ');
        element.appendChild(br);
        element.appendChild(button);

        button.onclick = e => import(/* webpackChunkName: "lazyload" */'./lazyload').then(module => {
            var print = module.default;
            print();
        });
        document.body.appendChild(element);
    });
    
}
component();

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept(); 
}