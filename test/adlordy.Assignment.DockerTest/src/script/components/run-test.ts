class RunTestComponent {
    static definition = <ng.IComponentOptions>{
        controller: RunTestComponent,
        templateUrl: "/components/run-test.html"
    };

    static $inject = ["$http"];
    constructor(private $http: ng.IHttpService) {
    }
    inProgress: boolean;
    error: any;
    url = "http://192.168.1.110:5000";
    results: TestResults;

    allPassed() {
        return this.results.collections.every(c => c.testsFailed == 0);
    }

    submit() {
        delete this.results;
        delete this.error;
        this.inProgress = true;
        this.$http.post<TestResults>("/v1/runtest", { url: this.url }).then((response) => {
            this.results = response.data;
        }).catch(response => {
            this.error = response;
        }).finally(() => {
            this.inProgress = false;
        });
    }
}