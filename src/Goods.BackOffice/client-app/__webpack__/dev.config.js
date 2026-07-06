const { merge } = require('webpack-merge');
const common = require('./common.config.js');

module.exports = (env) =>
	merge(common(env), {
		mode: 'development',
		watch: true,
		devtool: 'source-map',
		cache: {
			type: 'filesystem',
			allowCollectingMemory: true
		},
		watchOptions: {
			ignored: ['src/declares/**', 'node_modules/**']
		}
	});
