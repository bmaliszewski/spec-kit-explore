import React, { useState, useEffect } from 'react';
import passService from '../services/passService';
import paymentService from '../services/paymentService';
import Button from '../components/Button';
import Dropdown from '../components/Dropdown';

const PassesPage = () => {
  const [passes, setPasses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [selectedPassId, setSelectedPassId] = useState('');
  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState('card');

  useEffect(() => {
    const fetchPasses = async () => {
      try {
        const availablePasses = await passService.getAvailablePasses();
        setPasses(availablePasses);
        if (availablePasses.length > 0) {
          setSelectedPassId(availablePasses[0].id);
        }
      } catch (err) {
        setError('Failed to fetch passes');
      } finally {
        setLoading(false);
      }
    };

    fetchPasses();
  }, []);

  const handleBuyPass = async () => {
    if (!selectedPassId || !selectedPaymentMethod) {
      setError('Please select a pass and payment method.');
      return;
    }
    try {
      const response = await paymentService.initiatePayment(selectedPassId, selectedPaymentMethod);
      window.location.href = response.redirectUrl; // Redirect to payment gateway
    } catch (err) {
      setError('Failed to initiate payment');
    }
  };

  if (loading) {
    return <div>Loading passes...</div>; // Replace with Spinner component
  }

  if (error) {
    return <div className="error-message">{error}</div>;
  }

  const paymentMethodOptions = [
    { value: 'card', label: 'Karta płatnicza' },
    { value: 'transfer', label: 'Szybki przelew' },
  ];

  return (
    <div className="passes-page">
      <h2>Available Passes</h2>
      {passes.length === 0 ? (
        <p>No passes available.</p>
      ) : (
        <div>
          <Dropdown
            label="Select a Pass"
            options={passes.map(p => ({ value: p.id, label: `${p.name} - ${p.price} PLN` }))}
            value={selectedPassId}
            onChange={(e) => setSelectedPassId(e.target.value)}
          />
          <Dropdown
            label="Select Payment Method"
            options={paymentMethodOptions}
            value={selectedPaymentMethod}
            onChange={(e) => setSelectedPaymentMethod(e.target.value)}
          />
          <Button onClick={handleBuyPass}>Buy Pass</Button>
        </div>
      )}
    </div>
  );
};

export default PassesPage;
