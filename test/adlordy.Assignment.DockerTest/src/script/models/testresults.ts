interface TestResults {
    collections: TestCollectionResult[];
}

interface TestResult {
    testName: string;
    passed: boolean;
    executionTime: number;
}

interface TestCollectionResult {
    assembly: string;
    collectionName: string;
    testsFailed: number;
    testsRun: number;
    testsSkipped: number;
    tests: TestResult[];
}