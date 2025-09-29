import { render, screen } from '@testing-library/react';
import Spinner from './Spinner';

test('renders spinner', () => {
  render(<Spinner />);
  const spinnerElement = screen.getByRole('progressbar'); // Assuming spinner has role progressbar
  expect(spinnerElement).toBeInTheDocument();
});
