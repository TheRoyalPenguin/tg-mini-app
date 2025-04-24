import { forwardRef } from 'react';

const TestResults = forwardRef(({ score, totalQuestions, onRetry }, ref) => (
    <div
        ref={ref}
        className="bg-white p-6 rounded-xl shadow-sm text-center"
    >
        <h3 className="text-2xl font-bold mb-4">
            Результаты теста: {score} из {totalQuestions}
        </h3>
        <button
            onClick={onRetry}
            className="bg-[#0793fe] text-white py-2 px-6 rounded-lg hover:bg-[#0678cf] transition-colors"
        >
            Попробовать снова
        </button>
    </div>
));

export default TestResults;