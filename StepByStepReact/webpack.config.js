const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const extractCSS = new ExtractTextPlugin('allstyles.css');
module.exports = {
    entry: { 'main': './Frontend/app/app.js' },
    output: 
    {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: 'bundle.js',
        publicPath: 'dist/'
    },
    plugins: [
       extractCSS,
       new webpack.ProvidePlugin({
                  $: 'jquery',
             jQuery: 'jquery',
    'window.jQuery': 'jquery',
             Popper: ['popper.js', 'default']                    
        }),
       new webpack.optimize.UglifyJsPlugin()
        
    ],
    module: {
                rules: [
                    {
                        test: /\.css$/, 
                        use: extractCSS.extract(['css-loader?minimize']) // build on file allstyles.css
                        //[{ loader: "style-loader" },{ loader: "css-loader" }]} build on file bulder.js
                    },{ 
                        test: /\.js?$/, 
                        use: 
                        { 
                            loader: 'babel-loader', 
                            options: 
                            { 
                                presets:['@babel/preset-react', '@babel/preset-env', '@babel/preset-stage-3']
                            }
                        }
                    }
               ]
            }
};
