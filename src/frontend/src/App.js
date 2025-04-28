import {BrowserRouter as Router, Routes, Route } from "react-router-dom";
import React from 'react';
import CoursePage from './pages/CoursePage';
import AuthPage from './pages/AuthPage';
import WelcomePage from './pages/WelcomePage';
import TestFormPage from "./pages/TestFormPage";
import ModulePage from "./pages/ModulePage";
import FAQPage from "./pages/FAQPage";
import SupportPage from "./pages/SupportPage";
import ProfilePage from "./pages/ProfilePage";
import LongreadPage from "./pages/LongreadPage";

function App() {
  return (
      <Router>
          <Routes>
              <Route path="/" element={<AuthPage />} />
              <Route path="/courses" element={<WelcomePage />} />
              <Route path="/courses/:courseId" element={<CoursePage />} />
              <Route path="/courses/:courseId/tests/:moduleId" element={<TestFormPage />} />
              <Route path="/courses/:courseId/modules/:moduleId" element={<ModulePage />} />
              <Route path="/faq" element={<FAQPage/>} />
              <Route path="/support" element={<SupportPage/>} />
              <Route path="/profile/:userId" element={<ProfilePage />} />
              <Route path="/longreads/:longreadId" element={<LongreadPage />} />
          </Routes>
      </Router>
  );
}

export default App;
