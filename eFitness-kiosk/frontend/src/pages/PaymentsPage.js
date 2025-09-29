import React, { useState, useEffect } from 'react';
import paymentService from '../services/paymentService';

const PaymentsPage = () => {
  const [payments, setPayments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchPayments = async () => {
      try {
        const history = await paymentService.getPaymentHistory();
        setPayments(history);
      } catch (err) {
        setError('Failed to fetch payment history');
      } finally {
        setLoading(false);
      }
    };

    fetchPayments();
  }, []);

  if (loading) {
    return <div>Loading payment history...</div>; // Replace with Spinner component
  }

  if (error) {
    return <div className="error-message">{error}</div>;
  }

  return (
    <div className="payments-page">
      <h2>Payment History</h2>
      {payments.length === 0 ? (
        <p>No payment history found.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Amount</th>
              <th>Date</th>
              <th>Status</th>
              <th>Method</th>
            </tr>
          </thead>
          <tbody>
            {payments.map((payment) => (
              <tr key={payment.id}>
                <td>{payment.id}</td>
                <td>{payment.amount}</td>
                <td>{new Date(payment.date).toLocaleDateString()}</td>
                <td>{payment.status}</td>
                <td>{payment.paymentMethod}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default PaymentsPage;
