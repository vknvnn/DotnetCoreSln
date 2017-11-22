const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const merge = require('webpack-merge');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    //const extractCSS = new ExtractTextPlugin("[name].css"); // build on file [Name].css
    return {
        entry: { 'main': './Frontend/app/app.js' },
        output:
        {
            path: path.resolve(__dirname, 'wwwroot/dist'),
            filename: '[name].bundle.js',
            chunkFilename: '[name].bundle.js',
            publicPath: 'dist/'
        },
        plugins: [
            //extractCSS, // build on file [Name].css
            new webpack.ProvidePlugin({
                $: 'jquery',
                jQuery: 'jquery',
                'window.jQuery': 'jquery',
                moment: 'jquery',
                es6Promise: 'es6-promise',
                Fetch: 'isomorphic-fetch',
                Popper: ['popper.js', 'default']
            }),
             
            new webpack.DefinePlugin({
                'process.env.NODE_ENV': isDevBuild ? JSON.stringify('development') : JSON.stringify('production')
            })
        ].concat(isDevBuild ? [] : [new webpack.optimize.UglifyJsPlugin()]),
        module:
        {
            rules: [
                {
                    test: /\.css$/,
                    //use: extractCSS.extract([{ loader: (isDevBuild ? 'css-loader' : 'css-loader?minimize')}]) // build on file [Name].css
                    use: [{ loader: "style-loader" },{ loader: "css-loader" }] //build css on file bulder.js
                    
                }, {
                    test: /\.js?$/,
                    use:
                    {
                        loader: 'babel-loader',
                        options:
                        {
                            presets: ['react', 'es2015', 'env', 'stage-0'],
                            plugins: ['transform-es3-member-expression-literals', 'transform-es3-property-literals']
                        }
                    }
                }
            ]
        }
    }
};

