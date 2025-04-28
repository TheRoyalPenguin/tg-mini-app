import { useEffect, useState } from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import Header from "../components/header/Header";
import CustomButton from "../components/common/CustomButton";
import Modal from '../components/common/Modal';
import getUserProgress from '../services/getUserProgress';

const ProfilePage = () => {
    const { userId } = useParams();
    const [userData, setUserData] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [activeCourseId, setActiveCourseId] = useState('1');
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const data = await getUserProgress(userId);
                setUserData(data);
            } catch (error) {
                console.error('Ошибка при загрузке данных пользователя:', error);
            }
        };

        fetchUserData();
    }, []);

    const buttonColors = [
        'bg-[#0f9fff]',
        'bg-[#3ebfff]',
        'bg-[#fec810]',
        'bg-[#f87c14]',
    ];

    if (!userData) {
        return (
            <>
                <Header />
                <div className="flex items-center justify-center min-h-screen bg-[#f7f8fc]">
                    <p className="text-gray-500 text-lg">Загрузка...</p>
                </div>
            </>
        );
    }

    const modules = userData.coursesStatistic[activeCourseId] || [];

    const getOverallProgress = () => {
        if (!modules || modules.length === 0) return 0;
        const total = modules.reduce((acc, m) => acc + m.moduleCompletionPercentage, 0);
        return Math.round(total / modules.length);
    };

    return (
        <>
            <Header />
            <div className="flex flex-col items-center justify-start min-h-screen bg-[#f7f8fc] px-4 pt-12">
                <img src="/images/statistics.png" className="w-[180px] mb-6" alt="profile_element" />

                <div className="bg-white shadow-lg rounded-2xl p-6 w-full max-w-[500px] flex flex-col items-center">
                    <h1 className="text-2xl font-bold mb-2 text-center">{userData.name} {userData.surname}</h1>
                    <p className="text-gray-500 text-sm mb-6">Студент</p>

                    <div className="flex space-x-2 mb-6">
                        {Object.keys(userData.coursesStatistic).map(courseId => (
                            <button
                                key={courseId}
                                onClick={() => setActiveCourseId(courseId)}
                                className={`px-4 py-2 rounded-full text-sm font-semibold transition-all ${
                                    activeCourseId === courseId
                                        ? 'bg-[#0f9fff] text-white'
                                        : 'bg-gray-200 text-gray-700'
                                }`}
                            >
                                Курс {courseId}
                            </button>
                        ))}
                    </div>

                    <div className="w-full bg-gray-200 rounded-full h-4 mb-4">
                        <div
                            className="bg-[#0f9fff] h-4 rounded-full transition-all duration-500"
                            style={{ width: `${getOverallProgress()}%` }}
                        ></div>
                    </div>
                    <p className="text-gray-700 mb-6 text-[16px]">
                        Общий прогресс: {getOverallProgress()}%
                    </p>

                    <div className="flex flex-col space-y-3 w-full">
                        {modules.map((module, idx) => {
                            const baseColor = buttonColors[idx % buttonColors.length];
                            const isAvailable = module.isModuleAvailable;
                            const finalColor = isAvailable
                                ? baseColor
                                : `${baseColor} opacity-50 cursor-not-allowed`;

                            const handleClick = () => {
                                if (!isAvailable) {
                                    setIsModalOpen(true);
                                } else {
                                    navigate(`/modules/${module.moduleId}`);
                                }
                            };

                            return (
                                <CustomButton
                                    key={module.moduleId}
                                    text={
                                        isAvailable
                                            ? `Модуль ${module.moduleId} — ${module.moduleCompletionPercentage}%`
                                            : `Не открыт`
                                    }
                                    className={`${finalColor} w-full`}
                                    onClick={handleClick}
                                />
                            );
                        })}
                    </div>
                </div>

                <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
                    Этот модуль пока недоступен. Заверши предыдущие модули!
                </Modal>
            </div>
        </>
    );
};

export default ProfilePage;
