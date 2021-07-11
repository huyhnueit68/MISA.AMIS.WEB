var ApiConfig = {
    development: "https://localhost:44313/",
    production: "http://localhost:8080/"
}

export default ApiConfig[process.env.NODE_ENV]