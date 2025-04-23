import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import CustomButton from '../components/common/CustomButton';
import getAvailableCourses from '../services/getCourses.js';

const WelcomePage = function() {
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const buttonColors = [
        'bg-[#89d018]',
        'bg-[#0793fe]',
        'bg-[#f87c14]'
    ];

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const data = await getAvailableCourses();
                setCourses(data);
            } catch (err) {
                setError(err.response?.data || err.message);
            } finally {
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
                {String(error).includes('авторизации') && (
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
                Привет, <br /> Пользователь!
            </p>
            <p className="font-sans text-[20px]">
                Выберите курс, чтобы <br />скорее приступить <br />к обучению!
            </p>

            {courses.length > 0 ? (
                courses.map((course, index) => (
                    <CustomButton
                        key={course.id}
                        text={course.title}
                        className={`${buttonColors[index % buttonColors.length]} rounded-[15px] text-[18px] mb-2`}
                        onClick={() => navigate(`/courses/${course.id}`)}
                    />
                ))
            ) : (
                <p>Нет доступных курсов</p>
            )}
        </div>
    );
};

export default WelcomePage;
