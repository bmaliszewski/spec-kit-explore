import api from './api';

const authService = {
  login: async (email, password) => {
    const response = await api.post('/Auth/Login', { email, password });
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
    }
    return response.data;
  },

  register: async (firstName, lastName, email, password, personalData, rodoConsents) => {
    const response = await api.post('/Auth/Register', { firstName, lastName, email, password, personalData, rodoConsents });
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('token');
  },

  getCurrentUser: () => {
    const token = localStorage.getItem('token');
    if (token) {
      // In a real app, you'd decode the token or make an API call to validate/get user info
      return { token };
    }
    return null;
  },
};

export default authService;
