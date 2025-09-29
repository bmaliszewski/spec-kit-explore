import React from 'react';
import './Input.css';

const Input = ({ label, type = 'text', value, onChange, className = '' }) => {
  return (
    <div className="input-group">
      {label && <label>{label}</label>}
      <input type={type} value={value} onChange={onChange} className={`input ${className}`} />
    </div>
  );
};

export default Input;
