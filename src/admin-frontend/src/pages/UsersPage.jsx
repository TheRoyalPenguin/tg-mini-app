import { useState, useEffect } from "react";
import UserList from "../components/User/UserList";

export default function UsersPage() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    // Заменить на свой реальный запрос к API
    const fetchUsers = async () => {
      // Здесь пока тестовые данные
      setUsers([
        { id: 1, tgId: 123456789, name: "Иван", surname: "Иванов" },
        { id: 2, tgId: 987654321, name: "Петр", surname: "Петров" },
      ]);
    };

    fetchUsers();
  }, []);

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Пользователи</h1>
      <UserList users={users} />
    </div>
  );
}
