import { useEffect, useRef, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import authService from '../services/authService';

const INACTIVITY_TIMEOUT = 60 * 1000; // 1 minute in milliseconds

const useInactivityLogout = () => {
  const navigate = useNavigate();
  const timeoutRef = useRef(null);

  const resetTimer = useCallback(() => {
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
    }
    timeoutRef.current = setTimeout(() => {
      authService.logout();
      navigate('/login');
      alert('You have been logged out due to inactivity.');
    }, INACTIVITY_TIMEOUT);
  }, [navigate]);

  const handleActivity = useCallback(() => {
    resetTimer();
  }, [resetTimer]);

  useEffect(() => {
    // Start timer on component mount
    resetTimer();

    // Attach event listeners for user activity
    window.addEventListener('mousemove', handleActivity);
    window.addEventListener('keydown', handleActivity);
    window.addEventListener('click', handleActivity);
    window.addEventListener('scroll', handleActivity);

    // Clean up event listeners and timer on component unmount
    return () => {
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current);
      }
      window.removeEventListener('mousemove', handleActivity);
      window.removeEventListener('keydown', handleActivity);
      window.removeEventListener('click', handleActivity);
      window.removeEventListener('scroll', handleActivity);
    };
  }, [handleActivity, resetTimer]);

  return null; // This hook doesn't render anything
};

export default useInactivityLogout;
