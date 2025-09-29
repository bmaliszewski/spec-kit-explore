import { render, screen } from '@testing-library/react';
import Dropdown from './Dropdown';

test('renders dropdown with label', () => {
  const options = [
    { value: '1', label: 'Option 1' },
    { value: '2', label: 'Option 2' },
  ];
  render(<Dropdown label="Test Dropdown" options={options} />);
  const labelElement = screen.getByText(/Test Dropdown/i);
  expect(labelElement).toBeInTheDocument();
});
