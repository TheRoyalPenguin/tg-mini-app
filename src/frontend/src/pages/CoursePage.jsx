import { useNavigate, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getCourseModules } from '../services/getModules.js';
import CustomButton from '../components/common/CustomButton';

const CoursePage = function () {
    const { courseId } = useParams();
    const [modules, setModules] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const buttonColors = [
        'bg-[#0f9fff]',
        'bg-[#3ebfff]',
        'bg-[#fec810]',
        'bg-[#f87c14]',
    ];

    useEffect(() => {
        const loadModules = async () => {
            try {
                const data = await getCourseModules(courseId);
                console.log(data);
                setModules(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        loadModules();
    }, [courseId]);

    if (loading) return <div>Загрузка модулей...</div>;
    if (error) return <div className="text-red-500">Ошибка: {error}</div>;

    return (
        <div className="flex flex-col items-center justify-center h-screen text-center bg-[#f7f8fc]">
            <img src="/images/universal_element_3.png" className="w-[250px]" alt="universal_element" />

            {modules.length === 0 ? (
                <p className="text-gray-500 mt-4 text-[18px]">По этому курсу пока нет модулей. Приходи завтра!</p>
            ) : (
                <div className="flex flex-col space-y-[13px]">
                    <p className="font-sans mt-[10px] font-bold text-[28px]">
                        Готов прокачать <br/>навыки Тим Лида?
                    </p>
                    {modules.map((module, idx) => (
                        <CustomButton
                            key={module.id}
                            text={`${module.title}`}
                            className={`${buttonColors[idx % buttonColors.length]}`}
                            onClick={() => navigate(`/modules/${module.id}`)}
                        />
                    ))}
                </div>
            )}
        </div>
    );

};

export default CoursePage;
