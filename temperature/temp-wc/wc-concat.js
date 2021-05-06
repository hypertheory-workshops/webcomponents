const fs = require('fs-extra');
const concat = require('concat');

build = async () => {
  const files = [
    './dist/temp-wc/runtime.js',
    './dist/temp-wc/polyfills.js',
    './dist/temp-wc/main.js'
  ];

  await fs.ensureDir('widget');
  await concat(files, 'widget/temp-wc.js');
}
build();
