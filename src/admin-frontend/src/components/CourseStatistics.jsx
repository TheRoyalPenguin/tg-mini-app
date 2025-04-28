import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";

export default function CourseStatistics() {
  const { courseId } = useParams();
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [expandedSections, setExpandedSections] = useState({
    course: true,
    users: {},
    modules: {}
  });

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5000/Admin/test-statistic/course/${courseId}`
        );
        setStats(response.data);
      } catch (error) {
        console.error("Ошибка загрузки статистики:", error);
        setError("Не удалось загрузить данные");
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, [courseId]);

  const toggleSection = (section, id = null) => {
    setExpandedSections(prev => {
      if (section === 'course') {
        return { ...prev, course: !prev.course };
      }
      if (section === 'user') {
        return { 
          ...prev, 
          users: { ...prev.users, [id]: !prev.users[id] },
          modules: { ...prev.modules, [id]: {} }
        };
      }
      if (section === 'module') {
        const [userId, moduleId] = id.split('-');
        return { 
          ...prev, 
          modules: { 
            ...prev.modules, 
            [userId]: { 
              ...prev.modules[userId], 
              [moduleId]: !prev.modules[userId]?.[moduleId] 
            } 
          }
        };
      }
      return prev;
    });
  };

  const formatDate = (dateString) => {
    const options = {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    };
    return new Date(dateString).toLocaleDateString('ru-RU', options);
  };

  if (loading) return <div className="p-4 text-gray-600">Загрузка статистики...</div>;
  if (error) return <div className="p-4 text-red-500">{error}</div>;
  if (!stats?.length) return <div className="p-4">Данные не найдены</div>;

  return (
    <div className="container mx-auto px-4 py-6">
      {/* Заголовок курса */}
      <div className="bg-white rounded-lg shadow-md mb-6">
        <div 
          className="p-4 cursor-pointer hover:bg-gray-50 flex justify-between items-center"
          onClick={() => toggleSection('course')}
        >
          <h1 className="text-2xl font-bold text-gray-800">
            Статистика курса #{courseId}
          </h1>
          <span className="text-xl">
            {expandedSections.course ? '−' : '+'}
          </span>
        </div>

        {/* Общая информация о курсе */}
        {expandedSections.course && (
          <div className="p-4 border-t">
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-4">
              <div className="bg-gray-50 p-3 rounded-lg">
                <p className="text-sm text-gray-500">Всего пользователей</p>
                <p className="text-lg font-bold">{stats.length}</p>
              </div>
              <div className="bg-gray-50 p-3 rounded-lg">
                <p className="text-sm text-gray-500">Активных</p>
                <p className="text-lg font-bold text-green-600">
                  {stats.filter(u => !u.isBanned).length}
                </p>
              </div>
              <div className="bg-gray-50 p-3 rounded-lg">
                <p className="text-sm text-gray-500">Заблокированных</p>
                <p className="text-lg font-bold text-red-600">
                  {stats.filter(u => u.isBanned).length}
                </p>
              </div>
            </div>
          </div>
        )}
      </div>

      {/* Список пользователей */}
      <div className="space-y-4">
        {stats.map((user) => (
          <div key={user.id} className="bg-white rounded-lg shadow-md">
            {/* Заголовок пользователя */}
            <div 
              className="p-4 cursor-pointer hover:bg-gray-50 flex justify-between items-center"
              onClick={() => toggleSection('user', user.id)}
            >
              <div>
                <h2 className="text-lg font-semibold">
                  {user.name} {user.surname}
                  {user.isBanned && (
                    <span className="ml-2 text-red-500 text-sm">(заблокирован)</span>
                  )}
                </h2>
                <p className="text-sm text-gray-500">TG ID: {user.tgId}</p>
              </div>
              <span className="text-xl">
                {expandedSections.users[user.id] ? '−' : '+'}
              </span>
            </div>

            {/* Детали пользователя */}
            {expandedSections.users[user.id] && (
              <div className="p-4 border-t">
                {Object.entries(user.coursesStatistic).map(([courseId, modules]) => (
                  <div key={courseId} className="space-y-4">
                    {modules.map((module) => (
                      <div key={module.moduleId} className="bg-gray-50 rounded-lg p-4">
                        {/* Заголовок модуля */}
                        <div
                          className="cursor-pointer flex justify-between items-center"
                          onClick={() => toggleSection('module', `${user.id}-${module.moduleId}`)}
                        >
                          <div>
                            <h3 className="font-medium">
                              Модуль #{module.moduleId}
                            </h3>
                            <span className={`text-sm ${
                              module.isModuleCompleted 
                                ? "text-green-600" 
                                : "text-yellow-600"
                            }`}>
                              {module.isModuleCompleted ? 'Завершён' : 'В процессе'}
                            </span>
                          </div>
                          <span className="text-xl">
                            {expandedSections.modules[user.id]?.[module.moduleId] ? '−' : '+'}
                          </span>
                        </div>

                        {/* Детали модуля */}
                        {expandedSections.modules[user.id]?.[module.moduleId] && (
                          <div className="mt-4 space-y-3">
                            <div className="grid grid-cols-2 gap-4">
                              <div>
                                <p className="text-sm text-gray-500">Прогресс</p>
                                <div className="w-full bg-gray-200 rounded-full h-2">
                                  <div 
                                    className="bg-blue-500 rounded-full h-2" 
                                    style={{ width: `${module.moduleCompletionPercentage}%` }}
                                  />
                                </div>
                                <p className="text-sm mt-1">
                                  {module.moduleCompletionPercentage}%
                                </p>
                              </div>
                              <div>
                                <p className="text-sm text-gray-500">Последняя активность</p>
                                <p className="text-sm">
                                  {module.lastActivity ? formatDate(module.lastActivity) : 'Нет данных'}
                                </p>
                              </div>
                            </div>

                            {module.testResults?.length > 0 && (
                              <div className="mt-4">
                                <h4 className="font-medium mb-2">Результаты тестов:</h4>
                                <div className="space-y-2">
                                  {module.testResults.map((test, i) => (
                                    <div key={i} className="bg-white p-3 rounded shadow">
                                      <div className="flex justify-between items-center mb-2">
                                        <div>
                                          <span className="font-medium">Попытка #{test.attemptNumber}</span>
                                          <span className="text-sm text-gray-500 ml-2">
                                            {formatDate(test.timestamp)}
                                          </span>
                                        </div>
                                        <span className={`px-2 py-1 rounded ${
                                          test.score >= 80 ? 'bg-green-100 text-green-800' :
                                          test.score >= 60 ? 'bg-yellow-100 text-yellow-800' : 'bg-red-100 text-red-800'
                                        }`}>
                                          {test.score}%
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
                                          <span>
                                            {test.totalQuestionsCount} вопросов
                                          </span>
                                        </div>
                                      </div>
                                    </div>
                                  ))}
                                </div>
                              </div>
                            )}
                          </div>
                        )}
                      </div>
                    ))}
                  </div>
                ))}
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
}