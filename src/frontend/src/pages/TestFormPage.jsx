import { useState, useEffect, useRef } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import QuestionCard from "../components/testForm/QuestionCard";
import TestResults from "../components/testForm/TestResults";
import Header from "../components/header/Header";
import Modal from "../components/common/Modal"; // <-- Добавил
import { getTest } from "../services/getTest";
import { setAnswersOfTest } from "../services/setAnswersOfTest";

const TestFormPage = () => {
    const [testData, setTestData] = useState([]);
    const [selectedAnswers, setSelectedAnswers] = useState({});
    const [submitted, setSubmitted] = useState(false);
    const [submitAttempted, setSubmitAttempted] = useState(false);
    const [correctness, setCorrectness] = useState([]);
    const [isSuccess, setIsSuccess] = useState(false);
    const [correctCount, setCorrectCount] = useState(0);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [showModal, setShowModal] = useState(false);
    const [modalMessage, setModalMessage] = useState("");

    const resultsRef = useRef(null);
    const { courseId, moduleId } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchTest() {
            try {
                const data = await getTest(courseId, moduleId);
                setTestData(data);
            } catch (error) {
                console.error("Ошибка загрузки теста:", error);
                setError('Не удалось загрузить тест');
            } finally {
                setLoading(false);
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
            const { correctCount, answerCount, correctness, isSuccess } = await setAnswersOfTest(courseId, moduleId, answersArray);

            setCorrectCount(correctCount);
            setCorrectness(correctness);
            setIsSuccess(isSuccess);
            setSubmitted(true);

        } catch (error) {
            console.error("Ошибка отправки теста:", error);
            setModalMessage("Произошла ошибка при отправке ответов. Попробуйте позже.");
            setShowModal(true);
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

    const handleRetry = async () => {
        setLoading(true);
        try {
            const data = await getTest(courseId, moduleId);
            setTestData(data);
            setSelectedAnswers({});
            setSubmitted(false);
            setSubmitAttempted(false);
            setCorrectness([]);
            setIsSuccess(false);
            setCorrectCount(0);
        } catch (error) {
            console.error("Ошибка перезагрузки теста:", error);

            if (error.response && error.response.status === 500) {
                setModalMessage("Вы исчерпали 3 попытки прохождения теста. Прочитайте лонгриды заново и попробуйте снова.");
                setShowModal(true);
            } else {
                setModalMessage("Не удалось перезагрузить тест. Попробуйте позже.");
                setShowModal(true);
            }
        } finally {
            setLoading(false);
        }
    };

    const handleExit = () => {
        navigate(`/courses/${courseId}`);
    };

    if (loading) {
        return (
            <>
                <Header backgroundColor="bg-[#d7defc]" />
                <div className="flex justify-center items-center min-h-screen bg-[#d7defc]">
                    <div className="text-xl text-gray-600">Загрузка...</div>
                </div>
            </>
        );
    }

    if (error) {
        return (
            <>
                <Header backgroundColor="bg-[#d7defc]" />
                <div className="flex justify-center items-center min-h-screen bg-[#d7defc]">
                    <div className="text-xl text-red-500">{error}</div>
                </div>
            </>
        );
    }

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
                            isCorrect={correctness[qIndex]}
                        />
                    ))}

                    {!submitted && (
                        <button
                            type="submit"
                            disabled={!allQuestionsAnswered}
                            className={`w-full text-white py-3 px-6 rounded-xl text-lg font-bold ${
                                allQuestionsAnswered
                                    ? 'bg-green-500 hover:bg-green-600'
                                    : 'bg-gray-400 cursor-not-allowed'
                            } transition`}
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
                        isSuccess={isSuccess}
                        onExit={handleExit}
                    />
                )}
            </div>

            <Modal
                isOpen={showModal}
                onClose={() => setShowModal(false)}
                title="Внимание!"
                message={modalMessage}
            />
        </>
    );
};

export default TestFormPage;
