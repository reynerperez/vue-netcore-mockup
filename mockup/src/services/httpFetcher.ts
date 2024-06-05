import axios from "axios";
import API_URL from "./const";
import { useInvoiceStore } from "@/stores/invoice";

const axiosInstance = axios.create({
    baseURL: API_URL,
});

axiosInstance.interceptors.response.use(function (response) {
    return response;
}, function (error) {
    if (error.response.status === 401) {
        const store = useInvoiceStore()
        store.snackbar.show = true;
        store.snackbar.title = "Error Unauthorized"
        store.snackbar.message = "You need to be authorized to make this request"
    }
    return Promise.reject(error);
});


export default axiosInstance;