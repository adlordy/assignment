/// <reference path="components/run-test.ts" />
/// <reference path="components/requirements.ts" />

angular.module("test", [])
    .component("requirements", RequirementsComponent.definition)
    .component("runTest", RunTestComponent.definition);