import { render, screen } from '@testing-library/react';
import Button from './Button';

test('renders button with children', () => {
  render(<Button>Test Button</Button>);
  const buttonElement = screen.getByText(/Test Button/i);
  expect(buttonElement).toBeInTheDocument();
});
