import AnswerOption from "./AnswerOption";

const QuestionCard = ({
                          question,
                          qIndex,
                          selectedAnswer,
                          submitted,
                          submitAttempted,
                          onAnswerSelect
                      }) => {
    const isAnswered = selectedAnswer !== undefined;
    const showError = submitAttempted && !isAnswered && !submitted;

    return (
        <div className={`mb-8 bg-white p-6 rounded-xl shadow-sm ${
            showError ? 'animate-pulse border-2 border-red-500' : ''
        }`}>
            <h3 className={`font-sans text-xl font-bold mb-4 text-left ${
                showError ? 'text-red-600' : ''
            }`}>
                {qIndex + 1}. {question.question}
                {showError && (
                    <span className="ml-2 text-sm font-normal">
            (Выберите ответ!)
          </span>
                )}
            </h3>
            <div className="space-y-3">
                {question.options.map((option, oIndex) => (
                    <AnswerOption
                        key={oIndex}
                        option={option}
                        isSelected={selectedAnswer === oIndex}
                        isCorrect={oIndex === question.correctAnswer}
                        submitted={submitted}
                        onSelect={() => onAnswerSelect(qIndex, oIndex)}
                    />
                ))}
            </div>
        </div>
    );
};

export default QuestionCard;