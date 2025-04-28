// UserList.js
import { Link } from "react-router-dom";
import UserRow from "./UserRow";

export default function UserList({ users }) {
  return (
    <table className="min-w-full bg-white shadow-md rounded-lg overflow-hidden">
      <thead>
        <tr className="bg-gray-200">
          <th className="p-2 text-left">ID</th>
          <th className="p-2 text-left">Telegram ID</th>
          <th className="p-2 text-left">Имя</th>
          <th className="p-2 text-left">Фамилия</th>
          <th className="p-2 text-left">Действия</th>
        </tr>
      </thead>
      <tbody>
        {users.map((user) => (
          <UserRow key={user.id} user={user} />
        ))}
      </tbody>
    </table>
  );
}
