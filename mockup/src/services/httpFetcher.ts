import axios from "axios";
import API_URL from "./const";

const axiosInstance = axios.create({
    baseURL: API_URL,
});

export default axiosInstance;