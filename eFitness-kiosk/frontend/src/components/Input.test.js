import { render, screen } from '@testing-library/react';
import Input from './Input';

test('renders input with label', () => {
  render(<Input label="Test Label" />);
  const labelElement = screen.getByText(/Test Label/i);
  expect(labelElement).toBeInTheDocument();
});
