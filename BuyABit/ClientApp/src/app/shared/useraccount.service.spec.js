"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var useraccount_service_1 = require("./useraccount.service");
describe('UseraccountService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(useraccount_service_1.UseraccountService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=useraccount.service.spec.js.map