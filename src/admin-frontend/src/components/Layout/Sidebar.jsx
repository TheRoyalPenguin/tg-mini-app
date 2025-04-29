import { Link } from "react-router-dom";

export default function Sidebar() {
  return (
    <div className="w-64 h-screen bg-gray-800 text-white flex flex-col p-4 space-y-2 shadow-xl fixed left-0 top-0">
      <div className="flex flex-col justify-between h-full">
        <div>
          <div className="mb-8 mt-4 px-2">
            <h2 className="text-xl font-bold text-gray-200">Админ-панель</h2>
          </div>
          <Link
            to="/users"
            className="flex items-center px-4 py-3 rounded-lg hover:bg-gray-700 transition-all duration-200 group"
          >
            <svg className="w-5 h-5 mr-3 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"/>
            </svg>
            <span className="font-medium text-gray-300 group-hover:text-white">Пользователи</span>
          </Link>
          <Link
            to="/statistics"
            className="flex items-center px-4 py-3 rounded-lg hover:bg-gray-700 transition-all duration-200 group"
          >
            <svg className="w-5 h-5 mr-3 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z"/>
            </svg>
            <span className="font-medium text-gray-300 group-hover:text-white">Статистика</span>
          </Link>
          <div className="mt-4">
            <h3 className="text-lg font-semibold text-gray-300 px-2">Обучающие ресурсы</h3>
            <Link
              to="/resources/tests"
              className="flex items-center px-4 py-3 rounded-lg hover:bg-gray-700 transition-all duration-200 group"
            >
              <svg className="w-5 h-5 mr-3 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8c1.654 0 3-1.346 3-3s-1.346-3-3-3-3 1.346-3 3 1.346 3 3 3zm0 2c-2.21 0-4 1.79-4 4v2h2c0-1.103.897-2 2-2v-2h2v2c1.103 0 2 .897 2 2h2v-2c0 2.21-1.79 4-4 4zm0 6c-1.654 0-3 1.346-3 3s1.346 3 3 3 3-1.346 3-3-1.346-3-3-3z"/>
              </svg>
              <span className="font-medium text-gray-300 group-hover:text-white">Тесты</span>
            </Link>
            <Link
              to="/resources/longreads"
              className="flex items-center px-4 py-3 rounded-lg hover:bg-gray-700 transition-all duration-200 group"
            >
              <svg className="w-5 h-5 mr-3 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8c1.654 0 3-1.346 3-3s-1.346-3-3-3-3 1.346-3 3 1.346 3 3 3zm0 2c-2.21 0-4 1.79-4 4v2h2c0-1.103.897-2 2-2v-2h2v2c1.103 0 2 .897 2 2h2v-2c0 2.21-1.79 4-4 4zm0 6c-1.654 0-3 1.346-3 3s1.346 3 3 3 3-1.346 3-3-1.346-3-3-3z"/>
              </svg>
              <span className="font-medium text-gray-300 group-hover:text-white">Лонгриды</span>
            </Link>
            <Link
              to="/resources/books"
              className="flex items-center px-4 py-3 rounded-lg hover:bg-gray-700 transition-all duration-200 group"
            >
              <svg className="w-5 h-5 mr-3 text-gray-400 group-hover:text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8c1.654 0 3-1.346 3-3s-1.346-3-3-3-3 1.346-3 3 1.346 3 3 3zm0 2c-2.21 0-4 1.79-4 4v2h2c0-1.103.897-2 2-2v-2h2v2c1.103 0 2 .897 2 2h2v-2c0 2.21-1.79 4-4 4zm0 6c-1.654 0-3 1.346-3 3s1.346 3 3 3 3-1.346 3-3-1.346-3-3-3z"/>
              </svg>
              <span className="font-medium text-gray-300 group-hover:text-white">Книги</span>
            </Link>
          </div>
        </div>
        <div className="pt-4 mt-4 border-t border-gray-700">
          <p className="px-2 text-sm text-gray-400">Версия 1.0.0</p>
        </div>
      </div>
    </div>
  );
}
