import {BrowserRouter as Router, Routes, Route } from "react-router-dom";
import React from 'react';
import AuthPage from './pages/TestPage';
import TestPage from "./pages/TestPage";


function App() {
  return (
      <Router>
          <Routes>
              <Route path="/" element={<TestPage />} />
          </Routes>
      </Router>
  );
}

export default App;
