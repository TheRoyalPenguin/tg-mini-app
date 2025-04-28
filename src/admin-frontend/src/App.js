import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Sidebar from "./components/Layout/Sidebar";
import UserStatistics from "./components/User/UserStatistics";
import UsersPage from "./pages/UsersPage"; 

function App() {
  return (
    <Router>
      <div className="flex">
        <Sidebar />
        <div className="flex-1 p-6 overflow-auto">
          <Routes>
            <Route path="/users" element={<UsersPage />} />
            <Route path="/statistics/:userId" element={<UserStatistics />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
