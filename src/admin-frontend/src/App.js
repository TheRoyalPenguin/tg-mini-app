import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout/Layout";
import UsersPage from "./pages/UsersPage";

export default function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/users" element={<UsersPage />} />
          {/* В будущем другие страницы */}
        </Routes>
      </Layout>
    </Router>
  );
}
