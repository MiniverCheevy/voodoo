//http://help.appveyor.com/discussions/problems/3289-aspnet-5-version-patching
var jsonfile = require('jsonfile');

debugger;
var file = './voodoo.patterns/project.json';
var buildVersion = process.env.APPVEYOR_BUILD_VERSION;

console.log(buildVersion);
jsonfile.readFile(file, function(err, project) {
	project.version = buildVersion;
	jsonfile.writeFile(file, project, { spaces: 2 }, function (err) {
		console.log(file);
		console.error(err);
	});
});

