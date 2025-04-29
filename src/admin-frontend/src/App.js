// App.js
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout/Layout';
import UsersPage from './pages/UsersPage';
import CoursesPage from './pages/CoursesPage';
import CourseStatistics from './components/CourseStatistics';
import TestsPage from './pages/TestsPage';
import LongreadsPage from './pages/LongreadsPage';
import BooksPage from './pages/BooksPage';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/users" element={<UsersPage />} />
          <Route path="/statistics" element={<CoursesPage />} />
          <Route path="/statistics/courses/:courseId" element={<CourseStatistics />} />
          <Route path="/" element={<UsersPage />} />
          <Route path="/resources/tests" element={<TestsPage />} />
          <Route path="/resources/longreads" element={<LongreadsPage />} />
          <Route path="/resources/books" element={<BooksPage />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;