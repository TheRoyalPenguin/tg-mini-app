import { useState } from 'react';
import Header from "../components/header/Header";
import CustomButton from "../components/common/CustomButton";
import Modal from '../components/common/Modal';
import { useNavigate } from "react-router-dom";

const ProfilePage = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [activeCourseId, setActiveCourseId] = useState('1');
    const navigate = useNavigate();

    const userData = {
        "id": 2,
        "tgId": 987,
        "name": "ivan",
        "surname": "ivanov",
        "patronymic": "ivanovich",
        "phoneNumber": "9876",
        "isBanned": false,
        "coursesStatistic": {
            "1": [
                {
                    "moduleId": 1,
                    "moduleAccessId": 1,
                    "isModuleAvailable": true,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                },
                {
                    "moduleId": 2,
                    "moduleAccessId": 2,
                    "isModuleAvailable": false,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                },
                {
                    "moduleId": 3,
                    "moduleAccessId": 3,
                    "isModuleAvailable": false,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                },
                {
                    "moduleId": 4,
                    "moduleAccessId": 4,
                    "isModuleAvailable": false,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                }
            ],
            "2": [
                {
                    "moduleId": 1,
                    "moduleAccessId": 5,
                    "isModuleAvailable": true,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                },
                {
                    "moduleId": 6,
                    "moduleAccessId": 6,
                    "isModuleAvailable": false,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                },
                {
                    "moduleId": 7,
                    "moduleAccessId": 7,
                    "isModuleAvailable": false,
                    "completedLongreadsIds": [],
                    "testTriesCount": 0,
                    "moduleCompletionPercentage": 0,
                    "isModuleCompleted": false,
                    "completionDate": null,
                    "lastActivity": null
                }
            ]
        }
    };


    const buttonColors = [
        'bg-[#0f9fff]',
        'bg-[#3ebfff]',
        'bg-[#fec810]',
        'bg-[#f87c14]',
    ];

    const modules = userData.coursesStatistic[activeCourseId] || [];

    const getOverallProgress = () => {
        if (!modules || modules.length === 0) return 0;
        const total = modules.reduce((acc, m) => acc + m.moduleCompletionPercentage, 0);
        return Math.round(total / modules.length);
    };

    return (
        <>
            <Header />
            <div className="flex flex-col items-center justify-start min-h-screen bg-[#f7f8fc] px-4 pt-6">
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
                                    navigate(`/modules/${module.moduleId}`)
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
