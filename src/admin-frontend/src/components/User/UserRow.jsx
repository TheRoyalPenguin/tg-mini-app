// UserRow.js
import { banUser, unbanUser } from "../../api/adminApi";
import { useState } from "react";

export default function UserRow({ user }) {
  const [isBanned, setIsBanned] = useState(user.isBanned);

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
    <tr className="border-b">
      <td className="p-2">{user.id}</td>
      <td className="p-2">{user.tgId}</td>
      <td className="p-2">{user.name}</td>
      <td className="p-2">{user.surname}</td>
      <td className="p-2 space-x-2">
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
  );
}
