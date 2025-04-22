import axios from "axios";

async function getAvailableCourses() {
    const authToken = localStorage.getItem('authToken');

    try {
        const response = await axios.get('http://localhost:5000/api/Enrollments/availablecourses', {
            headers: {
                'Authorization': `Bearer ${authToken}`
            }
        });

        console.log('Доступные курсы:', response.data);
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
            console.log(`Курс ID: ${course.id}, Название: ${course.title}`);
        });
    })
    .catch(() => {
    });