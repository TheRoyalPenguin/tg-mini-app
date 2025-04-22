import { useState, useEffect } from 'react';
import axios from 'axios';
import CustomButton from './CustomButton'; // Предположим, что компонент кнопки импортирован

const WelcomePage = function() {
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchCourses = async () => {
            const authToken = localStorage.getItem('authToken');

            try {
                const response = await axios.get('http://localhost:5000/api/Enrollments/availablecourses', {
                    headers: {
                        'Authorization': `Bearer ${authToken}`
                    }
                });

                setCourses(response.data);
                setLoading(false);
            } catch (error) {
                setError(error.response?.data || error.message);
                setLoading(false);
            }
        };

        fetchCourses();
    }, []);

    if (loading) {
        return <div>Загрузка доступных курсов...</div>;
    }

    if (error) {
        return (
            <div className="text-red-500">
                Ошибка загрузки курсов: {error}
                {error.includes('авторизации') && (
                    <button onClick={() => window.location.href = '/login'}>
                        Перейти к авторизации
                    </button>
                )}
            </div>
        );
    }

    return (
        <div className="flex flex-col items-center h-screen text-center bg-[#f7f8fc]">
            <img
                src="/images/universal_element_2.png"
                className="w-[200px] mt-[20px]"
                alt="universal_element"
            />
            <p className="font-sans mt-[10px] mb-[7px] font-bold text-[33px]">
                Привет, <br />Телеграм ID!
            </p>
            <p className="font-sans text-[20px]">
                Выберите курс, чтобы <br />скорее приступить <br />к обучению!
            </p>

            {courses.length > 0 ? (
                courses.map(course => (
                    <CustomButton
                        key={course.id}
                        text={course.title}
                        className="bg-[#89d018] rounded-[15px] text-[18px] mb-2"
                        onClick={() => {/* Обработчик выбора курса */}}
                    />
                ))
            ) : (
                <p>Нет доступных курсов</p>
            )}
        </div>
    );
}

export default WelcomePage;