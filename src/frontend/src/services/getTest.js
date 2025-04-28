import axiosInstance from "../axiosInstance";

export async function getTest(courseId, moduleId) {
    const authToken = localStorage.getItem('authToken');

    try {
        const response = await axiosInstance.get(`/courses/${courseId}/modules/${moduleId}/questions`, {
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });
        console.log(response.data);
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