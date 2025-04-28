// UserList.js
import UserRow from "./UserRow";
import SortIndicator from "./SortIndicator";

export default function UserList({ users, onSort, sortField, sortDirection }) {
  const columns = [
    { field: 'id', label: 'ID' },
    { field: 'tgId', label: 'Telegram ID' },
    { field: 'name', label: 'Имя' },
    { field: 'surname', label: 'Фамилия' },
    { field: 'actions', label: 'Действия' }
  ];

  return (
    <table className="min-w-full bg-white shadow-md rounded-lg overflow-hidden">
      <thead>
        <tr className="bg-gray-200">
          {columns.map((col) => (
            <th 
              key={col.field}
              className="p-2 text-left cursor-pointer hover:bg-gray-300"
              onClick={() => col.field !== 'actions' && onSort(col.field)}
            >
              <div className="flex items-center">
                {col.label}
                {sortField === col.field && <SortIndicator direction={sortDirection} />}
              </div>
            </th>
          ))}
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