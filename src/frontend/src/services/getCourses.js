import axiosInstance from "../axiosInstance";

async function getAvailableCourses() {
    const authToken = localStorage.getItem('authToken');

    try {
        const response = await axiosInstance.get('/courses/available', {
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

getAvailableCourses()
    .then(courses => {
        courses.forEach(course => {
        });
    })
    .catch(() => {
    });

export default getAvailableCourses;