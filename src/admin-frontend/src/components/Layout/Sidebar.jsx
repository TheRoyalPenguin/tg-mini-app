import { Link } from "react-router-dom";

export default function Sidebar() {
  return (
    <div className="w-64 h-full bg-gray-800 text-white flex flex-col p-4">
      <Link to="/users" className="mb-2 hover:bg-gray-700 p-2 rounded">
        Пользователи
      </Link>
    </div>
  );
}