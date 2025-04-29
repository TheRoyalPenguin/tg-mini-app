// UserRow.js
import { banUser, unbanUser } from "../../api/adminApi";
import { useState } from "react";
import UserStatisticsModal from "./UserStatisticsModal";

export default function UserRow({ user }) {
  const [isBanned, setIsBanned] = useState(user.isBanned);
  const [showStats, setShowStats] = useState(false);

  const handleBan = async () => {
    try {
      await banUser(user.id);
      setIsBanned(true);
    } catch (error) {
      console.error("Ошибка при бане пользователя", error);
    }
  };

  const handleUnban = async () => {
    try {
      await unbanUser(user.id);
      setIsBanned(false);
    } catch (error) {
      console.error("Ошибка при разбане пользователя", error);
    }
  };

  return (
    <>
      <tr className="border-b hover:bg-gray-50">
        <td className="p-2">{user.id}</td>
        <td className="p-2">{user.tgId}</td>
        <td className="p-2">{user.name}</td>
        <td className="p-2">{user.surname}</td>
        <td className="p-2 space-x-2">
          <button
            onClick={() => setShowStats(true)}
            className="bg-blue-500 hover:bg-blue-600 text-white py-1 px-3 rounded"
          >
            Статистика
          </button>
          {isBanned ? (
            <button
              onClick={handleUnban}
              className="bg-green-500 hover:bg-green-600 text-white py-1 px-3 rounded"
            >
              Разбанить
            </button>
          ) : (
            <button
              onClick={handleBan}
              className="bg-red-500 hover:bg-red-600 text-white py-1 px-3 rounded"
            >
              Забанить
            </button>
          )}
        </td>
      </tr>
      
      <UserStatisticsModal 
        userId={user.id}
        isOpen={showStats}
        onClose={() => setShowStats(false)}
      />
    </>
  );
}