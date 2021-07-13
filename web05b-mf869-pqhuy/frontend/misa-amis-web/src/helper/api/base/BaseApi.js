import BaseApiConfig from "./BaseApiConfig";

export default class BaseApi {
    /**
     * init contructor
     */
    constructor() {
        this.controller = null;
    }

    /**
     * Function set all record
     * @returns {Object} list record
     * CreatedBy: PQ Huy (07.07.2021)
     */
    getAll() {
        return BaseApiConfig.get(`${this.controller}`);
    }

    /**
     * Function get record by id
     * @param {string} id 
     * @returns a first record
     * CreatedBy: PQ Huy (07.07.2021)
     */
    getById(id) {
        return BaseApiConfig.get(`${this.controller}/${id}`)
    }

    /**
     * Function insert data
     * @param {Json} data 
     * @returns staus insert
     * CreatedBy: PQ Huy (08.07.2021)
     */
    insert(data) {
        return BaseApiConfig.post(`${this.controller}`, data);
    }

    /**
     * Function update data
     * @param {string} id 
     * @param {Json} data 
     * @returns result update data
     * CreatedBy: PQ Huy (08.07.2021)
     */
    update(id, data) {
        return BaseApiConfig.put(`${this.controller}/${id}`, data);
    }

    /**
     * Function delete record
     * @param {string} id 
     * @returns status delete
     * CreatedBy: PQ Huy (10.07.2021)
     */
    delete(id) {
        return BaseApiConfig.delete(`${this.controller}/${id}`);
    }
}