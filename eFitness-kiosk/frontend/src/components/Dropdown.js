import React from 'react';
import './Dropdown.css';

const Dropdown = ({ label, options, value, onChange, className = '' }) => {
  return (
    <div className="dropdown-group">
      {label && <label>{label}</label>}
      <select value={value} onChange={onChange} className={`dropdown ${className}`}>
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
    </div>
  );
};

export default Dropdown;
