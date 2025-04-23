import {BrowserRouter as Router, Routes, Route } from "react-router-dom";
import React from 'react';
import CoursePage from './pages/CoursePage';
import AuthPage from './pages/AuthPage';
import WelcomePage from './pages/WelcomePage';
import TestFormPage from "./pages/TestFormPage";

function App() {
  return (
      <Router>
          <Routes>
              <Route path="/" element={<AuthPage />} />
              <Route path="/courses" element={<WelcomePage />} />
              <Route path="/courses/:courseId" element={<CoursePage />} />
              <Route path="/test" element={<TestFormPage />} />
          </Routes>
      </Router>
  );
}

export default App;
