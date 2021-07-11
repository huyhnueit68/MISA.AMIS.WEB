import BaseApi from "../base/BaseApi";
import BaseApiConfig from "../base/BaseApiConfig";

class DepartmentApi extends BaseApi {
    constructor() {
        super();

        this.controller = "v1/Departments";
    }

    /**
     * 
     * @returns {string} new code
     * CreatedBy: PQ Huy (08.07.2021)
     */
    getNewEmployeeCode() {
        return BaseApiConfig.get(`${this.controller}/NewEmployeeCode`);
    }
}

export default new DepartmentApi();