import {useNavigate, useParams} from 'react-router-dom';
import { useEffect, useState } from 'react';
import { getCourseModules } from '../services/getModules.js';
import CustomButton from '../components/common/CustomButton';
import Modal from '../components/common/Modal';
import Header from "../components/header/Header";

const CoursePage = function () {
    const { courseId } = useParams();
    const [modules, setModules] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const [isModalOpen, setIsModalOpen] = useState(false);

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
        <>
            <Header/>
            <div className="flex flex-col items-center justify-center min-h-screen text-center bg-[#f7f8fc]">
            <img src="/images/universal_element_3.png" className="w-[250px]" alt="universal_element" />

            {modules.length === 0 ? (
                <p className="text-gray-500 mt-4 text-[18px]">
                    По этому курсу пока нет модулей. Приходи завтра!
                </p>
            ) : (
                <div className="flex flex-col space-y-[13px] mt-4">
                    <p className="font-sans mt-[10px] font-bold text-[28px] mb-3">
                        Готов прокачать <br/>навыки Тим Лида?
                    </p>
                    {modules.map((module, idx) => {
                        const isAccessible = module.isAccessed === true;
                        const baseColor = buttonColors[idx % buttonColors.length];
                        const finalColor = isAccessible ? baseColor : `${baseColor} opacity-50 cursor-not-allowed`;

                        const handleClick = () => {
                            if (!isAccessible) {
                                setIsModalOpen(true);
                            } else {
                                navigate(`/modules/${module.id}`)
                            }
                        };

                        return (
                            <CustomButton
                                key={module.id}
                                text={module.title}
                                className={finalColor}
                                onClick={handleClick}
                            />
                        );
                    })}

                </div>
            )}

            <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
                Данный модуль пока недоступен. Заверши предыдущие модули, чтобы открыть его.
            </Modal>
        </div>
        </>
    );
};

export default CoursePage;
