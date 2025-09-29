import api from './api';

const paymentService = {
  initiatePayment: async (passId, paymentMethod) => {
    const response = await api.post(`/Payments/Passes/${passId}/Initiate`, { paymentMethod });
    return response.data;
  },

  getPaymentHistory: async () => {
    const response = await api.get('/Payments/Me');
    return response.data;
  },
};

export default paymentService;
