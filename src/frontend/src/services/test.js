import axiosInstance from "../axiosInstance";

export const fetchTest = async () => {
    try {
        const response = await axiosInstance.get('/test');
        console.log(response.data);
    } catch (e) {
        console.error(e);
    }
};