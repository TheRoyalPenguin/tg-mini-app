// UserStatisticsModal.js
import { useState, useEffect } from "react";
import axios from "axios";
import { Dialog } from "@headlessui/react";

export default function UserStatisticsModal({ userId, isOpen, onClose }) {
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [expandedCourses, setExpandedCourses] = useState({});

  useEffect(() => {
    if (!isOpen) return;

    const fetchStats = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5000/Admin/test-statistic/user/${userId}`
        );
        setStats(response.data);
      } catch (err) {
        console.error("Ошибка загрузки статистики:", err);
        setError("Не удалось загрузить статистику");
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, [userId, isOpen]);

  const toggleCourse = (courseId) => {
    setExpandedCourses(prev => ({
      ...prev,
      [courseId]: !prev[courseId]
    }));
  };

  const formatDate = (dateString) => {
    if (!dateString) return "Н/Д";
    const options = {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    };
    return new Date(dateString).toLocaleDateString('ru-RU', options);
  };

  return (
    <Dialog open={isOpen} onClose={onClose} className="relative z-50">
      <div className="fixed inset-0 bg-black/30" />
      
      <div className="fixed inset-0 flex items-center justify-center p-4">
        <Dialog.Panel className="w-full max-w-4xl bg-white rounded-xl p-6 max-h-[90vh] overflow-y-auto">
          <Dialog.Title className="text-xl font-bold mb-4">
            Статистика пользователя
          </Dialog.Title>

          {loading && <div className="text-center py-4">Загрузка...</div>}
          {error && <div className="text-red-500 text-center py-4">{error}</div>}
          
          {stats && (
            <div className="space-y-6">
              {/* Основная информация */}
              <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-6">
                <div className="bg-gray-50 p-3 rounded-lg">
                  <p className="text-sm text-gray-500">Telegram ID</p>
                  <p className="font-medium">{stats.tgId}</p>
                </div>
                <div className="bg-gray-50 p-3 rounded-lg">
                  <p className="text-sm text-gray-500">Статус</p>
                  <p className={stats.isBanned ? "text-red-600" : "text-green-600"}>
                    {stats.isBanned ? "Заблокирован" : "Активен"}
                  </p>
                </div>
                <div className="bg-gray-50 p-3 rounded-lg">
                  <p className="text-sm text-gray-500">Телефон</p>
                  <p className="font-medium">{stats.phoneNumber || "Н/Д"}</p>
                </div>
              </div>

              {/* Статистика по курсам */}
              {Object.entries(stats.coursesStatistic).map(([courseId, modules]) => (
                <div key={courseId} className="bg-gray-50 rounded-lg p-4">
                  <div
                    className="flex justify-between items-center cursor-pointer"
                    onClick={() => toggleCourse(courseId)}
                  >
                    <h3 className="text-lg font-semibold">Курс {courseId}</h3>
                    <span className="text-xl">
                      {expandedCourses[courseId] ? '−' : '+'}
                    </span>
                  </div>

                  {expandedCourses[courseId] && (
                    <div className="mt-4 space-y-4">
                      {modules.map((module) => (
                        <div key={module.moduleId} className="bg-white rounded-lg p-4 shadow">
                          <div className="flex justify-between items-center mb-3">
                            <div>
                              <h4 className="font-medium">Модуль {module.moduleId}</h4>
                              <span className={`text-sm ${
                                module.isModuleCompleted ? "text-green-600" : "text-yellow-600"
                              }`}>
                                {module.isModuleCompleted ? "Завершён" : "В процессе"}
                              </span>
                            </div>
                            <div className="w-24 h-2 bg-gray-200 rounded-full">
                              <div 
                                className="h-full bg-blue-500 rounded-full" 
                                style={{ width: `${module.moduleCompletionPercentage}%` }}
                              />
                            </div>
                          </div>

                          {module.testResults?.length > 0 && (
                            <div className="space-y-3">
                              <h5 className="font-medium text-sm">Попытки тестирования:</h5>
                              {module.testResults.map((test, i) => (
                                <div key={i} className="p-3 bg-gray-50 rounded">
                                  <div className="flex justify-between items-center mb-2">
                                    <span className="text-sm font-medium">
                                      Попытка #{test.attemptNumber}
                                    </span>
                                    <span className="text-sm text-gray-500">
                                      {formatDate(test.timestamp)}
                                    </span>
                                  </div>
                                  <div className="grid grid-cols-3 gap-2 text-sm">
                                    <div>
                                      <span className="text-green-600">
                                        +{test.correctAnswersCount}
                                      </span>
                                    </div>
                                    <div>
                                      <span className="text-red-600">
                                        -{test.wrongAnswersCount}
                                      </span>
                                    </div>
                                    <div>
                                      <span className="font-medium">
                                        {test.score}%
                                      </span>
                                    </div>
                                  </div>
                                </div>
                              ))}
                            </div>
                          )}
                        </div>
                      ))}
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}

          <button
            onClick={onClose}
            className="mt-4 px-4 py-2 bg-gray-100 hover:bg-gray-200 rounded-lg"
          >
            Закрыть
          </button>
        </Dialog.Panel>
      </div>
    </Dialog>
  );
}