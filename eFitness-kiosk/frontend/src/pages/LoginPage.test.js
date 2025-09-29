import { render, screen, fireEvent } from '@testing-library/react';
import { BrowserRouter as Router } from 'react-router-dom';
import LoginPage from './LoginPage';
import authService from '../services/authService';

// Mock authService
jest.mock('../services/authService', () => ({
  login: jest.fn(),
  logout: jest.fn(),
}));

describe('LoginPage', () => {
  test('renders login form', () => {
    render(
      <Router>
        <LoginPage />
      </Router>
    );
    expect(screen.getByLabelText(/Email/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Password/i)).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /Login/i })).toBeInTheDocument();
  });

  test('allows user to login', async () => {
    authService.login.mockResolvedValueOnce({ token: 'fake-token' });

    render(
      <Router>
        <LoginPage />
      </Router>
    );

    fireEvent.change(screen.getByLabelText(/Email/i), { target: { value: 'test@example.com' } });
    fireEvent.change(screen.getByLabelText(/Password/i), { target: { value: 'password' } });
    fireEvent.click(screen.getByRole('button', { name: /Login/i }));

    expect(authService.login).toHaveBeenCalledWith('test@example.com', 'password');
    // Further assertions for navigation would require mocking useNavigate
  });

  test('displays error message on failed login', async () => {
    authService.login.mockRejectedValueOnce(new Error('Login failed'));

    render(
      <Router>
        <LoginPage />
      </Router>
    );

    fireEvent.change(screen.getByLabelText(/Email/i), { target: { value: 'test@example.com' } });
    fireEvent.change(screen.getByLabelText(/Password/i), { target: { value: 'wrongpassword' } });
    fireEvent.click(screen.getByRole('button', { name: /Login/i }));

    expect(await screen.findByText(/Invalid credentials/i)).toBeInTheDocument();
  });
});
