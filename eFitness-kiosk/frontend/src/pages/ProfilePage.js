import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import userService from '../services/userService';
import authService from '../services/authService';
import Input from '../components/Input';
import Button from '../components/Button';

const ProfilePage = () => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const currentUser = await userService.getCurrentUser();
        setUser(currentUser);
      } catch (err) {
        setError('Failed to fetch user data');
        authService.logout();
        navigate('/login');
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, [navigate]);

  const handleUpdate = async (e) => {
    e.preventDefault();
    try {
      await userService.updateUser(user);
      alert('Profile updated successfully!');
    } catch (err) {
      setError('Failed to update profile');
    }
  };

  if (loading) {
    return <div>Loading profile...</div>; // Replace with Spinner component
  }

  if (error) {
    return <div className="error-message">{error}</div>;
  }

  return (
    <div className="profile-page">
      <h2>User Profile</h2>
      <form onSubmit={handleUpdate}>
        <Input
          label="First Name"
          type="text"
          value={user.firstName}
          onChange={(e) => setUser({ ...user, firstName: e.target.value })}
        />
        <Input
          label="Last Name"
          type="text"
          value={user.lastName}
          onChange={(e) => setUser({ ...user, lastName: e.target.value })}
        />
        <Input
          label="Email"
          type="email"
          value={user.email}
          onChange={(e) => setUser({ ...user, email: e.target.value })}
          disabled // Email usually not editable directly
        />
        {/* Add more fields for personalData and RodoConsents as needed */}
        <Button type="submit">Update Profile</Button>
      </form>
      <Button onClick={() => {
        authService.logout();
        navigate('/login');
      }}>Logout</Button>
    </div>
  );
};

export default ProfilePage;
