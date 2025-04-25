import {BrowserRouter as Router, Routes, Route } from "react-router-dom";
import React from 'react';
import CoursePage from './pages/CoursePage';
import AuthPage from './pages/AuthPage';
import WelcomePage from './pages/WelcomePage';
import TestFormPage from "./pages/TestFormPage";
import ModulePage from "./pages/ModulePage";

function App() {
  return (
      <Router>
          <Routes>
              <Route path="/" element={<AuthPage />} />
              <Route path="/courses" element={<WelcomePage />} />
              <Route path="/courses/:courseId" element={<CoursePage />} />
              <Route path="/tests/:testId" element={<TestFormPage />} />
              <Route path="/modules/:moduleId" element={<ModulePage />} />
          </Routes>
      </Router>
  );
}

export default App;
