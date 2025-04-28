// UsersPage.js
import { useState, useEffect } from "react";
import UserList from "../components/User/UserList";
import { getUsers } from "../api/adminApi";

export default function UsersPage() {
  const [users, setUsers] = useState([]);
  const [sortField, setSortField] = useState(null);
  const [sortDirection, setSortDirection] = useState("asc");

  useEffect(() => {
    const fetchUsers = async () => {
      const usersData = await getUsers();
      setUsers(usersData);
    };

    fetchUsers();
  }, []);

  const handleSort = (field) => {
    if (field === sortField) {
      setSortDirection(sortDirection === "asc" ? "desc" : "asc");
    } else {
      setSortField(field);
      setSortDirection("asc");
    }
  };

  const sortedUsers = [...users].sort((a, b) => {
    if (!sortField) return 0;
    if (typeof a[sortField] === "string") {
      const comparison = a[sortField].localeCompare(b[sortField]);
      return sortDirection === "asc" ? comparison : -comparison;
    } else {
      const comparison = a[sortField] - b[sortField];
      return sortDirection === "asc" ? comparison : -comparison;
    }
  });

  return (
    <div className="container mx-auto px-4 py-6">
      <h1 className="text-2xl font-bold mb-4">Пользователи</h1>
      <UserList
        users={sortedUsers}
        onSort={handleSort}
        sortField={sortField}
        sortDirection={sortDirection}
      />
    </div>
  );
}
