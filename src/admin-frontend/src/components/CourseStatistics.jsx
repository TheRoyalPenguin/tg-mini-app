import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";

export default function CourseStatistics() {
  const { courseId } = useParams();
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

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

  if (loading) return <div className="p-4 text-gray-600">Загрузка статистики...</div>;
  if (error) return <div className="p-4 text-red-500">{error}</div>;
  if (!stats?.length) return <div className="p-4">Данные не найдены</div>;

  // Агрегируем данные
  const totalUsers = stats.length;
  const activeUsers = stats.filter(u => !u.isBanned).length;
  const bannedUsers = stats.filter(u => u.isBanned).length;

  return (
    <div className="container mx-auto px-4 py-6">
      <div className="bg-white rounded-xl shadow-lg p-6">
        <h1 className="text-3xl font-bold text-gray-800 mb-6">Статистика курса #{courseId}</h1>
        
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
          <div className="bg-blue-50 p-4 rounded-lg">
            <p className="text-sm text-blue-600">Всего пользователей</p>
            <p className="text-2xl font-bold">{totalUsers}</p>
          </div>
          
          <div className="bg-green-50 p-4 rounded-lg">
            <p className="text-sm text-green-600">Активных пользователей</p>
            <p className="text-2xl font-bold">{activeUsers}</p>
          </div>
          
          <div className="bg-red-50 p-4 rounded-lg">
            <p className="text-sm text-red-600">Заблокированных</p>
            <p className="text-2xl font-bold">{bannedUsers}</p>
          </div>
        </div>
      </div>
    </div>
  );
}