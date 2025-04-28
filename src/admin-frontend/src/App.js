// App.js
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout/Layout';
import UsersPage from './pages/UsersPage';
import CoursesPage from './pages/CoursesPage';
import CourseStatistics from './components/CourseStatistics';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/users" element={<UsersPage />} />
          <Route path="/statistics" element={<CoursesPage />} />
          <Route path="/statistics/courses/:courseId" element={<CourseStatistics />} />
          <Route path="/" element={<UsersPage />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;