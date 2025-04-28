import {useState, useEffect, useRef} from 'react';
import QuestionCard from "../components/testForm/QuestionCard";
import TestResults from "../components/testForm/TestResults";
import Header from "../components/header/Header";

const testData = [
    {
        question: "Какой тег используется для создания ссылки в HTML?",
        options: ["<link>", "<a>", "<href>", "<url>"],
        correctAnswer: 1
    },
    {
        question: "Какой метод используется для обновления состояния в React?",
        options: ["setState", "updateState", "changeState", "modifyState"],
        correctAnswer: 0
    },
    {
        question: "Какой хук React используется для создания состояния?",
        options: ["useEffect", "useContext", "useReducer", "useState"],
        correctAnswer: 3
    }
];

const TestFormPage = () => {
    const [selectedAnswers, setSelectedAnswers] = useState({});
    const [submitted, setSubmitted] = useState(false);
    const [submitAttempted, setSubmitAttempted] = useState(false);
    const resultsRef = useRef(null);

    const allQuestionsAnswered = testData.every(
        (_, index) => selectedAnswers.hasOwnProperty(index)
    );

    const handleSelectAnswer = (questionIndex, answerIndex) => {
        if (!submitted) {
            setSelectedAnswers(prev => ({
                ...prev,
                [questionIndex]: answerIndex
            }));
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (allQuestionsAnswered) {
            setSubmitted(true);
        }
        setSubmitAttempted(true);
    };

    useEffect(() => {
        if (allQuestionsAnswered) {
            setSubmitAttempted(false);
        }
    }, [selectedAnswers]);

    useEffect(() => {
        if (submitted && resultsRef.current) {
            resultsRef.current.scrollIntoView({ behavior: 'smooth' });
        }
    }, [submitted]);

    const calculateScore = () => testData.reduce((acc, q, i) =>
        acc + (selectedAnswers[i] === q.correctAnswer ? 1 : 0), 0
    );

    return (
        <>
            <Header backgroundColor="bg-[#d7defc]"/>
            <div className="flex flex-col items-center min-h-screen bg-[#d7defc] p-6  pt-16">
                <form onSubmit={handleSubmit} className="w-full max-w-2xl">
                    {testData.map((question, qIndex) => (
                        <QuestionCard
                            key={qIndex}
                            question={question}
                            qIndex={qIndex}
                            selectedAnswer={selectedAnswers[qIndex]}
                            submitted={submitted}
                            submitAttempted={submitAttempted}
                            onAnswerSelect={handleSelectAnswer}
                        />
                    ))}

                    {!submitted && (
                        <button
                            type="submit"
                            disabled={!allQuestionsAnswered}
                            className={`w-full text-white py-3 px-6 rounded-xl text-lg font-bold ${
                                allQuestionsAnswered
                                    ? 'bg-[#89d018] hover:bg-[#7abb15]'
                                    : 'bg-gray-400 cursor-not-allowed'
                            }`}
                        >
                            Проверить ответы
                        </button>
                    )}
                </form>

                {submitted && (
                    <TestResults
                        ref={resultsRef}
                        score={calculateScore()}
                        totalQuestions={testData.length}
                        onRetry={() => window.location.reload()}
                    />
                )}
            </div>
        </>
    );
};

export default TestFormPage;