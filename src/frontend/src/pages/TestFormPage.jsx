import { useState, useEffect, useRef } from 'react';
import QuestionCard from "../components/testForm/QuestionCard";
import TestResults from "../components/testForm/TestResults";
import Header from "../components/header/Header";
import { getTest } from "../services/getTest";
import axiosInstance from "../axiosInstance";
import {useParams} from "react-router-dom";

const TestFormPage = () => {
    const [testData, setTestData] = useState([]);
    const [selectedAnswers, setSelectedAnswers] = useState({});
    const [submitted, setSubmitted] = useState(false);
    const [submitAttempted, setSubmitAttempted] = useState(false);
    const [correctness, setCorrectness] = useState([]);
    const [correctCount, setCorrectCount] = useState(0);
    const resultsRef = useRef(null);
    const { courseId, moduleId } = useParams();

    useEffect(() => {
        async function fetchTest() {
            try {
                const data = await getTest(courseId, moduleId);
                setTestData(data);
            } catch (error) {
                console.error("Ошибка загрузки теста:", error);
            }
        }
        fetchTest();
    }, [courseId, moduleId]);

    const allQuestionsAnswered = testData.length > 0 && testData.every(
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

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSubmitAttempted(true);

        if (!allQuestionsAnswered) return;

        try {
            const answersArray = testData.map((_, index) => selectedAnswers[index]);
            const authToken = localStorage.getItem('authToken');
            const response = await axiosInstance.post(`/courses/${courseId}/modules/${moduleId}/submit`,
                { answers: answersArray },
                {
                    headers: {
                        'Authorization': `Bearer ${authToken}`
                    }
                }
            );

            const { correctCount, answerCount, correctness } = response.data;
            setCorrectCount(correctCount);
            setCorrectness(correctness);
            setSubmitted(true);

        } catch (error) {
            console.error("Ошибка отправки теста:", error);
        }
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

    const handleRetry = () => {
        window.location.reload();
    };

    return (
        <>
            <Header backgroundColor="bg-[#d7defc]" />
            <div className="flex flex-col items-center min-h-screen bg-[#d7defc] p-6 pt-16">
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
                            isCorrect={correctness[qIndex]} // <-- Новое!
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
                        score={correctCount}
                        totalQuestions={testData.length}
                        onRetry={handleRetry}
                    />
                )}
            </div>
        </>
    );
};

export default TestFormPage;
