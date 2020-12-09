"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const app_po_1 = require("./app.po");
describe('App', () => {
    let page;
    beforeEach(() => {
        page = new app_po_1.AppPage();
    });
    it('should display welcome message', () => {
        page.navigateTo();
        expect(page.getMainHeading()).toEqual('Hello, world!');
    });
});
//# sourceMappingURL=app.e2e-spec.js.map