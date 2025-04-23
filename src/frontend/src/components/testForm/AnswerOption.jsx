const AnswerOption = ({
                          option,
                          isSelected,
                          isCorrect,
                          submitted,
                          onSelect
                      }) => {
    let buttonStyle = "bg-white hover:bg-[#f0f0f8]";

    if (submitted && isSelected) {
        buttonStyle = isCorrect
            ? "bg-green-100 border-2 border-green-500"
            : "bg-red-100 border-2 border-red-500";
    } else if (isSelected) {
        buttonStyle = "bg-blue-100 border-2 border-blue-500";
    }

    return (
        <button
            type="button"
            onClick={onSelect}
            className={`w-full text-left p-4 rounded-lg text-lg ${buttonStyle}`}
            disabled={submitted}
        >
            {option}
        </button>
    );
};

export default AnswerOption;