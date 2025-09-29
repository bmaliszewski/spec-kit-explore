import api from './api';

const userService = {
  getCurrentUser: async () => {
    const response = await api.get('/Users/Me');
    return response.data;
  },

  updateUser: async (userData) => {
    const response = await api.put('/Users/Me', userData);
    return response.data;
  },
};

export default userService;
