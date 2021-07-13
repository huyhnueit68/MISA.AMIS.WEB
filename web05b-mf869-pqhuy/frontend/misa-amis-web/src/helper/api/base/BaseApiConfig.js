import ApiConfig from "../config/ApiConfig";
import axios from "axios";

/**
 * init base api
 */
var BaseApiConfig = axios.create({
    baseURL: ApiConfig,
    headers: { 'Content-type': 'application/json' }
});

export default BaseApiConfig;