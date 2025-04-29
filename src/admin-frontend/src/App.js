import { BrowserRouter as Router, Route, Routes, Navigate, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import Layout from './components/Layout/Layout';
import UsersPage from './pages/UsersPage';
import CoursesPage from './pages/CoursesPage';
import CourseStatistics from './components/CourseStatistics';
import TestsPage from './pages/TestsPage';
import LongreadsPage from './pages/LongreadsPage';
import BooksPage from './pages/BooksPage';

// Компонент страницы входа
function LoginPage() {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = (e) => {
    e.preventDefault();
    // Любые введенные данные считаются валидными
    if (login.trim() !== '' || password.trim() !== '') {
      localStorage.setItem('isAuthenticated', 'true');
      navigate('/users');
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="w-full max-w-md p-8 space-y-8 bg-white rounded-lg shadow">
        <h2 className="text-2xl font-bold text-center">Вход в админку</h2>
        <form className="mt-8 space-y-6" onSubmit={handleLogin}>
          <div className="space-y-4">
            <div>
              <label htmlFor="login" className="block text-sm font-medium text-gray-700">
                Логин
              </label>
              <input
                id="login"
                type="text"
                value={login}
                onChange={(e) => setLogin(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-md"
              />
            </div>
            <div>
              <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                Пароль
              </label>
              <input
                id="password"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="w-full px-3 py-2 border border-gray-300 rounded-md"
              />
            </div>
          </div>
          <div>
            <button
              type="submit"
              className="w-full px-4 py-2 text-white bg-blue-600 rounded-md hover:bg-blue-700"
            >
              Войти
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

// Компонент-обертка для проверки авторизации
function PrivateRoute({ children }) {
  const isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';
  return isAuthenticated ? children : <Navigate to="/login" />;
}

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route
          path="/*"
          element={
            <PrivateRoute>
              <Layout>
                <Routes>
                  <Route path="/users" element={<UsersPage />} />
                  <Route path="/statistics" element={<CoursesPage />} />
                  <Route path="/statistics/courses/:courseId" element={<CourseStatistics />} />
                  <Route path="/" element={<Navigate to="/users" />} />
                  <Route path="/resources/tests" element={<TestsPage />} />
                  <Route path="/resources/longreads" element={<LongreadsPage />} />
                  <Route path="/resources/books" element={<BooksPage />} />
                </Routes>
              </Layout>
            </PrivateRoute>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;