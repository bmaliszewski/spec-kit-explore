import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ProfilePage from './pages/ProfilePage';
import PassesPage from './pages/PassesPage';
import PaymentsPage from './pages/PaymentsPage';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <nav>
          <ul>
            <li>
              <Link to="/login">Login</Link>
            </li>
            <li>
              <Link to="/register">Register</Link>
            </li>
            <li>
              <Link to="/profile">Profile</Link>
            </li>
            <li>
              <Link to="/passes">Passes</Link>
            </li>
            <li>
              <Link to="/payments">Payments</Link>
            </li>
          </ul>
        </nav>

        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/profile" element={<ProfilePage />} />
          <Route path="/passes" element={<PassesPage />} />
          <Route path="/payments" element={<PaymentsPage />} />
          <Route path="*" element={<LoginPage />} /> {/* Default route */}
        </Routes>
      </div>
    </Router>
  );
}

export default App;