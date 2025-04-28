import axiosInstance from "../axiosInstance";

export async function setLongreadCompletion(longreadId, moduleId) {
    const authToken = localStorage.getItem('authToken');

    try {
        const response = await axiosInstance.post(`/longreads/${longreadId}/completion`,
            { moduleId },
            {
                headers: {
                    'Authorization': `Bearer ${authToken}`
                }
            }
        );
        console.log('Успешное подтверждение:', response.data);
        return response.data;
    } catch (error) {
        if (error.response) {
            console.error('Ошибка сервера:', error.response.data);
        } else {
            console.error('Ошибка сети:', error.message);
        }
        throw error;
    }
}
