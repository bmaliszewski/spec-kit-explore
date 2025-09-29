import api from './api';

const passService = {
  getAvailablePasses: async () => {
    const response = await api.get('/Passes');
    return response.data;
  },
};

export default passService;
