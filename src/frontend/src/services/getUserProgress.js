import axiosInstance from "../axiosInstance";
import axios from "axios";

async function getUserProgress(userId) {
    const authToken = localStorage.getItem('authToken');

    try {
        const response = await axios.get(`http://localhost:5000/user/${userId}`, {
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });

        return response.data;

    } catch (error) {
        if (error.response) {
            if (error.response.status === 401) {
                console.error('Ошибка авторизации:', error.response.data);
            } else {
                console.error('Ошибка сервера:', error.response.data);
            }
        } else {
            console.error('Ошибка сети:', error.message);
        }
        throw error;
    }
}

export default getUserProgress;