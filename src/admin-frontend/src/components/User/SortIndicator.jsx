// SortIndicator.js
export default function SortIndicator({ direction }) {
    return (
      <span className="ml-2">
        {direction === 'asc' ? '↑' : '↓'}
      </span>
    );
  }