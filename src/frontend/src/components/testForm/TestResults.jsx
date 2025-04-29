import { forwardRef } from 'react';

const TestResults = forwardRef(({ score, totalQuestions, onRetry, isSuccess, onExit }, ref) => (
    <div
        ref={ref}
        className="bg-white p-6 rounded-xl shadow-sm text-center"
    >
        <h3 className="text-2xl font-bold mb-4">
            Результаты теста
        </h3>

        {isSuccess ? (
            <p className="mb-4 text-green-600 font-semibold">
                Поздравляем! Вы прошли тест, ответив правильно на {score} из {totalQuestions} вопросов.
            </p>
        ) : (
            <p className="mb-4 text-red-600 font-semibold">
                Вы ответили правильно на {score} из {totalQuestions} вопросов. Чтобы пройти тест, нужно набрать не менее
                80% правильных ответов.
            </p>
        )}

        {isSuccess ? (
            <button
                onClick={onExit}
                className="bg-[#0793fe] text-white py-2 px-6 rounded-lg hover:bg-[#0678cf] transition-colors"
            >
                К новому модулю
            </button>
        ) : (
            <button
                onClick={onRetry}
                className="bg-[#0793fe] text-white py-2 px-6 rounded-lg hover:bg-[#0678cf] transition-colors"
            >
                Попробовать снова
            </button>
        )}
    </div>


));

export default TestResults;